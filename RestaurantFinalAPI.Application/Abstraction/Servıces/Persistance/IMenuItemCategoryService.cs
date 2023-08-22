using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance
{
    
    public interface IMenuItemCategoryService 
    {
        Task<Response<MenuItemCategoryCreateDTO>> AddMenuItemCategory(MenuItemCategoryCreateDTO model);
        Task<Response<bool>> DeleteMenuItemCategory(string Id);
        Task<Response<bool>> UpdateMenuItemCategory(MenuItemCategoryUpdateDTO model);

        Task<Response<List<MenuItemCategoryGetDTO>>> GetAllMenuItemCategory(bool isDelete = false);
        Task<Response<MenuItemCategoryGetDTO>> GetMenuItemCategoryById(string Id, bool isDelete);
    }
}
