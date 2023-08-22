using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.OrderRepos
{
    //bu clasin                        bu uygulanmasi,        bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class OrderReadRepository : RepositoryRead<Order>, IOrderReadRepository
    {
        public OrderReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                         bu uygulanmasi,        bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class OrderWriteRepository : RepositoryWrite<Order>, IOrderWriteRepository
    {
        public OrderWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
