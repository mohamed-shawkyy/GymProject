using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gym.Models;

namespace Gym.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(p => p.UpdatedAt).HasDefaultValueSql("GETDATE()");
            builder.ToTable(p=>
            { 
                p.HasCheckConstraint("DurationDays_Constraint", "DurationDays between 1 and 365");
            });   

        }

    }
}
