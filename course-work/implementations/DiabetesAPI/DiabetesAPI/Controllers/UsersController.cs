using Application.DTOs.Food;
using Application.DTOs.GlucoseEntry;
using Application.DTOs.User;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseCrudController<User, UserResponseDTO,
    UserUpdateDTO, UserFilter>
    {
        public UsersController(IUserService userService)
            : base(userService)
        {
        }


        [NonAction]
        public override async Task<ActionResult<UserResponseDTO>> Create(
                UserUpdateDTO dto,
                [FromServices] IValidator<UserUpdateDTO> validator)
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed, new
            {
                Message = "Създаването на потребители през този ендпоинт е забранено. Използвайте /api/Auth/register."
            });
        }
    }}


