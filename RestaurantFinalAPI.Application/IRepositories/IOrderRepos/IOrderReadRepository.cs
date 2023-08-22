using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.IOrderRepos
{
    public interface IOrderReadRepository : IRepositoryRead<Order>
    {
    }
    public interface IOrderWriteRepository : IRepositoryWrite<Order>
    {
    }
}
