using RestaurantFinalAPI.Application.IRepositories;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.Common;
using RestaurantFinalAPI.Persistance.Concreates.Repositories;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.UnitOfWork
{
    public class UnitOfWork<TRepo> : IUnitOfWork<TRepo> where TRepo : BaseEntity
    {
        private readonly RestaurantFinalAPIContext restaurantContext;
        private IRepository<TRepo> myRepo;

        public UnitOfWork(RestaurantFinalAPIContext _restaurantContext)
        {
            restaurantContext = _restaurantContext;
        }

        public IRepository<TRepo> Repository
        {
            get
            {
                if (myRepo == null)
                {
                    myRepo = new RepositoryWrite<TRepo>(restaurantContext);
                }
                return myRepo;
            }
        }

        public void Dispose()
        {
            restaurantContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await restaurantContext.SaveChangesAsync();
            return result;
        }
    }
}
