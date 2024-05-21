using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaxAmountConfiguration : IEntityTypeConfiguration<TaxAmount>
{
    public void Configure(EntityTypeBuilder<TaxAmount> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CityTaxRuleId)
            .IsRequired();

        builder.Property(x => x.StartTime)
            .IsRequired();

        builder.Property(x => x.EndTime)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired();
    }
}
