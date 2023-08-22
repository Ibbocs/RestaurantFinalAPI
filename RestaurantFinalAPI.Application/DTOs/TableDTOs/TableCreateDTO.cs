using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.DTOs.TableDTOs
{
    public class TableCreateDTO
    {
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
    }
}
