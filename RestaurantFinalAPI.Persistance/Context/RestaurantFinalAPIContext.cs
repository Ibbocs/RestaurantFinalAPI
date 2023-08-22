using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Domain.Entities.Common;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Domain.Helper;
using RestaurantFinalAPI.Persistance.Configurations.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Context
{
    public class RestaurantFinalAPIContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public RestaurantFinalAPIContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
        //public DbSet<MenuItemCategory> MenuItemCategories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderDetalis> OrderDetalis { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Global Query Filtire
            //modelBuilder.Entity<Table>().HasQueryFilter(t => t.IsDeleted == false);
            //modelBuilder.Entity<MenuItemCategory>().HasQueryFilter(t => t.IsDeleted == false);
            //modelBuilder.Entity<MenuItem>().HasQueryFilter(t => t.IsDeleted == false);

            base.OnModelCreating(modelBuilder);

            var guidAdmin = Guid.NewGuid().ToString();
            var guidUser = Guid.NewGuid().ToString();
            var guidAdminCreat = Guid.NewGuid().ToString();
            // Role Seed Data
            modelBuilder.Entity<AppRole>().HasData(
                new AppRole { Id = guidAdmin, Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = guidUser, Name = "User", NormalizedName = "USER" }
            );

            // User Seed Data
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                Id = guidAdminCreat,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "default",
                LastName = "default",
                BirthDate = DateTime.UtcNow,
                SecurityStamp = Guid.NewGuid().ToString(),
                //ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };

            user.PasswordHash = hasher.HashPassword(user, "Admin!23");

            modelBuilder.Entity<AppUser>().HasData(user);

            // User - Role Relationship Seed Data
            modelBuilder.Entity<AppUserRoles>().HasData(
                new AppUserRoles { UserId = guidAdminCreat, RoleId = guidAdmin } // Admin user is assigned the Admin role
            );



            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RestaurantFinalAPIContext).Assembly);//queryfiltirde en zindan bu isledi
            //modelBuilder.ApplyConfiguration(new BookingConfiguration());
            //modelBuilder.ApplyConfiguration(new MenuItemCategoryConfiguration());

            //MenuItemCategory-MenuItem O-M relation
            //modelBuilder.Entity<MenuItem>()
            //    .HasOne<MenuItemCategory>(mi => mi.MenuItemCategory)
            //    .WithMany(mic => mic.MenuItems)
            //    .HasForeignKey(mi => mi.MenuItemCategoryId);

            //Composit Key For OrderDetalis
            modelBuilder.Entity<OrderDetalis>()
                .HasKey(od => new { od.OrderId, od.MenuItemId });

            //Order-MenuItem M-M Relations
            modelBuilder.Entity<OrderDetalis>()
                .HasOne<Order>(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderDetalis>()
                .HasOne<MenuItem>(od => od.MenuItem)
                .WithMany(mi => mi.OrderDetails)
                .HasForeignKey(od => od.MenuItemId);

            //Order-User M-O Relation
            modelBuilder.Entity<Order>()
                .HasOne<AppUser>(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            //Order-Table M-O Relation
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableId);

            //Order-Payment O-O Relation
            modelBuilder.Entity<Order>()
                .HasOne<Payment>(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);

            //Payment-User M-O Relation
            modelBuilder.Entity<Payment>()
                .HasOne<AppUser>(p => p.User)
                .WithMany(u => u.Payments)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            //Booking-User M-O Relation
            //modelBuilder.Entity<Booking>()
            //    .HasOne<AppUser>(b => b.User)
            //    .WithMany(u => u.Bookings)
            //    .HasForeignKey(b => b.UserId);

            //Table-Booking O-M Relation
            //modelBuilder.Entity<Booking>()
            //    .HasOne<Table>(b => b.Table)
            //    .WithMany(t => t.Bookings)
            //    .HasForeignKey(b => b.TableId);

           // base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                if (data.State == EntityState.Added)
                {
                    data.Entity.CreateDate = DateTime.Now;
                }
                else if (data.State == EntityState.Modified)
                {
                    data.Entity.UpdateDate = DateTime.Now;
                }
                else if (data.State == EntityState.Deleted && data.Entity is ISoftDelete)//todo burda data ola da biler
                {
                    data.State = EntityState.Modified;
                    data.Entity.IsDeleted = true;
                }
                ////discard ile geri nese dondermirik
                //_ = data.State switch
                //{
                //    EntityState.Added => data.Entity.CreateDate = DateTime.Now,
                //    EntityState.Modified => data.Entity.UpdateDate = DateTime.Now,
                //    _ => DateTime.Now
                //    //EntityState.Deleted => data.Entity.IsDeleted = false
                //};
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
