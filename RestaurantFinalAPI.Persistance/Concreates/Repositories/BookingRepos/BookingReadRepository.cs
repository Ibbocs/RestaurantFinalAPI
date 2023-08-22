using RestaurantFinalAPI.Application.IRepositories.IBookingRepos;
using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.BookingRepos
{
    //bu clasin                          bu uygulanmasi,          bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class BookingReadRepository : RepositoryRead<Booking>, IBookingReadRepository
    {
        //RepositoryRead'nin ctoruna gozlediyi context veririk
        public BookingReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                           bu uygulanmasi,           bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class BookingWriteRepository : RepositoryWrite<Booking>, IBookingWriteRepository
    {
        //RepositoryWrite'nin ctoruna gozlediyi context veririk
        public BookingWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
