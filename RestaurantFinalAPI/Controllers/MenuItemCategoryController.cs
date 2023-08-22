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

        /// <summary>
        /// Gets all menu item categories.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/MenuItemCategory/GetAllMenuItemCategory
        /// </remarks>
        /// <param name="isDelete">Flag to include deleted categories</param>
        /// <returns>List of all menu item categories</returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMenuItemCategory([FromQuery] bool isDelete = false)
        {
            var data = await menuItemCategoryService.GetAllMenuItemCategory(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets a specific menu item category by ID.
        /// </summary>
        /// <remarks>
        /// URL: GET /api/MenuItemCategory/GetMenuItemCategory/{Id}
        /// </remarks>
        /// <param name="Id">ID of the menu item category</param>
        /// <param name="isDelete">Flag to include deleted categories</param>
        /// <returns>Details of the specified menu item category</returns>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetMenuItemCategory(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await menuItemCategoryService.GetMenuItemCategoryById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Creates a new menu item category.
        /// </summary>
        /// <remarks>
        /// URL: POST /api/MenuItemCategory/CreateMenuItemCategory
        /// </remarks>
        /// <param name="model">Category creation model</param>
        /// <returns>Result of the category creation operation</returns>
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateMenuItemCategory(MenuItemCategoryCreateDTO model)
        {
            var data = await menuItemCategoryService.AddMenuItemCategory(model);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Updates an existing menu item category.
        /// </summary>
        /// <remarks>
        /// URL: PUT /api/MenuItemCategory/UpdateMenuItemCategory
        /// </remarks>
        /// <param name="model">Category update model</param>
        /// <returns>Result of the category update operation</returns>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateMenuItemCategory(MenuItemCategoryUpdateDTO model)
        {
            var data = await menuItemCategoryService.UpdateMenuItemCategory(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Deletes a menu item category.
        /// </summary>
        /// <remarks>
        /// URL: DELETE /api/MenuItemCategory/DeleteMenuItemCategory/{Id}
        /// </remarks>
        /// <param name="Id">ID of the menu item category to delete</param>
        /// <returns>Result of the category deletion operation</returns>
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteMenuItemCategory(string Id)
        {
            var data = await menuItemCategoryService.DeleteMenuItemCategory(Id);
            return StatusCode(data.StatusCode, data);
        }

        public class Response<T>
        {
            public T Data { get; set; }
            public int StatusCode { get; set; }
        }
    }
}
