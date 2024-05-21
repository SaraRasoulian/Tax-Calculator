using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CityTaxRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MaximumTaxPerDay = table.Column<long>(type: "bigint", nullable: false),
                    SingleChargeDurationMinutes = table.Column<long>(type: "bigint", nullable: false),
                    IsHolidayTaxExempt = table.Column<bool>(type: "boolean", nullable: false),
                    IsDayBeforeHolidayTaxExempt = table.Column<bool>(type: "boolean", nullable: false),
                    IsWeekendTaxExempt = table.Column<bool>(type: "boolean", nullable: false),
                    IsJulyTaxExempt = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityTaxRules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityTaxRuleId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Holidays_CityTaxRules_CityTaxRuleId",
                        column: x => x.CityTaxRuleId,
                        principalTable: "CityTaxRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxAmounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityTaxRuleId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxAmounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxAmounts_CityTaxRules_CityTaxRuleId",
                        column: x => x.CityTaxRuleId,
                        principalTable: "CityTaxRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaxExemptVehicles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityTaxRuleId = table.Column<long>(type: "bigint", nullable: false),
                    VehicleType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxExemptVehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaxExemptVehicles_CityTaxRules_CityTaxRuleId",
                        column: x => x.CityTaxRuleId,
                        principalTable: "CityTaxRules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CityTaxRules",
                columns: new[] { "Id", "CityName", "IsDayBeforeHolidayTaxExempt", "IsHolidayTaxExempt", "IsJulyTaxExempt", "IsWeekendTaxExempt", "MaximumTaxPerDay", "SingleChargeDurationMinutes" },
                values: new object[] { 1L, "Gothenburg", true, true, true, true, 60L, 60L });

            migrationBuilder.InsertData(
                table: "Holidays",
                columns: new[] { "Id", "CityTaxRuleId", "Date", "Description" },
                values: new object[,]
                {
                    { 1L, 1L, new DateOnly(2013, 1, 1), "New year holiday" },
                    { 2L, 1L, new DateOnly(2013, 3, 29), "Good Friday" },
                    { 3L, 1L, new DateOnly(2013, 4, 1), "Easter Monday" },
                    { 4L, 1L, new DateOnly(2013, 5, 1), "Labour Day" },
                    { 5L, 1L, new DateOnly(2013, 5, 9), "Ascension Day" },
                    { 6L, 1L, new DateOnly(2013, 6, 6), "National Day" },
                    { 7L, 1L, new DateOnly(2013, 12, 25), "Christmas Day" },
                    { 8L, 1L, new DateOnly(2013, 12, 26), "Boxing Day" }
                });

            migrationBuilder.InsertData(
                table: "TaxAmounts",
                columns: new[] { "Id", "Amount", "CityTaxRuleId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1L, 0L, 1L, new TimeOnly(5, 59, 0), new TimeOnly(0, 0, 0) },
                    { 2L, 8L, 1L, new TimeOnly(6, 29, 0), new TimeOnly(6, 0, 0) },
                    { 3L, 13L, 1L, new TimeOnly(6, 59, 0), new TimeOnly(6, 30, 0) },
                    { 4L, 18L, 1L, new TimeOnly(7, 59, 0), new TimeOnly(7, 0, 0) },
                    { 5L, 13L, 1L, new TimeOnly(8, 29, 0), new TimeOnly(8, 0, 0) },
                    { 6L, 8L, 1L, new TimeOnly(14, 59, 0), new TimeOnly(8, 30, 0) },
                    { 7L, 13L, 1L, new TimeOnly(15, 29, 0), new TimeOnly(15, 0, 0) },
                    { 8L, 18L, 1L, new TimeOnly(16, 59, 0), new TimeOnly(15, 30, 0) },
                    { 9L, 13L, 1L, new TimeOnly(17, 59, 0), new TimeOnly(17, 0, 0) },
                    { 10L, 8L, 1L, new TimeOnly(18, 29, 0), new TimeOnly(18, 0, 0) },
                    { 11L, 0L, 1L, new TimeOnly(23, 59, 0), new TimeOnly(18, 30, 0) }
                });

            migrationBuilder.InsertData(
                table: "TaxExemptVehicles",
                columns: new[] { "Id", "CityTaxRuleId", "VehicleType" },
                values: new object[,]
                {
                    { 1L, 1L, "Emergency" },
                    { 2L, 1L, "Buss" },
                    { 3L, 1L, "Diplomat" },
                    { 4L, 1L, "Motorcycle" },
                    { 5L, 1L, "Military" },
                    { 6L, 1L, "Foreign" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityTaxRules_CityName",
                table: "CityTaxRules",
                column: "CityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CityTaxRuleId",
                table: "Holidays",
                column: "CityTaxRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxAmounts_CityTaxRuleId",
                table: "TaxAmounts",
                column: "CityTaxRuleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaxExemptVehicles_CityTaxRuleId",
                table: "TaxExemptVehicles",
                column: "CityTaxRuleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "TaxAmounts");

            migrationBuilder.DropTable(
                name: "TaxExemptVehicles");

            migrationBuilder.DropTable(
                name: "CityTaxRules");
        }
    }
}
