using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Models.ResponseModels
{
    public class Response<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
    }

    //public class SuccessedResponse<T>:Response<T>
    //{

    //}

    //public class ErrorResponse<T>:Response<T>
    //{
    //    public int ErrorCode { get; set; }
    //    //public T Data { get; set; }
    //}


}
