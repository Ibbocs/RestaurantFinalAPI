using RestaurantFinalAPI.Application.IRepositories.IBookingRepos;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.MenuItemRepos
{
    //bu clasin                           bu uygulanmasi,          bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class MenuItemReadRepository : RepositoryRead<MenuItem>, IMenuItemReadRepository
    {
        //RepositoryRead'nin ctoruna gozlediyi context veririk
        public MenuItemReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                            bu uygulanmasi,           bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class MenuItemWriteRepository : RepositoryWrite<MenuItem>, IMenuItemWriteRepository
    {
        //RepositoryWrite'nin ctoruna gozlediyi context veririk
        public MenuItemWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
