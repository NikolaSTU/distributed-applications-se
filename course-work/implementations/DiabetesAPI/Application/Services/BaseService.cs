using Application.DTOs.GlucoseEntry;
using Application.DTOs.Paging;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseService<TEntity, TResponseDto, TCreateUpdateDto, TFilter> : IBaseService<TEntity, TResponseDto, TCreateUpdateDto, TFilter>
        where TEntity : class, IEntity
        where TResponseDto : class, IResponseDTO
        where TCreateUpdateDto : class, ICreateUpdateDTO
    {
        protected readonly IGenericRepository<TEntity> _repository;
        protected readonly IMapper _mapper;
        public BaseService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public virtual async Task<DTOs.Paging.PagedResult<TResponseDto>> GetPagedAsync(BaseQueryParameters<TFilter> query)
        {
            IQueryable<TEntity> source = _repository.GetQueryable();

            if (query.Filters != null)
            {
                source = ApplyFiltering(source, query.Filters);
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                string sortOrder = query.IsDescending ? "desc" : "asc";
                source = source.OrderBy($"{query.SortBy} {sortOrder}");
            }
            else
            {
                source = source.OrderBy("Id desc");
            }

            int totalCount = await source.CountAsync();

            var entities = await source
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return new DTOs.Paging.PagedResult<TResponseDto>
            {
                Items = _mapper.Map<IEnumerable<TResponseDto>>(entities),
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };
        }

        public virtual IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query, TFilter filters)
        {
            return query;
        }

        public virtual async Task<TResponseDto> CreateAsync(TCreateUpdateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _repository.Delete(entity);
            return await _repository.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TResponseDto>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await _repository.FindAsync(predicate);
            return _mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        public virtual async Task<IEnumerable<TResponseDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TResponseDto>>(entities);
        }

        public virtual async Task<TResponseDto> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TResponseDto>(entity);
        }

        public virtual async Task<bool> UpdateAsync(int id, TCreateUpdateDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return false;

            _mapper.Map(dto, entity);

            _repository.Update(entity);
            return await _repository.SaveChangesAsync();
        }


    }
}
