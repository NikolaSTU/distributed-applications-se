using Application.DTOs.GlucoseEntry;
using Application.DTOs.InsulinEntry;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GlucoseEntriesController : BaseCrudController<GlucoseEntry, GlucoseEntryResponseDTO,
        GlucoseEntryCreateUpdateDTO, GlucoseEntryFilter>
    {
        public GlucoseEntriesController(IGlucoseEntryService glucoseEntryService)
            : base(glucoseEntryService)
        {
        }
    }
}
