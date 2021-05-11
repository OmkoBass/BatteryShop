using AutoMapper;
using Battery_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Battery_Shop.Profiles
{
    public class BatteryShopDto : Profile
    {
        public BatteryShopDto()
        {
            CreateMap<BatteryShop, BatteryShopDto>().ReverseMap();
        }
    }
}
