using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Configurations.EntityConfigurations
{
    public class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasQueryFilter(t => t.IsDeleted == false);

            builder.Property(m => m.Price)
                .IsRequired();

            builder.Property(m => m.Description)
                .HasMaxLength(201);

            builder.Property(m => m.MenuItemName)
                .IsRequired()
                .HasMaxLength(51);

            builder.Property(m=>m.MenuItemCategoryId) 
                .IsRequired();

            //MenuItemCategory-MenuItem O-M relation
            builder
                .HasOne<MenuItemCategory>(mi => mi.MenuItemCategory)
                .WithMany(mic => mic.MenuItems)
                .HasForeignKey(mi => mi.MenuItemCategoryId);
        }
    }
}
