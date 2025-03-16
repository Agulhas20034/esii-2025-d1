using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Joms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Label = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    TestNumber = table.Column<float>(type: "real", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Joms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Joms",
                columns: new[] { "Id", "Date", "IsDone", "Label", "TestNumber", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(1754), false, "Joms", 2.5f, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(2512), null, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(2672) },
                    { 2, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(3006), true, "Joms2", 7.5f, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(3007), null, new DateTime(2025, 3, 15, 19, 57, 8, 809, DateTimeKind.Utc).AddTicks(3008) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Joms");
        }
    }
}
