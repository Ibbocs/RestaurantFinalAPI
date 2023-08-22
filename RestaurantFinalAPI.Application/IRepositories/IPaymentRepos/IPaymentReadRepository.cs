using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.IPaymentRepos
{
    public interface IPaymentReadRepository : IRepositoryRead<Payment>
    {
    }
    public interface IPaymentWriteRepository : IRepositoryWrite<Payment>
    {
    }
}
