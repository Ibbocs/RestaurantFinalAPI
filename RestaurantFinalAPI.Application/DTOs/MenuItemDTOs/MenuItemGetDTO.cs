using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.DTOs.MenuItemDTOs
{
    public class MenuItemGetDTO
    {
        public Guid Id { get; set; }
        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public string MenuItemCategoryId { get; set; }
        public string MenuItemCategoryName { get; set; }
    }
}
