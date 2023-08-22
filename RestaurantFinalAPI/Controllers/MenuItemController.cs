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

        /// <summary>
        /// Gets all menu items.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/MenuItem/GetAllMenuItem
        /// </remarks>
        /// <param name="isDelete">Flag to include deleted items</param>
        /// <returns>List of all menu items</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMenuItem([FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetAllMenuItem(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets a specific menu item by ID.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/MenuItem/GetMenuItem/{Id}
        /// </remarks>
        /// <param name="Id">ID of the menu item</param>
        /// <param name="isDelete">Flag to include deleted items</param>
        /// <returns>Details of the specified menu item</returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMenuItem(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetMenuItemById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Creates a new menu item.
        /// </summary>
        /// <remarks>
        /// URL: POST /api/MenuItem/CreateMenuItem
        /// </remarks>
        /// <param name="model">Item creation model</param>
        /// <returns>Result of the item creation operation</returns>
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItem(MenuItemCreateDTO model)
        {
            var data = await menuItemService.AddMenuItem(model);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Updates an existing menu item.
        /// </summary>
        /// <remarks>
        /// URL: PUT /api/MenuItem/UpdateMenuItem
        /// </remarks>
        /// <param name="model">Item update model</param>
        /// <returns>Result of the item update operation</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItem(MenuItemUpdateDTO model)
        {
            var data = await menuItemService.UpdateMenuItem(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets all menu items by a specific category.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/MenuItem/by-category/{id}
        /// </remarks>
        /// <param name="id">ID of the menu item category</param>
        /// <param name="isDelete">Flag to include deleted items</param>
        /// <returns>List of menu items in the specified category</returns
        [HttpGet("by-category/{id}")]
        public async Task<IActionResult> GetAllMenuItemByCategory(string id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemService.GetAllMenuItemByCategory(id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Deletes a menu item.
        /// </summary>
        /// <remarks>
        /// URL: DELETE /api/MenuItem/DeleteMenuItem/{Id}
        /// </remarks>
        /// <param name="Id">ID of the menu item to delete</param>
        /// <returns>Result of the item deletion operation</returns>
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItem(string Id)
        {
            var data = await menuItemService.DeleteMenuItem(Id);
            return StatusCode(data.StatusCode, data);
        }

    }
}

