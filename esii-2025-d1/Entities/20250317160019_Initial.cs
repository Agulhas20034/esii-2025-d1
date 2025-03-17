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
                    TestNumber = table.Column<float>(type: "real", nullable: false),
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
                    { 1, new DateTime(2025, 3, 17, 16, 0, 19, 298, DateTimeKind.Utc).AddTicks(9785), false, "Joms", 2.5f, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(371), null, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(543) },
                    { 2, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1002), true, "Joms2", 7.5f, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1002), null, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1003) },
                    { 3, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1008), true, "Joms3", 10f, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1009), null, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1009) },
                    { 4, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1013), false, "Joms4", 62f, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1014), null, new DateTime(2025, 3, 17, 16, 0, 19, 299, DateTimeKind.Utc).AddTicks(1014) }
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
