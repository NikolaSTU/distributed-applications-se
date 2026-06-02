using Application.DTOs.Food;
using Application.DTOs.FoodEntry;
using Application.DTOs.GlucoseEntry;
using Application.DTOs.InsulinEntry;
using Application.DTOs.MealEntry;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Food, FoodResponseDTO>();
            CreateMap<FoodCreateUpdateDTO, Food>();

            CreateMap<FoodEntry, FoodEntryResponseDTO>();
            CreateMap<FoodEntryCreateUpdateDTO, FoodEntry>();

            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRegisterDTO, User>();
            CreateMap<UserUpdateDTO, User>();

            CreateMap <MealEntry, MealEntryResponseDTO>();
            CreateMap<MealEntryCreateUpdateDTO, MealEntry>();

            CreateMap<InsulinEntry, InsulinEntryResponseDTO>();
            CreateMap<InsulinEntryCreateUpdateDTO, InsulinEntry>();

            CreateMap<GlucoseEntry, GlucoseEntryResponseDTO>();
            CreateMap<GlucoseEntryCreateUpdateDTO, GlucoseEntry>();
        }
    }
}
