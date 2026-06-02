using Application.DTOs.Paging;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DiabetesAPI.Controllers
{
    [Authorize]
    public class BaseCrudController<TEntity, TResponseDto, TCreateUpdateDto, TFilter> : ControllerBase
    where TEntity : class, IEntity
    where TResponseDto : class, IResponseDTO
    where TCreateUpdateDto : class, ICreateUpdateDTO
    where TFilter : class, new()
    {
        protected readonly IBaseService<TEntity, TResponseDto, TCreateUpdateDto, TFilter> _service;
        public BaseCrudController(IBaseService<TEntity, TResponseDto, TCreateUpdateDto, TFilter> service)
        {     
            _service = service;
        }

        protected int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
        [HttpGet]
        public virtual async Task<ActionResult<PagedResult<TResponseDto>>> GetPaged([FromQuery] BaseQueryParameters<TFilter> query)
        {
            if (query.Filters == null)
            {
                query.Filters = new TFilter();
            }

            if (!User.IsInRole("Admin"))
            {
                var userId = GetCurrentUserId();

                var property = query.Filters.GetType().GetProperty("CurrentUserId");
                if (property != null)
                {
                    property.SetValue(query.Filters, userId);
                }
            }

            var result = await _service.GetPagedAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TResponseDto>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if(entity == null)
            {
                return NotFound();
            }

            var userId = GetCurrentUserId();

            if (!User.IsInRole("Admin") && entity.UserId != null && entity.UserId != GetCurrentUserId())
            {
                return Forbid(); 
            }

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TResponseDto>> Create
            (TCreateUpdateDto dto,
            [FromServices] IValidator<TCreateUpdateDto> validator)
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            dto.UserId = GetCurrentUserId(); // override kudeto e global

            await _service.CreateAsync(dto);
            return Ok();
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(int id,
            TCreateUpdateDto dto,
            [FromServices] IValidator<TCreateUpdateDto> validator)
        {
            var entity = await _service.GetByIdAsync(id);
            
            if (!User.IsInRole("Admin") && entity.UserId != GetCurrentUserId())
            {
                return Forbid(); 
            }

            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }

            dto.UserId = GetCurrentUserId();

            await _service.UpdateAsync(id, dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (!User.IsInRole("Admin") && entity.UserId != GetCurrentUserId())
                return Forbid();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
