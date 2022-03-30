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
                    if(user == null || !_userService.ValidateHash(userDTO, user))
                    {
                        return BadRequest("Email ou Senha incorretos");
                    }
                    var tokenString = _userService.CreateToken(user);                
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
                var user = _userService.NewUser(userDTO);
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
