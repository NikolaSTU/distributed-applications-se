using Application.DTOs.Food;
using Application.DTOs.Paging;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiabetesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodsController : BaseCrudController<Food, FoodResponseDTO, FoodCreateUpdateDTO, FoodFilter>
    {
        public FoodsController(IFoodService foodService)
            : base(foodService)
        {
        }

        [HttpGet]
        public override async Task<ActionResult<PagedResult<FoodResponseDTO>>> GetPaged([FromQuery] BaseQueryParameters<FoodFilter> query)
        {
            if (query.Filters == null)
            {
                query.Filters = new FoodFilter();
            }

            var result = await _service.GetPagedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<FoodResponseDTO>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            return Ok(entity);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<FoodResponseDTO>> Create(FoodCreateUpdateDTO dto,
            [FromServices] IValidator<FoodCreateUpdateDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update(int id, FoodCreateUpdateDTO dto,
            [FromServices] IValidator<FoodCreateUpdateDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
