using Application.DTOs.Food;
using Application.DTOs.Paging;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBaseService<TEntity, TResponseDTO, TCreateUpdateDTO, TFilter> where TEntity : class
    {
        Task<PagedResult<TResponseDTO>> GetPagedAsync(BaseQueryParameters<TFilter> query);
        IQueryable<TEntity> ApplyFiltering(IQueryable<TEntity> query, TFilter filters);
        Task<TResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<TResponseDTO>> GetAllAsync();
        Task<TResponseDTO> CreateAsync(TCreateUpdateDTO dto);
        Task<bool> UpdateAsync(int id, TCreateUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<TResponseDTO>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
