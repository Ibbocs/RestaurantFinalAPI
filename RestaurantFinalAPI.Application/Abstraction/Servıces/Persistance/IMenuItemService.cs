using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance
{
    public interface IMenuItemService
    {
        Task<Response<MenuItemCreateDTO>> AddMenuItem(MenuItemCreateDTO model);
        Task<Response<bool>> DeleteMenuItem(string Id);
        Task<Response<bool>> UpdateMenuItem(MenuItemUpdateDTO model);

        Task<Response<List<MenuItemGetDTO>>> GetAllMenuItem(bool isDelete = false);
        Task<Response<MenuItemGetDTO>> GetMenuItemById(string Id, bool isDelete);

        Task<Response<List<MenuItemGetDTO>>> GetAllMenuItemByCategory(string categoryId, bool isDeleted);
    }
}
