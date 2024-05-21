using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaxExemptDateConfiguration : IEntityTypeConfiguration<Holiday>
{
    public void Configure(EntityTypeBuilder<Holiday> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CityTaxRuleId)
            .IsRequired();

        builder.Property(x => x.Date)
            .IsRequired();
    }
}
