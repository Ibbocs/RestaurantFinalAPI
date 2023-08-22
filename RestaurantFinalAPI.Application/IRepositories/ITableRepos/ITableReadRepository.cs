using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.ITableRepos
{
    public interface ITableReadRepository : IRepositoryRead<Table>
    {
    }
    public interface ITableWriteRepository : IRepositoryWrite<Table>
    {
    }
}
