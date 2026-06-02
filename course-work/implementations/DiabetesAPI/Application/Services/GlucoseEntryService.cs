using Application.DTOs.GlucoseEntry;
using Application.DTOs.InsulinEntry;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GlucoseEntryService : BaseService<GlucoseEntry,
        GlucoseEntryResponseDTO, GlucoseEntryCreateUpdateDTO, GlucoseEntryFilter>, IGlucoseEntryService
    {
        public GlucoseEntryService(IGenericRepository<GlucoseEntry> repository, IMapper mapper)
            : base(repository, mapper) 
        { }

        public override IQueryable<GlucoseEntry> ApplyFiltering(IQueryable<GlucoseEntry> query, GlucoseEntryFilter filters)
        {
            if (filters.CurrentUserId.HasValue)
            {
                query = query.Where(e => e.UserId == filters.CurrentUserId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filters.Note))
            {
                query = query.Where(e => e.Note.Contains(filters.Note));
            }

            if (!string.IsNullOrWhiteSpace(filters.Source))
            {
                query = query.Where(e => e.Source == filters.Source);
            }

            if (filters.FromDate.HasValue)
            {
                query = query.Where(e => e.MeasuredAt >= filters.FromDate.Value);
            }
            if (filters.ToDate.HasValue)
            {
                query = query.Where(e => e.MeasuredAt <= filters.ToDate.Value);
            }

            if (filters.MaxValue.HasValue)
            {
                query = query.Where(e => e.Value <= filters.MaxValue.Value);
            }

            return query;
        }
    }
}
