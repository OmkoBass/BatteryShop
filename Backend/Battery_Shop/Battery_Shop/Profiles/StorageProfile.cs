using AutoMapper;
using Battery_Shop.Dtos;
using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Profiles
{
    public class StorageProfile : Profile
    {
        public StorageProfile()
        {
            CreateMap<Storage, AddStorageDto>().ReverseMap();
            CreateMap<Storage, StorageDto>().ReverseMap();
        }
    }
}
