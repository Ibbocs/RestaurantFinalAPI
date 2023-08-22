using RestaurantFinalAPI.Domain.Entities.Common;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Domain.Entities.RestaurantDBContext
{
    public class Order : BaseEntity, ISoftDelete
    {
        //public Guid OrderId { get; set;}
        public string Status { get; set; }
        public string Note { get; set; }

        public ICollection<OrderDetalis> OrderDetails { get; set; }

        public AppUser User { get; set; }
        public string UserId { get; set; }

        public Table Table { get; set; }
        public Guid TableId { get; set; }

        public Payment Payment { get; set; }

    }

    public class OrderDetalis
    {
        public Guid OrderId { get; set; }
        public Guid MenuItemId { get; set; }

        public MenuItem MenuItem { get; set; }
        public Order Order { get; set; }

    }

    public class Booking : BaseEntity
    {
        //public Guid BookingId { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsConfrimed { get; set; }

        public AppUser User { get; set; }
        public string UserId { get; set; }

        public Table Table { get; set; }
        public Guid TableId { get; set; }

    }

    public class MenuItem : BaseEntity, ISoftDelete
    {
        //public Guid MenuItemId { get; set;}
        public string MenuItemName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }

        public MenuItemCategory MenuItemCategory { get; set; }
        public Guid MenuItemCategoryId { get; set; }

        public ICollection<OrderDetalis> OrderDetails { get; set; }
    }

    public class MenuItemCategory : BaseEntity, ISoftDelete
    {
        //public Guid MenuItemCategoryId { get; set;}
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<MenuItem> MenuItems { get; }
    }

    public class Payment : BaseEntity, ISoftDelete
    {
        //public Guid PaymentId { get; set; }
        public string Status { get; set; }
        public int Amount { get; set; }
        public string PaymentMethod { get; set; }

        public Order Order { get; set; }
        public Guid OrderId { get; set; }

        public AppUser User { get; set; }
        public string UserId { get; set; }
    }

    public class Table : BaseEntity, ISoftDelete
    {
        //public Guid TableId { get; set;}
        public string TableName { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public bool IsOccupied { get; set; }
        public bool IsReserved { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

    public class Customer : BaseEntity
    {
        //public Guid UserId { get; set;}
        //public string UserNickName { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Adress { get; set; }
        //public string PhoneNumber { get; set; }

        //public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
