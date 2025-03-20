using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class logdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 33, 47, 186, DateTimeKind.Utc).AddTicks(9106), new DateTime(2025, 3, 20, 20, 33, 47, 186, DateTimeKind.Utc).AddTicks(9631), new DateTime(2025, 3, 20, 20, 33, 47, 186, DateTimeKind.Utc).AddTicks(9809) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(140), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(141), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(141) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(143), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(144), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(144) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(146), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(147), new DateTime(2025, 3, 20, 20, 33, 47, 187, DateTimeKind.Utc).AddTicks(147) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(1537), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(2601), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(2947) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3636), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3637), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3638) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3645), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3646), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3647) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3654), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3655), new DateTime(2025, 3, 20, 20, 26, 34, 698, DateTimeKind.Utc).AddTicks(3656) });
        }
    }
}
