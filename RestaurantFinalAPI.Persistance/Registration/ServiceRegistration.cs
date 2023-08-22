using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.AuthenticationServices;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance.IUserServices;
using RestaurantFinalAPI.Application.IRepositories.IBookingRepos;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemCategoryRepos;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemRepos;
using RestaurantFinalAPI.Application.IRepositories.IOrderRepos;
using RestaurantFinalAPI.Application.IRepositories.IPaymentRepos;
using RestaurantFinalAPI.Application.IRepositories.ITableRepos;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.BookingRepos;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.MenuItemCategoryRepos;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.MenuItemRepos;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.OrderRepos;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.PaymentRepos;
using RestaurantFinalAPI.Persistance.Concreates.Repositories.TableRepos;
using RestaurantFinalAPI.Persistance.Configurations;
using RestaurantFinalAPI.Persistance.Context;
using RestaurantFinalAPI.Persistance.Implementation.Services;
using RestaurantFinalAPI.Persistance.Implementation.Services.UserServices;
using RestaurantFinalAPI.Persistance.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Registration
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            //sql connection and context registration
            //bu islesin deye Configuration clasinda namespaceden .Configuration sildim
            services.AddDbContext<RestaurantFinalAPIContext>(options => options.UseSqlServer(Configuration.ConnectionStringForRestaurantApiDB));

            //todo identitinin oz validationunu silmek, GY 38bolum 53:50
            //Identity Registration
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<RestaurantFinalAPIContext>().AddDefaultTokenProviders();

            //Repository Registrations //todo controllorda task ile yaz async methodlari yoxsa lifetime xetasi verer
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IBookingReadRepository, BookingReadRepository>();
            services.AddScoped<IBookingWriteRepository, BookingWriteRepository>();

            services.AddScoped<IMenuItemReadRepository, MenuItemReadRepository>();
            services.AddScoped<IMenuItemWriteRepository, MenuItemWriteRepository>();

            services.AddScoped<IMenuItemCategoryReadRepository, MenuItemCategoryReadRepository>();
            services.AddScoped<IMenuItemCategoryWriteRepository, MenuItemCategoryWriteRepository>();

            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();

            services.AddScoped<ITableReadRepository, TableReadRepository>();
            services.AddScoped<ITableWriteRepository, TableWriteRepository>();

            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAuthoService, AuthService>();
            services.AddScoped<IExternalAuthentications, AuthService>();
            services.AddScoped<IInternalAuthentications, AuthService>();

            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IMenuItemCategoryService, MenuItemCategoryService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            services.AddScoped<IBookingService, BookingService>();



        }
    }
}

