using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Models.ViewModels.UserVMS
{
    public class VMUserCreate
    {
        //todo bu model deyisecek, hetta dto isledek bundansa
        public string UserNickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
