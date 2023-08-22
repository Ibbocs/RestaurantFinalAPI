using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.IMenuItemCategoryRepos
{
    public interface IMenuItemCategoryReadRepository : IRepositoryRead<MenuItemCategory>
    {
    }
    public interface IMenuItemCategoryWriteRepository : IRepositoryWrite<MenuItemCategory>
    {
    }
}
