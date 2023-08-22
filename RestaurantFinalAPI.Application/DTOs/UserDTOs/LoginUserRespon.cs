using RestaurantFinalAPI.Application.DTOs.TokenDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.DTOs.UserDTOs
{
    //todo bunu isletmirik, silek belek-rftLogin de isletdim 
    //singile responsibilitye gore boldum
    public class LoginUserRespon
    {
    }

    public class LoginUserSuccessedResponse : LoginUserRespon
    {
        public TokenDTO TokenDTO { get; set; }
    }

    public class LoginUserErrorResponse : LoginUserRespon
    {
        public string ErrorMessage { get; set; }
        //public AuthenticationErrorException AuthenticationError { get; set; }
    }
}
