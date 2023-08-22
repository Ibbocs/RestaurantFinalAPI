using AutoMapper;
using RestaurantFinalAPI.Application.DTOs.BookingDTOs;
using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.DTOs.UserDTOs;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Table, TableGetDTO>();
            CreateMap<AppUser, UserGetDTO>();
            CreateMap<Booking, BookingGetDTO>();
            CreateMap<MenuItemCategory, MenuItemCategoryGetDTO>();
            CreateMap<MenuItem, MenuItemGetDTO>()
                .ForMember(dest => dest.MenuItemCategoryName, opt => opt.MapFrom(src => src.MenuItemCategory.CategoryName));
            
        }
    }
}
