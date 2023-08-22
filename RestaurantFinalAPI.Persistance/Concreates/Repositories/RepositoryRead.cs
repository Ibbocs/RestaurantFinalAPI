using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.Exceptions.CommonExceptions;
using RestaurantFinalAPI.Application.IRepositories;
using RestaurantFinalAPI.Domain.Entities.Common;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories
{
    //ById ile sorgulamada id gormek ucun marker pattern isledirem. BaseEntity den id cixardim deye FindAsync ile de yazdim
    public class RepositoryRead<T> : IRepositoryRead<T> where T : /*class*/ BaseEntity
    {
        private readonly RestaurantFinalAPIContext restaurantAPIContext;

        public RepositoryRead(RestaurantFinalAPIContext _restaurantAPIContext)
        {
            this.restaurantAPIContext = _restaurantAPIContext;
        }

        //...bu conextde guya T tipinde entiti oldugnu (genericde) bildirmek ucundu
        public DbSet<T> Table => restaurantAPIContext.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            //todo isdelete burda icinde de yoxlaya biler de
            //query halinda table aliram
            var query = Table.AsQueryable();
            //tracink baglanma serti
            if (!tracking) query = query.AsNoTracking();
            return query;
        }


        public async Task<T> GetByIdAsync(string id, bool tracking = true, bool isDeleted =false)
        //=>Table.FirstOrDefaultAsync(data=> data.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            //todo include edende burda nece edecem? yoxsa bu mehodu isletdiyim yerde elemeym onu
            bool checkIdFormat = Guid.TryParse(id, out Guid guid); //todo burda yaranan guid ver dana methodlara asagida
            if (checkIdFormat)
            {
                var query = Table.AsQueryable();
                if (!tracking) query = query.AsNoTracking();

                if (isDeleted) return await query.IgnoreQueryFilters().FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));

                return await query.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            }
            else
                throw new InvalidIdFormatException(id);


        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true, bool isDeleted = false)
        {
            var query = Table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();

            if (isDeleted) return await query.IgnoreQueryFilters().FirstOrDefaultAsync(expression);
            
            return await query.FirstOrDefaultAsync(expression);
        }

        public IQueryable<T> GetWithFiltir(Expression<Func<T, bool>> expression, bool tracking = true, bool isDeleted = false)
        {
            var query = Table.Where(expression);
            if (!tracking) query = query.AsNoTracking();

            if(isDeleted) return query.IgnoreQueryFilters();

            return query;
        }
    }

    public class RepositoryWrite<T> : IRepositoryWrite<T> where T : /*class*/ BaseEntity
    {
        private readonly RestaurantFinalAPIContext restaurantAPIContext;

        public RepositoryWrite(RestaurantFinalAPIContext _restaurantAPIContext)
        {
            restaurantAPIContext = _restaurantAPIContext;
        }

        public DbSet<T> Table => restaurantAPIContext.Set<T>();

        public async Task<bool> AddAsync(T data)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(data);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(ICollection<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }

        public bool Remove(T data)
        {
            EntityEntry<T> entityEntry = Table.Remove(data);
            return entityEntry.State == EntityState.Deleted;
        }

        public async Task<bool> Remove(string id)
        {
            bool checkIdFormat = Guid.TryParse(id, out Guid guid);
            if (checkIdFormat)
            {
                T data = await Table.FindAsync(Guid.Parse(id));
                return Remove(data);
            }
            else
                throw new InvalidIdFormatException(id);

        }

        public bool RemoveRange(ICollection<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        //todo updateler async deyil
        public bool Update(T data)
        {
            EntityEntry<T> entityEntry = Table.Update(data);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool UpdateRange(ICollection<T> datas)
        {
            Table.UpdateRange(datas);
            return true;
        }

        public async Task<int> SaveChangesAsync()
        => await restaurantAPIContext.SaveChangesAsync();

    }
}
