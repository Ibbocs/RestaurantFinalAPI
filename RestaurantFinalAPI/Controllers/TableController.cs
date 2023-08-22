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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllTable([FromQuery] bool isDelete = false)
        {
            var data = await tableService.GetAllTable(isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetTable(string Id, [FromQuery] bool isDelete = false)
        {
            var data = await tableService.GetTableById(Id, isDelete);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> CreateTable(TableCreateDTO model)
        {
            var data = await tableService.AddTable(model);
            return StatusCode(data.StatusCode, data);

        }

        [HttpPut("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> UpdateTable(TableUpdateDTO model)
        {
            var data = await tableService.UpdateTable(model);
            return StatusCode(data.StatusCode, data);
        }

        [HttpPut("tables/{id}/status")]//todo queryden gelencek deyeri bele istemeliyemi? yoxsa routede olur bu?
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> ChangeOccupiedStatusForTable(string id, [FromQuery] bool isOccupied)
        {
            var data = await tableService.ChangeOccupiedStatusForTable(id, isOccupied);
            return StatusCode(data.StatusCode, data);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin", Roles = "Admin")]
        public async Task<IActionResult> DeleteTable(string Id)
        {
            var data = await tableService.DeleteTable(Id);
            return StatusCode(data.StatusCode, data);
        }
    }
}
