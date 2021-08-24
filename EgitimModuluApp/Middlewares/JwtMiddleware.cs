using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entities.Dtos.AccountDtos;

namespace EgitimModuluApp
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, DataContext dataContext)
        {
            //eğer gelen isteğin header'larından authorization boş değil ise access token'i güvenli bir şekilde cookie'den çekiyoruz
            if (context.Request.Headers["Authorization"].Count > 0)
            {
                string token = context.Request.Cookies["at"];
                string method = context.Request.Method;
                bool info;
                if (token != null)
                    await attachAccountToContext(context, dataContext, token, method, Boolean.TryParse(context.Request.Query["info"], out info));
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.CompleteAsync();
                }
            }
            //token boş işe işleme(her ne ise) devam ediyoruz
            await _next(context);
        }

        private async Task attachAccountToContext(HttpContext context, DataContext dataContext, string token, string method, bool info)
        {
            try
            {
                //token validation parametreleri ve secret key'in appsettings.json'dan çekilmesi
                byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                SecurityToken validatedToken = null;
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                //token validation işlemi
                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;

                //account'u context'e bağlıyoruz
                int accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "i").Value);
                Entities.Role role = (Entities.Role)int.Parse(jwtToken.Claims.First(x => x.Type == "r").Value);
                if (info || method == HttpMethods.Put)
                {
                    if (role == Entities.Role.School)
                    {
                        if (info)
                        {
                            context.Items["Account"] = await dataContext.Schools.Where(sc => sc.Id == accountId).Select(a => new AccountDto
                            {
                                Id = accountId,
                                Username = a.Username,
                                Tel = a.Tel,
                                Name = a.Name,
                                Payment = a.Payment,
                                Role = role,
                            })
                            .FirstOrDefaultAsync();
                        }
                        else
                        {
                            context.Items["Account"] = await dataContext.Schools.Where(sc => sc.Id == accountId).Select(a => new AccountDto
                            {
                                Id = a.Id,
                                Username = a.Username,
                                Role = role
                            })
                            .FirstOrDefaultAsync();
                        }
                    }
                    else if (role == Entities.Role.Teacher)
                    {
                        int schoolId = int.Parse(jwtToken.Claims.First(x => x.Type == "s").Value);
                        if (info)
                        {
                            context.Items["Account"] = await dataContext.Teachers.Where(th => th.Id == accountId).Select(a => new AccountDto
                            {
                                Id = accountId,
                                Username = a.Username,
                                FirstName = a.FirstName,
                                LastName = a.LastName,
                                Tel = a.Tel,
                                EmailAddress = a.EmailAddress,
                                SchoolId = schoolId,
                                Role = role
                            })
                            .FirstOrDefaultAsync();
                        }
                        else
                        {
                            context.Items["Account"] = await dataContext.Teachers.Where(th => th.Id == accountId).Select(a => new AccountDto
                            {
                                Id = accountId,
                                Username = a.Username,
                                Role = role,
                                SchoolId = schoolId
                            })
                            .FirstOrDefaultAsync();
                        }
                    }
                    else if (role == Entities.Role.Student)
                    {
                        int schoolId = int.Parse(jwtToken.Claims.First(x => x.Type == "s").Value);
                        if (info)
                        {
                            context.Items["Account"] = await dataContext.Students.Where(th => th.Id == accountId).Select(a => new AccountDto
                            {
                                Id = accountId,
                                Username = a.Username,
                                FirstName = a.FirstName,
                                LastName = a.LastName,
                                Tel = a.Tel,
                                SchoolNumber = a.SchoolNumber,
                                EmailAddress = a.EmailAddress,
                                Grade = a.Grade,
                                ClassroomName = a.Classroom.Name,
                                ClassroomId = a.Classroom.Id,
                                SchoolId = schoolId,
                                Role = role
                            })
                            .FirstOrDefaultAsync();
                        }
                        else
                        {
                            context.Items["Account"] = await dataContext.Students.Where(th => th.Id == accountId).Select(a => new AccountDto
                            {
                                Id = accountId,
                                Username = a.Username,
                                Role = role,
                                SchoolId = schoolId
                            })
                            .FirstOrDefaultAsync();
                        }
                    }
                }
                else
                {
                    //...güncellenecek
                    //***database user exist check

                    if (role == Entities.Role.School)
                    {
                        context.Items["Account"] = new AccountDto
                        {
                            Id = accountId,
                            Role = role
                        };
                    }
                    else
                    {
                        context.Items["Account"] = new AccountDto
                        {
                            Id = accountId,
                            SchoolId = int.Parse(jwtToken.Claims.First(x => x.Type == "s").Value),
                            Role = role
                        };
                    }
                }
            }
            catch (SecurityTokenException)
            {
                //status kodu ekleyip httprequesti tamamlıyoruz(daha sonra refresh token isteği gelmesi için 403 hata kodu ile bitiriyoruz(forbidden))
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.CompleteAsync();
            }
            catch (Exception)
            {
                //herhangi olası başka bir hata için güvene alıyoruz
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.CompleteAsync();
            }
        }
    }
}