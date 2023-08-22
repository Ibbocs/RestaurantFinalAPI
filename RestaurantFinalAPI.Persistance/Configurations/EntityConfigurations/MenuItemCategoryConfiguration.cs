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
    public class MenuItemCategoryConfiguration : IEntityTypeConfiguration<MenuItemCategory>
    {
        public void Configure(EntityTypeBuilder<MenuItemCategory> builder)
        {
            builder.Property(m => m.CategoryName)
                .IsRequired()
                .HasMaxLength(51);

            builder.Property(m => m.Description)
                .HasMaxLength(201);

            builder.HasQueryFilter(t => t.IsDeleted == false);
        }
    }
}
