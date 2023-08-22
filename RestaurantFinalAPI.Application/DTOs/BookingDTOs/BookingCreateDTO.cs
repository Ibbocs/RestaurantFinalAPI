using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.DTOs.BookingDTOs
{
    public class BookingCreateDTO
    {
        public DateTime StartDate { get; set; }
        public string UserId { get; set; }
        public string TableId { get; set; }
    }
}
