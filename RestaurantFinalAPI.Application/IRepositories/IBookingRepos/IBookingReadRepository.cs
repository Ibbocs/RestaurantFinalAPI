using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Application.IRepositories.IBookingRepos
{
    public interface IBookingReadRepository : IRepositoryRead<Booking>
    {
    }
    public interface IBookingWriteRepository : IRepositoryWrite<Booking>
    {
    }
}
