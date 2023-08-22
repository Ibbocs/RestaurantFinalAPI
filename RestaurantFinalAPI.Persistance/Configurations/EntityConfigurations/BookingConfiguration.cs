using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Configurations.EntityConfigurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(b => b.StartDate)
                .IsRequired();

            builder.Property(b => b.UserId)
                .IsRequired();//guid tipinde tuturam deye length lazim deyil

            builder.Property(b => b.TableId)
                .IsRequired();

            //Booking-User M-O Relation
            builder
                .HasOne<AppUser>(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId);

            //Table-Booking O-M Relation
            builder
                .HasOne<Table>(b => b.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TableId);

        }
    }
}
