using Microsoft.AspNetCore.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndTime { get; set; }

        //todo created date ve updatedate qoyub ozumuz manuel deyer vere bilerik, ya da golbal filtirle

        public ICollection<Order> Orders { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }

    public class AppRole : IdentityRole<string>
    {

    }

    public class AppUserRoles : IdentityUserRole<string>
    {

    }
}
