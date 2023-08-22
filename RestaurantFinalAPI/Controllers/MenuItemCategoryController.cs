using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Persistance.Implementation.Services;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemCategoryController : ControllerBase
    {
        private readonly IMenuItemCategoryService menuItemCategoryService;

        public MenuItemCategoryController(IMenuItemCategoryService _menuItemCategoryService)
        {
            this.menuItemCategoryService = _menuItemCategoryService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMenuItemCategory([FromQuery] bool isDelete = false)
        {
            var data = await menuItemCategoryService.GetAllMenuItemCategory(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMenuItemCategory(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemCategoryService.GetMenuItemCategoryById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItemCategory(MenuItemCategoryCreateDTO model)
        {
            var data = await menuItemCategoryService.AddMenuItemCategory(model);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItemCategory(MenuItemCategoryUpdateDTO model)
        {
            var data = await menuItemCategoryService.UpdateMenuItemCategory(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItemCategory(string Id)
        {
            var data = await menuItemCategoryService.DeleteMenuItemCategory(Id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
