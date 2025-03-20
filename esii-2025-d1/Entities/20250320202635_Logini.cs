using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class Logini : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    entity_id = table.Column<int>(type: "integer", nullable: true),
                    entity_name = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 17, 16, 0, 19, 298, DateTimeKind.Utc).AddTicks(9785), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(371), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(543) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1002), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1002), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1003) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1008), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1009), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1009) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1013), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1014), new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1014) });
        }
    }
}
