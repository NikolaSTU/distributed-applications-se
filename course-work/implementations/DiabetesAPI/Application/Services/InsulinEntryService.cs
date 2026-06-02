using Application.DTOs.Food;
using Application.DTOs.GlucoseEntry;
using Application.DTOs.InsulinEntry;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class InsulinEntryService : BaseService<InsulinEntry,
        InsulinEntryResponseDTO, InsulinEntryCreateUpdateDTO, InsulinEntryFilter>, IInsulinEntryService
    {
        public InsulinEntryService(IGenericRepository<InsulinEntry> repository, IMapper mapper)
            : base(repository, mapper)
        { }

        public override IQueryable<InsulinEntry> ApplyFiltering(IQueryable<InsulinEntry> query, InsulinEntryFilter filters)
        {
            if (filters.CurrentUserId.HasValue)
            {
                query = query.Where(e => e.UserId == filters.CurrentUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.Type))
            {
                query = query.Where(e => e.Type == filters.Type);
            }

            if (filters.FromDate.HasValue)
            {
                query = query.Where(e => e.InjectedAt >= filters.FromDate.Value);
            }

            if (filters.ToDate.HasValue)
            {
                query = query.Where(e => e.InjectedAt <= filters.ToDate.Value);
            }

            if (filters.MinUnits.HasValue)
            {
                query = query.Where(e => e.Units >= filters.MinUnits.Value);
            }

            if (filters.MaxUnits.HasValue)
            {
                query = query.Where(e => e.Units <= filters.MaxUnits.Value);
            }

            return query;
        }
    }
}
