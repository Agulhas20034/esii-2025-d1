using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class IDK2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5643), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5644), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5644) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5646), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5646), new DateTime(2025, 3, 21, 14, 54, 58, 159, DateTimeKind.Utc).AddTicks(5647) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(4121), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(5098), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(5424) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6081), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6082), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6082) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6085), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6085), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6086) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6089), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6090), new DateTime(2025, 3, 21, 12, 57, 50, 819, DateTimeKind.Utc).AddTicks(6090) });
        }
    }
}
