using Application.DTOs.Food;
using Application.DTOs.FoodEntry;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodEntriesController : BaseCrudController<FoodEntry, FoodEntryResponseDTO,
        FoodEntryCreateUpdateDTO, FoodEntryFilter>
    {
        public FoodEntriesController(IFoodEntryService foodEntryService)
            : base(foodEntryService)
        {
        }
    }
}
