using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Configurations.EntityConfigurations
{
    public class TableConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasQueryFilter(t => t.IsDeleted == false);

            builder.Property(t => t.Capacity)
                .IsRequired();

            builder.Property(t => t.TableName)
                .IsRequired()
                .HasMaxLength(51);

            builder.Property(t => t.Description)
                .HasMaxLength(201);
        }
    }
}
