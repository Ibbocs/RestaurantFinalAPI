using RestaurantFinalAPI.Application.IRepositories.IMenuItemCategoryRepos;
using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.MenuItemCategoryRepos
{
    //bu clasin                                   bu uygulanmasi,                   bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class MenuItemCategoryReadRepository : RepositoryRead<MenuItemCategory>, IMenuItemCategoryReadRepository
    {
        public MenuItemCategoryReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                                    bu uygulanmasi,                   bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class MenuItemCategoryWriteRepository : RepositoryWrite<MenuItemCategory>, IMenuItemCategoryWriteRepository
    {
        public MenuItemCategoryWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
