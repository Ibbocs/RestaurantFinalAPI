using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions
{                                         //bu klasdan torenende exception olur
    public class UserCreateFaileedException : Exception
    {
        public UserCreateFaileedException() :base("An Error When Creating A User")
        {
        }

        public UserCreateFaileedException(string? message) : base(message)
        {
        }

        public UserCreateFaileedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
