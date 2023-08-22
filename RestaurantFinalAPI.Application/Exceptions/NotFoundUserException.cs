using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions
{
    public class NotFoundUserException2 : Exception
    {
        public NotFoundUserException2() : base("Username, Email Or Password Incorrect")
        {
        }

        public NotFoundUserException2(string? message) : base(message)
        {
        }

        public NotFoundUserException2(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
