using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PocDDD.Domain.Entities;

namespace PocDDD.Infra.Data.EntityConfigurations
{
    public class OrderConfiguraton : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.OrderId);
            builder.HasOne(order => order.User)
                .WithMany(user => user.Orders)
                .HasForeignKey(order => order.OrderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}