using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using System.Data;

namespace RestaurantFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        //todo aldigim bezi string falan datalar var methodlarda, hansi ki olara validation qoyulmur, bulari da model ile deyisim ya basqa yolu var validation ucun??
        private readonly ITableService tableService;

        public TableController(ITableService _tableService)
        {
            this.tableService = _tableService;
        }

        /// <summary>
        /// Gets all tables.
        /// </summary>
        /// <remarks>URL: GET /api/Table</remarks>
        /// <param name="isDelete">Flag to include deleted items</param>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTable([FromQuery] bool isDelete = false)
        {
            var data = await tableService.GetAllTable(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Gets a specific table by ID.
        /// </summary>
        /// <param name="Id">ID of the table</param>
        /// <param name="isDelete">Flag to include deleted items</param>
        /// <remarks>URL: GET /api/Table/{Id}</remarks>
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetTable(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await tableService.GetTableById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Creates a new table.
        /// </summary>
        /// <param name="model">Table information</param>
        /// <remarks>URL: POST /api/Table</remarks>
        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateTable(TableCreateDTO model)
        {
            var data = await tableService.AddTable(model);
            return StatusCode(data.StatusCode, data);

        }

        /// <summary>
        /// Updates an existing table.
        /// </summary>
        /// <param name="model">Updated table information</param>
        /// <remarks>URL: PUT /api/Table</remarks>
        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateTable(TableUpdateDTO model)
        {
            var data = await tableService.UpdateTable(model);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Changes the occupied status for a table.
        /// </summary>
        /// <param name="id">ID of the table</param>
        /// <param name="isOccupied">New occupied status</param>
        /// <remarks>URL: PUT /api/Table/tables/{id}/status</remarks>
        [HttpPut("tables/{id}/status")]//todo queryden gelencek deyeri bele istemeliyemi? yoxsa routede olur bu?
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> ChangeOccupiedStatusForTable(string id, [FromQuery] bool isOccupied)
        {
            var data = await tableService.ChangeOccupiedStatusForTable(id, isOccupied);
            return StatusCode(data.StatusCode, data);
        }

        /// <summary>
        /// Deletes a table.
        /// </summary>
        /// <param name="Id">ID of the table to delete</param>
        /// <remarks>URL: DELETE /api/Table/{Id}</remarks>
        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteTable(string Id)
        {
            var data = await tableService.DeleteTable(Id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
