using Application.DTOs.GlucoseEntry;
using Application.DTOs.InsulinEntry;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGlucoseEntryService : IBaseService<GlucoseEntry, GlucoseEntryResponseDTO,
        GlucoseEntryCreateUpdateDTO, GlucoseEntryFilter>
    {

    }
}
