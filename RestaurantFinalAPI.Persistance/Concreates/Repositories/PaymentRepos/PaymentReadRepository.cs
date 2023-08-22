using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Application.IRepositories.IPaymentRepos;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Concreates.Repositories.PaymentRepos
{
    //bu clasin                          bu uygulanmasi,          bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class PaymentReadRepository : RepositoryRead<Payment>, IPaymentReadRepository
    {
        public PaymentReadRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }

    //bu clasin                          bu uygulanmasi,           bu ise abstractionu, yeni ioc'de register ucun imzasidir.
    //2si bir birine baglidir, kilitdir sanki
    public class PaymentWriteRepository : RepositoryWrite<Payment>, IPaymentWriteRepository
    {
        public PaymentWriteRepository(RestaurantFinalAPIContext _restaurantAPIContext) : base(_restaurantAPIContext)
        {
        }
    }
}
