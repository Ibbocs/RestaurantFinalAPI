using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Application.IRepositories.ITableRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.TableRepos
{
    //bu clasin                        bu uygulanmasi,        bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class TableReadRepository : RepositoryRead<Table>, ITableReadRepository
    {
        public TableReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                         bu uygulanmasi,        bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class TableWriteRepository : RepositoryWrite<Table>, ITableWriteRepository
    {
        public TableWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
