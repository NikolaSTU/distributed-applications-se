using Application.DTOs.Food;
using Application.DTOs.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService : IBaseService<User, UserResponseDTO, UserUpdateDTO, UserFilter>
    {
        Task<UserResponseDTO> RegisterAsync(UserRegisterDTO dto);
        Task<string> LoginAsync(UserLoginDTO dto);
        
    }
}
