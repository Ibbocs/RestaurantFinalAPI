using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService menuItemService;

        public MenuItemController(IMenuItemService _menuItemService)
        {
            this.menuItemService = _menuItemService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMenuItem([FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetAllMenuItem(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMenuItem(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetMenuItemById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItem(MenuItemCreateDTO model)
        {
            var data = await menuItemService.AddMenuItem(model);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItem(MenuItemUpdateDTO model)
        {
            var data = await menuItemService.UpdateMenuItem(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("by-category/{id}")]
        public async Task<IActionResult> GetAllMenuItemByCategory(string id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetAllMenuItemByCategory(id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItem(string Id)
        {
            var data = await menuItemService.DeleteMenuItem(Id);
            return StatusCode(data.StatusCode, data);


        }
    }
}

