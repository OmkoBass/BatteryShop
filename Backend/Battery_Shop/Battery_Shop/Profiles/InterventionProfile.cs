using AutoMapper;
using Battery_Shop.Dtos;
using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Profiles
{
    public class InterventionProfile : Profile
    {
        public InterventionProfile()
        {
            CreateMap<Intervention, AddInterventionDto>().ReverseMap();
        }
    }
}
