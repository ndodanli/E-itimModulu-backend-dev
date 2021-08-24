using AutoMapper;
using BC = BCrypt.Net.BCrypt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Entities;
using Entities.Dtos;
using DataAccess;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos.AccountDtos;

namespace EgitimModuluApp.Services.AccountService
{
    public interface IAccountService
    {
        Task<(AccountDto, RefreshJwtDto)> AuthenticateSchool(AuthReqDto model, string ipAddress);
        Task<(AccountDto, RefreshJwtDto)> AuthenticateTeacher(AuthReqDto model, string ipAddress);
        Task<(AccountDto, RefreshJwtDto)> AuthenticateStudent(AuthReqDto model, string ipAddress);
        Task<RefreshJwtDto> RefreshToken(string token, string ipAddress, string accessToken);
        Task<ActionResult> RemoveToken(string token, string ipAddress, string accessToken);
        Task<ActionResult> Register(School model);
        Task<bool> CheckSchoolUsername(string username);
        Task<bool> CheckSchoolEmail(string email);
        Task<bool> CheckSchoolTel(string tel);
    }

    public class AccountService : IAccountService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public AccountService(
            DataContext context,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        #region Revoke Token
        public async Task<ActionResult> RemoveToken(string token, string ipAddress, string accessToken)
        {
            Role role = GetRole(accessToken);
            RefreshToken accountRt;
            switch (role)
            {
                case Role.School:
                    accountRt = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.Token == token && p.SchoolId != null);
                    break;
                case Role.Teacher:
                    accountRt = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.Token == token && p.TeacherId != null);
                    break;
                case Role.Student:
                    accountRt = await _context.RefreshTokens.FirstOrDefaultAsync(p => p.Token == token && p.StudentId != null);
                    break;
                default:
                    accountRt = null;
                    break;
            }
            if (accountRt != null)
                _context.RefreshTokens.Remove(accountRt);
            else
                return new BadRequestResult();

            await _context.SaveChangesAsync();
            return new OkResult();
        }
        #endregion

        #region Get Role
        private Role GetRole(string accessToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken at = tokenHandler.ReadToken(accessToken) as JwtSecurityToken;
            return (Entities.Role)int.Parse(at.Claims.First(x => x.Type == "r").Value);
        }
        #endregion

        #region Refresh Token
        public async Task<RefreshJwtDto> RefreshToken(string token, string ipAddress, string accessToken)
        {

            Role role = GetRole(accessToken);
            IAccount account = await getRefreshToken(token, role);

            // eski refresh-token'ı yenisi ile değiştiriyoruz
            RefreshToken newRefreshToken = generateRefreshToken(ipAddress);
            account.RefreshToken.Token = newRefreshToken.Token;
            account.RefreshToken.Expires = newRefreshToken.Expires;

            _context.Update(account);
            await _context.SaveChangesAsync();

            var jwtToken = generateJwtToken(account);
            RefreshJwtDto tokenPair = new RefreshJwtDto();
            tokenPair.RefreshToken = newRefreshToken.Token;
            tokenPair.AccessToken = jwtToken;
            return tokenPair;
        }
        #endregion

        #region Register
        public async Task<ActionResult> Register(School account)
        {
            if (_context.Schools.Any(x => x.Username == account.Username))
            {
                return new ConflictObjectResult(new { usernameExist = $"<b>{account.Username}</b> zaten kullanılıyor." });
            }
            account.Role = Role.School;
            // account.Password = BC.HashPassword(account.Password);
            account.Password = account.Password;
            //geliştime aşaması için payment id 1 olarak ayarlandı
            account.PaymentId = 1;
            await _context.Schools.AddAsync(account);
            await _context.SaveChangesAsync();
            return new OkResult();
        }
        #endregion

        #region Get Refresh Token
        private async Task<IAccount> getRefreshToken(string token, Role role)
        {
            IAccount account = null;
            switch (role)
            {
                case Role.School:
                    account = await _context.Schools.Include(rt => rt.RefreshToken).SingleOrDefaultAsync(u => u.RefreshToken.Token == token);
                    break;
                case Role.Teacher:
                    account = await _context.Teachers.Include(rt => rt.RefreshToken).SingleOrDefaultAsync(u => u.RefreshToken.Token == token);
                    break;
                case Role.Student:
                    account = await _context.Students.Include(rt => rt.RefreshToken).SingleOrDefaultAsync(u => u.RefreshToken.Token == token);
                    break;
                default:
                    break;
            }
            if (account == null) throw new Exception("Invalid token");
            if (!account.RefreshToken.IsActive) throw new Exception("Invalid token");
            return (account);
        }
        #endregion

        #region Generate Access Token
        private string generateJwtToken<T>(T account) where T : IAccount
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("i", account.Id.ToString()),
                    new Claim("r", ((int)account.Role).ToString()),
                    account.Role == Role.Teacher || account.Role == Role.Student ?
                    new Claim("s", ((int)account.GetType().GetProperty("SchoolId").GetValue(account, null)).ToString())
                    : null
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Generate Refresh Token
        private RefreshToken generateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = randomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                CreatedByIp = ipAddress
            };
        }
        #endregion

        #region Generate Random Refresh Token
        private string randomTokenString()
        {
            //!unique için farklı şekilde şifrelenebilir(ileride)**GUID, ya da veri tabanında çözülebilir.
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
        #endregion

        #region Authenticate SCHOOL
        public async Task<(AccountDto, RefreshJwtDto)> AuthenticateSchool(AuthReqDto model, string ipAddress)
        {
            School account = await _context.Schools
                          .Include(a => a.Payment)
                          .Include(a => a.RefreshToken)
                          .SingleOrDefaultAsync(p => p.Code == model.SchoolCode && p.Username == model.Username);

            /**
            *performans açısından geliştirme sürecinde hash parola sistemi ve kontrolü kaldırıldı
            */

            // if (account == null || !BC.Verify(model.Password, account.Password))
            //     throw new Exception("Kullanıcı adı ya da parola hatalı.");
            if (account == null || model.Password != account.Password)
                throw new Exception("Kullanıcı adı, parola ya da kurum kodu hatalı.");
            var jwtToken = generateJwtToken<IAccount>(account);
            var refreshToken = generateRefreshToken(ipAddress);
            if (account.RefreshToken == null)
                account.RefreshToken = refreshToken;
            else
            {
                account.RefreshToken.Token = refreshToken.Token;
                account.RefreshToken.Expires = refreshToken.Expires;
            }

            _context.Update(account);
            await _context.SaveChangesAsync();
            account.Password = null;
            RefreshJwtDto refreshJwt = new RefreshJwtDto();

            var accountDto = _mapper.Map<School, AccountDto>(account);

            refreshJwt.AccessToken = jwtToken;
            refreshJwt.RefreshToken = refreshToken.Token;
            return (accountDto, refreshJwt);
        }
        #endregion

        #region Authenticate TEACHER
        public async Task<(AccountDto, RefreshJwtDto)> AuthenticateTeacher(AuthReqDto model, string ipAddress)
        {
            Teacher account = await _context.Teachers
                          .Include(p => p.RefreshToken)
                          .SingleOrDefaultAsync(p => p.School.Code == model.SchoolCode && p.Username == model.Username); //okul kodundan gelecek

            /**
            *performans açısından geliştirme sürecinde hash parola sistemi ve kontrolü kaldırıldı
            */

            // if (account == null || !BC.Verify(model.Password, account.Password))
            //     throw new Exception("Kullanıcı adı ya da parola hatalı.");
            if (account == null || model.Password != account.Password)
                throw new Exception("Kullanıcı adı, parola ya da kurum kodu hatalı.");
            var jwtToken = generateJwtToken<IAccount>(account);
            var refreshToken = generateRefreshToken(ipAddress);
            if (account.RefreshToken == null)
                account.RefreshToken = refreshToken;
            else
            {
                account.RefreshToken.Token = refreshToken.Token;
                account.RefreshToken.Expires = refreshToken.Expires;
            }

            _context.Update(account);
            await _context.SaveChangesAsync();
            account.Password = null;
            RefreshJwtDto refreshJwt = new RefreshJwtDto();

            var accountDto = _mapper.Map<Teacher, AccountDto>(account);

            refreshJwt.AccessToken = jwtToken;
            refreshJwt.RefreshToken = refreshToken.Token;
            return (accountDto, refreshJwt);
        }
        #endregion

        #region Authenticate STUDENT
        public async Task<(AccountDto, RefreshJwtDto)> AuthenticateStudent(AuthReqDto model, string ipAddress)
        {
            Student account = await _context.Students
                          .Where(p => p.School.Code == model.SchoolCode && p.Username == model.Username)
                          .Include(p => p.Classroom)
                          .Include(p => p.RefreshToken)
                          .SingleOrDefaultAsync();

            /**
            *performans açısından geliştirme sürecinde hash parola sistemi ve kontrolü kaldırıldı
            */

            // if (account == null || !BC.Verify(model.Password, account.Password))
            //     throw new Exception("Kullanıcı adı ya da parola hatalı.");
            if (account == null || model.Password != account.Password)
                throw new Exception("Kullanıcı adı, parola ya da kurum kodu hatalı.");
            var jwtToken = generateJwtToken<IAccount>(account);
            var refreshToken = generateRefreshToken(ipAddress);
            if (account.RefreshToken == null)
                account.RefreshToken = refreshToken;
            else
            {
                account.RefreshToken.Token = refreshToken.Token;
                account.RefreshToken.Expires = refreshToken.Expires;
            }

            _context.Update(account);
            await _context.SaveChangesAsync();
            account.Password = null;
            RefreshJwtDto refreshJwt = new RefreshJwtDto();

            var accountDto = _mapper.Map<Student, AccountDto>(account);

            refreshJwt.AccessToken = jwtToken;
            refreshJwt.RefreshToken = refreshToken.Token;
            return (accountDto, refreshJwt);
        }
        #endregion

        #region Check Properties If Exist
        public async Task<bool> CheckSchoolUsername(string username)
        {
            return await _context.Schools.AnyAsync(t => t.Username.ToLower() == username.ToLower());
        }

        public async Task<bool> CheckSchoolEmail(string emailAddress)
        {
            return await _context.Schools.AnyAsync(t => t.EmailAddress.ToLower() == emailAddress.ToLower());
        }

        public async Task<bool> CheckSchoolTel(string tel)
        {
            return await _context.Schools.AnyAsync(t => t.Tel.ToLower() == tel.ToLower());
        }
        #endregion

    }
}
