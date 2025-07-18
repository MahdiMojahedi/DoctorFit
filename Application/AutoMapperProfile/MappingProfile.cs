using Application.Dtos;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.AutoMapperProfile
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();  
            CreateMap<Movement, MovementDto>().ReverseMap();  
            CreateMap<Program, ProgramDto>().ReverseMap();  
            CreateMap<ProgramDayMovement, ProgramDayMovementDto>().ReverseMap();  
            CreateMap<ProgramDays, ProgramDaysDto>().ReverseMap();


            CreateMap<List<Category>, List<CategoryDto>>().ReverseMap();
            CreateMap<List<Movement>, List<MovementDto>>().ReverseMap();
            CreateMap<List<Program>, List<ProgramDto>>().ReverseMap();
            CreateMap<List<ProgramDayMovement>, List<ProgramDayMovementDto>>().ReverseMap();
            CreateMap<List<ProgramDays>, List<ProgramDaysDto>>().ReverseMap();
        }


    }
}
