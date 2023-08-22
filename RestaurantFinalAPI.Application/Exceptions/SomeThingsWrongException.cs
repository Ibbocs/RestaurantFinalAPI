using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Exceptions
{
    public class SomeThingsWrongException : Exception
    {
        public SomeThingsWrongException() : base("Something went wrong.")
        {
        }

        public SomeThingsWrongException(string? message) : base(message)
        {
        }

        public SomeThingsWrongException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
