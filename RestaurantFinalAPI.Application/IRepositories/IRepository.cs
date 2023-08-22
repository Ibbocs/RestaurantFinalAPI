using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories
{
    public interface IRepository<T> where T : /*class*/ BaseEntity
    {
        DbSet<T> Table { get; }
    }

    public interface IRepositoryRead<T> : IRepository<T> where T : /*class*/ BaseEntity
    {
        IQueryable<T> GetAll(bool tracking = true);
        IQueryable<T> GetWithFiltir(Expression<Func<T, bool>> expression, bool tracking = true, bool isDeleted = false);
        Task<T> GetByIdAsync(string id, bool tracking = true, bool isDeleted = false);
        //serte gore tek data
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true, bool isDeleted = false);
    }

    public interface IRepositoryWrite<T> : IRepository<T> where T : /*class*/ BaseEntity
    {
        Task<bool> AddAsync(T data);
        Task<bool> AddRangeAsync(ICollection<T> datas);

        bool Remove(T data);
        Task<bool> Remove(string id);
        bool RemoveRange(ICollection<T> datas);

        bool Update(T data);
        bool UpdateRange(ICollection<T> datas);

        Task<int> SaveChangesAsync();

    }
}
