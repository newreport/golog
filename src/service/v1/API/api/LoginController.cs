﻿using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        //private readonly IBLLUser _iBLLUser;
        private readonly IBLLBlog _iBLLBlog;
        private readonly IBLLUser _iBLLUser;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private result _result;
        public LoginController(IBLLUser bLLUser, IBLLBlog bLLBlog, IConfiguration configuration, IMapper mapper)
        {
            _iBLLUser = bLLUser;
            _iBLLBlog = bLLBlog;
            _configuration = configuration;
            _mapper = mapper;

        }

        [HttpGet]
        public string GetTest()
        {
            var res = Appsettings.Get<Dictionary<string, string>>("ConnectionStrings");
            return "连接成功" + DateTime.Now + "nya";
        }


        [HttpPost("")]
        public async Task<result> Login([FromForm] login_user user)
        {
            _result.Instance();
            //var userRes = await _iBLLUser.FirstOrDefaultSync(x => x.UserName == user.user_id&&x.Password);
            if (user == null)
                _result.Fail();
            else
            {
                _result.Success(JObject.FromObject(user));
            }
            return _result;
        }


        private string GenerateJwtToken(User userInfo)
        {
            string algorithm = SecurityAlgorithms.HmacSha256;
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")); //私钥
            SigningCredentials signing = new SigningCredentials(key, algorithm);


            Claim[] claims = new[]
             {
            new Claim(ClaimTypes.NameIdentifier,userInfo.UserId.ToString()),
            new Claim(ClaimTypes.Name, userInfo.UserName),
            //new Claim(JwtRegisteredClaimNames.Sub, "client_brower"), //jwt所面向的用户
            };

            string issuer = null;
            string audience = null;
            DateTime notBefore = DateTime.Now;
            DateTime expires = notBefore.AddHours(9);
            JwtSecurityToken jwtToken = new JwtSecurityToken(issuer, audience, claims, notBefore, expires, signing);

            string strToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return strToken;
        }

        //[HttpGet]
        //public async Task<result> TestBlog()
        //{
        //    _result.Instance();
        //    var user = await _iBLLBlog.FirstOrDefaultSync(x => x.BlogId != null);
        //    if (user == null)
        //        _result.Fail();
        //    else
        //        _result.Success(JObject.FromObject(user));
        //    return _result;
        //}
    }
}
