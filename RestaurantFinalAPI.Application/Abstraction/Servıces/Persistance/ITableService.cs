using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance
{
    public interface ITableService
    {
        Task<Response<TableCreateDTO>> AddTable(TableCreateDTO model);
        Task<Response<bool>> DeleteTable(string Id);
        Task<Response<bool>> UpdateTable(TableUpdateDTO model);
        
        Task<Response<List<TableGetDTO>>> GetAllTable(bool isDelete = false);
        Task<Response<TableGetDTO>> GetTableById(string Id, bool isDelete);

        Task<Response<bool>> ChangeOccupiedStatusForTable(string Id, bool IsOccupied);
       // Task<Response<bool>> ChangeReservedStatusForTabl(string Id, bool IsReserved);

    }
}
