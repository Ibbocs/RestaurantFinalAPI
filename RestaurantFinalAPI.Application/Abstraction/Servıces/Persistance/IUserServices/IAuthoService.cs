using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices
{
    public interface IAuthoService:IInternalAuthentications,IExternalAuthentications
    {
    }
}
