using Application.DTOs.FoodEntry;
using Application.DTOs.MealEntry;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MealEntriesController : BaseCrudController<MealEntry, MealEntryResponseDTO,
        MealEntryCreateUpdateDTO, MealEntryFilter>
    {
        private readonly IMealEntryService _mealEntryService;


        public MealEntriesController(IMealEntryService mealEntryService)
            : base(mealEntryService)
        {
            _mealEntryService = mealEntryService;
        }

        [HttpGet("{id}/dose")]
        public async Task<ActionResult<decimal>> GetDose(int id)
        {
            var userId = GetCurrentUserId();
            var dose = await _mealEntryService.CalculateDose(id, userId);

            return Ok(dose);

        }
    }

    

}
