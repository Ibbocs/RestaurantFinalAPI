using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.DTOs.BookingDTOs
{
    public class BookingGetDTO
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsConfrimed { get; set; }

        public string UserId { get; set; }

        public Guid TableId { get; set; }
    }
}
