using AutoMapper;
using CryptoBoard.Application.DTOs;
using CryptoBoard.Application.Helpers;
using CryptoBoard.Application.Interfaces;
using CryptoBoard.Domain.Entities;
using CryptoBoard.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBoard.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public UserDTO FindUser(int id)
        {
            var userEntity = _userRepository.FindUser(id);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public UserDTO GetName(string name)
        {
            var userEntity = _userRepository.GetName(name);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public UserDTO GetUser(string email, string password)
        {
            var userEntity = _userRepository.GetUser(email, password);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public UserDTO GetUser(string email)
        {
            var userEntity = _userRepository.GetUser(email);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public int? GetUserId(string email)
        {
            var userEntity = _userRepository.GetUser(email);
            return _mapper.Map<int?>(userEntity);
        }

        public async Task PostUser(UserDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _userRepository.PostUser(userEntity);
        }

        public UserDTO Validar(UserDTO userDTO)
        {
            User user = new User
            {
                Email = userDTO.Email
            };

            var userEntity = _userRepository.Validar(user);
            return _mapper.Map<UserDTO>(userEntity);
        }

        public string CreateToken(UserDTO userDTO)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "https://localhost:5001",
                audience: "https://localhost:5001",
                claims: new List<Claim>
                {
                            new Claim(ClaimTypes.NameIdentifier, userDTO.Id.ToString()),
                            new Claim(ClaimTypes.Name, userDTO.UserName.ToString()),
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public bool ValidateHash(UserDTO userDTO, UserDTO user)
        {
            return HashPass.Validate(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT"), user.Password);
        }

        public UserDTO NewUser(UserDTO userDTO)
        {
            UserDTO user = new UserDTO
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                Password = HashPass.Create(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT")),
                CreationDate = DateTime.Now
            };

            return user;
        }
    }
}
