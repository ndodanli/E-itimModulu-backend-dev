using EgitimModuluApp.Services;
using Entities.Dtos;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Entities.Dtos.SchoolDtos;
using AutoMapper;
using Entities.Dtos.AccountDtos;
using EgitimModuluApp.Services.AccountService;

namespace EgitimModuluApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public AccountController(
            IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [HttpPost, Route("[action]")]
        public async Task<ActionResult<AccountDto>> Authenticate(AuthReqDto authReqDto)
        {
            if (authReqDto.AccountType == Role.School)
            {
                var (authAccount, refreshJwt) = await _accountService.AuthenticateSchool(authReqDto, ipAddress());
                setTokenCookie(refreshJwt.RefreshToken, refreshJwt.AccessToken);
                return Ok(authAccount);
            }
            else if (authReqDto.AccountType == Role.Teacher)
            {
                var (authAccount, refreshJwt) = await _accountService.AuthenticateTeacher(authReqDto, ipAddress());
                setTokenCookie(refreshJwt.RefreshToken, refreshJwt.AccessToken);
                return Ok(authAccount);
            }
            else if (authReqDto.AccountType == Role.Student)
            {
                var (authAccount, refreshJwt) = await _accountService.AuthenticateStudent(authReqDto, ipAddress());
                setTokenCookie(refreshJwt.RefreshToken, refreshJwt.AccessToken);
                return Ok(authAccount);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost, Route("[action]")]
        public async Task<ActionResult> RefreshToken()
        {
            string refreshToken = Request.Cookies["rt"];
            string accessToken = Request.Cookies["at"];
            RefreshJwtDto response = await _accountService.RefreshToken(refreshToken, ipAddress(), accessToken);
            setTokenCookie(response.RefreshToken, response.AccessToken);
            return Ok();
        }

        [HttpPost, Route("[action]")]
        public async Task<ActionResult> Logout()
        {
            // gelen request http-only olduğundan cookie'lerinden refresh token'ı çekiyoruz
            var refreshToken = Request.Cookies["rt"];
            var accessToken = Request.Cookies["at"];

            if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(accessToken))
                return BadRequest();

            ActionResult result = await _accountService.RemoveToken(refreshToken, ipAddress(), accessToken);
            Response.Cookies.Delete("at");
            Response.Cookies.Delete("rt");

            return result;
        }

        [HttpPost, Route("[action]")]
        public async Task<ActionResult> Register([FromBody] SchoolRegisterDto school)
        {
            var result = await _accountService.Register(_mapper.Map<School>(school));
            return result;
        }

        [HttpGet, Route("[action]")]
        public ActionResult<AccountDto> GetUser()
        {
            return Ok(Account);
        }

        private void setTokenCookie(string token, string jwtToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
                SameSite = SameSiteMode.Lax,
            };
            var jwtCookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Lax,
            };
            Response.Cookies.Append("rt", token, cookieOptions);
            Response.Cookies.Append("at", jwtToken, jwtCookieOptions);
        }

        [HttpGet, Route("username")]
        public async Task<ActionResult> GetCheckUsername([FromQuery] string username)
        {
            bool isExist = await _accountService.CheckSchoolUsername(username);
            return Ok(isExist);
        }

        [HttpGet, Route("email")]
        public async Task<ActionResult> GetCheckEmail([FromQuery] string emailAddress)
        {
            bool isExist = await _accountService.CheckSchoolEmail(emailAddress);
            return Ok(isExist);
        }

        [HttpGet, Route("tel")]
        public async Task<ActionResult> GetCheckTel([FromQuery] string tel)
        {
            bool isExist = await _accountService.CheckSchoolTel(tel);
            return Ok(isExist);
        }

        private string ipAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
