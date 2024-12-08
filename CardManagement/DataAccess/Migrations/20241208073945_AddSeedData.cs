using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CardTypes",
                columns: new[] { "Id", "CreatedBy", "CreatedTime", "Name", "UpdatedBy", "UpdatedTime" },
                values: new object[,]
                {
                    { new Guid("3b3b2476-fc8a-4f17-b9b6-5d3ec1ae3f4a"), "SeedData", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Water", null, null },
                    { new Guid("4e6a8a88-8c3f-45f6-9dd8-26c78a7f5172"), "SeedData", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Air", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CardTypes",
                keyColumn: "Id",
                keyValue: new Guid("3b3b2476-fc8a-4f17-b9b6-5d3ec1ae3f4a"));

            migrationBuilder.DeleteData(
                table: "CardTypes",
                keyColumn: "Id",
                keyValue: new Guid("4e6a8a88-8c3f-45f6-9dd8-26c78a7f5172"));
        }
    }
}
