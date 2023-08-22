using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.IMenuItemRepos
{
    public interface IMenuItemReadRepository : IRepositoryRead<MenuItem>
    {
    }
    public interface IMenuItemWriteRepository : IRepositoryWrite<MenuItem>
    {
    }
}
