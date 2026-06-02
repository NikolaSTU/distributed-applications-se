using Application.DTOs.InsulinEntry;
using Application.DTOs.MealEntry;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InsulinEntriesController : BaseCrudController<InsulinEntry, InsulinEntryResponseDTO,
        InsulinEntryCreateUpdateDTO, InsulinEntryFilter>
    {
        public InsulinEntriesController(IInsulinEntryService insulinEntryService)
            : base(insulinEntryService)
        {
        }
    }


}
