using Application.DTOs.Food;
using Application.DTOs.User;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : BaseService<User, UserResponseDTO, UserUpdateDTO, UserFilter>,IUserService
    {
        private readonly ITokenService _tokenService;


        public UserService(IGenericRepository<User> userRepository, IMapper mapper, ITokenService tokenService) 
            : base (userRepository, mapper)
        {
            _tokenService = tokenService;
        }

        public async Task<UserResponseDTO> RegisterAsync(UserRegisterDTO dto)
        {
            var foundUsers = await _repository.FindAsync(u => u.Username == dto.Username);
            var foundUser = foundUsers.FirstOrDefault();

            if ((foundUser) != null)
            {
                throw new Exception("A user with this username already exists");
            }

            var user = _mapper.Map<User>(dto);

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<string> LoginAsync(UserLoginDTO dto)
        {
            var users = await _repository.FindAsync(u => u.Username == dto.Username);
            var user = users.FirstOrDefault();
            if (user == null)
            {
                throw new Exception("A user with this username doesnt exist");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Username or password is invalid");
            }

            return _tokenService.CreateToken(user);
        }

       
    }
}
