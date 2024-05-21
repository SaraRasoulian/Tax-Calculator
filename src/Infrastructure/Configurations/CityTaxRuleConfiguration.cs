using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Configurations;

public class CityTaxRuleConfiguration : IEntityTypeConfiguration<CityTaxRule>
{
    public void Configure(EntityTypeBuilder<CityTaxRule> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CityName)
            .HasMaxLength(DataSchemaConstants.NameMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.CityName)
            .IsUnique();
    }
}
