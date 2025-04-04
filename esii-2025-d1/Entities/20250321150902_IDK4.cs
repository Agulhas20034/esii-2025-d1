using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class IDK4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 3, 21, 12, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(4724), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5231), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5368) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5641), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5641), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5642) });

            migrationBuilder.InsertData(
                table: "Joms",
                columns: new[] { "Id", "Date", "IsDone", "Label", "TestNumber", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { 3, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5643), true, "Joms3", 10f, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5644), null, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5644) },
                    { 4, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5646), false, "Joms4", 62f, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5646), null, new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5647) }
                });
        }
    }
}
