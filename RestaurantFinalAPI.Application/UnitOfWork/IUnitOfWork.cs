using RestaurantFinalAPI.Application.IRepositories;
using RestaurantFinalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.UnitOfWork
{
    public interface IUnitOfWork<TRepo> : IDisposable where TRepo : BaseEntity
    {
        public IRepository<TRepo> Repository { get; }
        Task<int> SaveChangesAsync();
    }
}
