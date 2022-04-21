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

        private IJWTService _jWTService;

        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IJWTService jWTService)
        {
            _userRepository = userRepository;

            _mapper = mapper;

            _jWTService = jWTService;
        }

        public async Task<UserDTO> FindUserById(int id)
        {
            var userEntity = await _userRepository.FindUserById(id);

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<UserDTO> FindUserByEmail(string email)
        {
            var userEntity = await _userRepository.FindUserByEmail(email);

            return _mapper.Map<UserDTO>(userEntity);
        }

        public async Task<string> PostUser(UserDTO userDTO)
        {
            var userEntity = new User(userDTO.UserName, userDTO.Email, HashPass.Create(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT")));

            await _userRepository.PostUser(userEntity);

            return userEntity.Id.ToString();
        }

        public async Task<bool> ValidateEmail(UserDTO userDTO)
        {
            var userEntity = await _userRepository.ValidateEmail(userDTO.Email);

            return _mapper.Map<bool>(userEntity);
        }

        public async Task<bool> ValidateUserName(UserDTO userDTO)
        {
            var userEntity = await _userRepository.ValidateUserName(userDTO.UserName);

            return _mapper.Map<bool>(userEntity);
        }

        public bool ValidateHash(UserDTO userDTO, UserDTO user)
        {
            return HashPass.Validate(userDTO.Password, Environment.GetEnvironmentVariable("AUTH_SALT"), user.Password);
        }

        public async Task<object> LoginUser(UserDTO userDTO)
        {
            try
            {
                var userReturn = await FindUserByEmail(userDTO.Email);

                if (userReturn == null || !ValidateHash(userDTO, userReturn))
                {
                    throw new AppException("Email ou Senha incorretos");
                }

                return new { Token = _jWTService.CreateToken(userReturn), userReturn.Id };
            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }

        public async Task<string> RegisterUser(UserDTO userDTO)
        {
            try
            {
                if (await ValidateEmail(userDTO))
                {
                    throw new AppException("Email já cadastrado no sistema");
                }

                if (await ValidateUserName(userDTO))
                {
                    throw new AppException("Nome já cadastrado no sistema");
                }

                return await PostUser(userDTO);
            }
            catch (Exception error)
            {
                return error.ToString();
            }
        }
    }
}
