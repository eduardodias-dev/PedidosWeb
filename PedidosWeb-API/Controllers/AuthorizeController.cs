using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PedidosWeb_API.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PedidosWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private IConfiguration _configuration;

        public AuthorizeController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]User user)
        {
            try
            {
                IdentityUser identityUser = new IdentityUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    EmailConfirmed = true
                };

                IdentityResult result = await _userManager.CreateAsync(identityUser, user.Password);

                if (!result.Succeeded)
                {

                    return BadRequest(result.Errors);
                }

                await _signInManager.SignInAsync(identityUser, false);

                return Ok(GenerateToken(user));

            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]User user)
        {
            try
            {
                var result = await  _signInManager.PasswordSignInAsync(user.Name, user.Password, true, false);

                if (result.Succeeded) {
                    return Ok(GenerateToken(user));
                }
                else
                {
                    return BadRequest("Usuário e/ou senha inválidos.");
                }
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        private UserToken GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Name), //Declara o usuário como único
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            var expirationDate = DateTime.Now.AddHours(double.Parse(_configuration["TokenConfiguration:ExpirationHours"]));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken
            (
                issuer : _configuration["TokenConfiguration:Issuer"],
                audience : _configuration["TokenConfiguration:Audience"],
                signingCredentials:  credentials,
                expires: expirationDate,
                claims: claims
            );

            return new UserToken
            {
                Authenticated = true,
                Message = "Token gerado com sucesso!",
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expirationDate
            };
        }
    }
}
