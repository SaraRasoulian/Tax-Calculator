using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.DbContexts;

public class TaxDBContext : DbContext
{
    public TaxDBContext(DbContextOptions<TaxDBContext> options) : base(options) { }

    public DbSet<CityTaxRule> CityTaxRules => Set<CityTaxRule>();
    public DbSet<TaxAmount> TaxAmounts => Set<TaxAmount>();
    public DbSet<TaxExemptVehicle> TaxExemptVehicles => Set<TaxExemptVehicle>();
    public DbSet<Holiday> Holidays => Set<Holiday>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Database Seeding

        #region CityTaxRule Seeds

        modelBuilder.Entity<CityTaxRule>().HasData(
            new CityTaxRule
            {
                Id = 1,
                CityName = "Gothenburg",
                MaximumTaxPerDay = 60,
                SingleChargeDurationMinutes = 60,
                IsHolidayTaxExempt = true,
                IsDayBeforeHolidayTaxExempt = true,
                IsWeekendTaxExempt = true,
                IsJulyTaxExempt = true
            }
            );

        #endregion

        #region TaxAmount Seeds

        modelBuilder.Entity<TaxAmount>().HasData(
            new TaxAmount
            {
                Id = 1,
                CityTaxRuleId = 1,
                StartTime = new TimeOnly(hour: 00, minute: 00),
                EndTime = new TimeOnly(hour: 05, minute: 59),
                Amount = 0
            }
            );

        modelBuilder.Entity<TaxAmount>().HasData(
            new TaxAmount
            {
                Id = 2,
                CityTaxRuleId = 1,
                StartTime = new TimeOnly(hour: 06, minute: 00),
                EndTime = new TimeOnly(hour: 06, minute: 29),
                Amount = 8
            }
            );

        modelBuilder.Entity<TaxAmount>().HasData(
            new TaxAmount
            {
                Id = 3,
                CityTaxRuleId = 1,
                StartTime = new TimeOnly(hour: 06, minute: 30),
                EndTime = new TimeOnly(hour: 06, minute: 59),
                Amount = 13
            }
            );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 4,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 07, minute: 00),
               EndTime = new TimeOnly(hour: 07, minute: 59),
               Amount = 18
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 5,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 08, minute: 00),
               EndTime = new TimeOnly(hour: 08, minute: 29),
               Amount = 13
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 6,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 08, minute: 30),
               EndTime = new TimeOnly(hour: 14, minute: 59),
               Amount = 8
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 7,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 15, minute: 00),
               EndTime = new TimeOnly(hour: 15, minute: 29),
               Amount = 13
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 8,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 15, minute: 30),
               EndTime = new TimeOnly(hour: 16, minute: 59),
               Amount = 18
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
           new TaxAmount
           {
               Id = 9,
               CityTaxRuleId = 1,
               StartTime = new TimeOnly(hour: 17, minute: 00),
               EndTime = new TimeOnly(hour: 17, minute: 59),
               Amount = 13
           }
           );

        modelBuilder.Entity<TaxAmount>().HasData(
        new TaxAmount
        {
            Id = 10,
            CityTaxRuleId = 1,
            StartTime = new TimeOnly(hour: 18, minute: 00),
            EndTime = new TimeOnly(hour: 18, minute: 29),
            Amount = 8
        }
        );

        modelBuilder.Entity<TaxAmount>().HasData(
        new TaxAmount
        {
            Id = 11,
            CityTaxRuleId = 1,
            StartTime = new TimeOnly(hour: 18, minute: 30),
            EndTime = new TimeOnly(hour: 23, minute: 59),
            Amount = 0
        }
        );

        #endregion

        #region TaxExemptVehicle Seeds

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 1,
                CityTaxRuleId = 1,
                VehicleType = "Emergency"
            }
            );

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 2,
                CityTaxRuleId = 1,
                VehicleType = "Buss"
            }
            );

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 3,
                CityTaxRuleId = 1,
                VehicleType = "Diplomat"
            }
            );

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 4,
                CityTaxRuleId = 1,
                VehicleType = "Motorcycle"
            }
            );

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 5,
                CityTaxRuleId = 1,
                VehicleType = "Military"
            }
            );

        modelBuilder.Entity<TaxExemptVehicle>().HasData(
            new TaxExemptVehicle
            {
                Id = 6,
                CityTaxRuleId = 1,
                VehicleType = "Foreign"
            }
            );

        #endregion

        #region Holiday Seeds

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 1,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 1, 1),
                Description = "New year holiday"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 2,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 3, 29),
                Description = "Good Friday"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 3,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 4, 1),
                Description = "Easter Monday"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 4,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 5, 1),
                Description = "Labour Day"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 5,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 5, 9),
                Description = "Ascension Day"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 6,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 6, 6),
                Description = "National Day"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 7,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 12, 25),
                Description = "Christmas Day"
            }
            );

        modelBuilder.Entity<Holiday>().HasData(
            new Holiday
            {
                Id = 8,
                CityTaxRuleId = 1,
                Date = new DateOnly(2013, 12, 26),
                Description = "Boxing Day"
            }
            );

        #endregion
    }
}