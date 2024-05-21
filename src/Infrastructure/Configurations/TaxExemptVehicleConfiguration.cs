using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class TaxExemptVehicleConfiguration : IEntityTypeConfiguration<TaxExemptVehicle>
{
    public void Configure(EntityTypeBuilder<TaxExemptVehicle> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.CityTaxRuleId)
            .IsRequired();

        builder.Property(x => x.VehicleType)
            .HasMaxLength(DataSchemaConstants.NameMaxLength)
            .IsRequired();
    }
}
