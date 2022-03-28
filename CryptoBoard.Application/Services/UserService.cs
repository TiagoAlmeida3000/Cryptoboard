using AutoMapper;
using CryptoBoard.Application.DTOs;
using CryptoBoard.Application.Interfaces;
using CryptoBoard.Domain.Entities;
using CryptoBoard.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
