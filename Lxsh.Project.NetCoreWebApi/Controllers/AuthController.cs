
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.NetCoreWebApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]/[action]")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController:Controller
    {
        [HttpGet]
        public IActionResult GetToken(string name)
        {
            try
            {

                //定义发行人issuer
                string iss = "lxsh.Auth";
                //定义受众人audience
                string aud = "api.auth";

                //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
                IEnumerable<Claim> claims = new Claim[]
                {
                    new Claim(JwtClaimTypes.Id,"1"),
                    new Claim(JwtClaimTypes.Name,name),
                    new Claim(JwtClaimTypes.Role,"admin"),
                };
                //notBefore  生效时间
                // long nbf =new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
                var nbf = DateTime.UtcNow;
                //expires   //过期时间
                // long Exp = new DateTimeOffset(DateTime.Now.AddSeconds(1000)).ToUnixTimeSeconds();
                var Exp = DateTime.UtcNow.AddDays(100);
                //signingCredentials  签名凭证
                string sign = "sifangboruiyanfayibu"; //SecurityKey 的长度必须 大于等于 16个字符
                var secret = Encoding.UTF8.GetBytes(sign);
                var key = new SymmetricSecurityKey(secret);
                var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //String issuer = default(String), String audience = default(String), IEnumerable<Claim> claims = null, Nullable<DateTime> notBefore = default(Nullable<DateTime>), Nullable<DateTime> expires = default(Nullable<DateTime>), SigningCredentials signingCredentials = null

                var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims:claims,notBefore:nbf,expires:Exp, signingCredentials: signcreds);

                var JwtHander = new JwtSecurityTokenHandler();

                var token = JwtHander.WriteToken(jwt);
                Request.HttpContext.User.AddIdentity(new ClaimsIdentity(claims));
              
                return Ok(new
                {
                    access_token = token,
                    token_type = "Bearer",
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
