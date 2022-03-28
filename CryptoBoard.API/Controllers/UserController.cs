using CryptoBoard.API.Helpers;
using CryptoBoard.Application.DTOs;
using CryptoBoard.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserDTO userDTO)
        {
            try
            {
                var userReturn = _userService.GetUser(userDTO.Email);

                if (userReturn != null)
                {
                    UserDTO user = _userService.Validar(userDTO);
                    if(user == null || !HashPass.Validate(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT"), user.Password))
                    {
                        return BadRequest("Email ou Semha incorretos");
                    }

                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:5001",
                        audience: "https://localhost:5001",
                        claims: new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.UserName.ToString()),
                        },
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signingCredentials
                        );

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return Ok(new { Token = tokenString, user.Id });
                }
                return Unauthorized();
            }
            catch(Exception error)
            {
                return BadRequest(error.ToString());
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {
            try
            {
                var registeredEmail = _userService.GetUser(userDTO.Email);
                var registeredName = _userService.GetName(userDTO.UserName);

                if (registeredEmail != null)
                {
                    return BadRequest("Email já cadastrado no sistema");
                }
                if (registeredName != null)
                {
                    return BadRequest("Nome já cadastrado no sistema");
                }

                UserDTO user = new UserDTO
                {
                    UserName = userDTO.UserName,
                    Email = userDTO.Email,
                    Password = HashPass.Create(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT")),
                    CreationDate = DateTime.Now
                };

                await _userService.PostUser(user);

                return Ok();

            }
            catch(Exception error)
            {
                return BadRequest(error.ToString());
            }
        }
    }
}
