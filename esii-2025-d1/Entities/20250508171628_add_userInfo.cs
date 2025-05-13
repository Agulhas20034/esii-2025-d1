using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class add_userInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserInfos");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserInfos",
                newName: "UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2347), new DateTime(2025, 5, 15, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2012), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(1824), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2495) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2793), new DateTime(2025, 5, 14, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2792), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2791), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2794) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2801), new DateTime(2025, 5, 11, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2800), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(2801) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(4387), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(4538) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(4855), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(4855) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 526, DateTimeKind.Utc).AddTicks(6468), new DateTime(2025, 5, 8, 17, 16, 27, 526, DateTimeKind.Utc).AddTicks(6990), new DateTime(2025, 5, 8, 17, 16, 27, 526, DateTimeKind.Utc).AddTicks(7155) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(1481), new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(1626) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(1924), new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(1925) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(7884), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(8035) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(8354), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(8354) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(8409), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(9592), new DateTime(2025, 5, 8, 17, 16, 27, 529, DateTimeKind.Utc).AddTicks(9735) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(35), new DateTime(2025, 5, 8, 17, 16, 27, 530, DateTimeKind.Utc).AddTicks(36) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfos",
                table: "UserInfos");

            migrationBuilder.RenameTable(
                name: "UserInfos",
                newName: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Password");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2362), new DateTime(2025, 5, 9, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(1975), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(1795), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2518) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2855), new DateTime(2025, 5, 8, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2854), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2853), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2855) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2862), new DateTime(2025, 5, 5, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2861), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2861), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(2862) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(4377), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(4525) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(4821), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(4821) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 69, DateTimeKind.Utc).AddTicks(6313), new DateTime(2025, 5, 2, 12, 45, 46, 69, DateTimeKind.Utc).AddTicks(6869), new DateTime(2025, 5, 2, 12, 45, 46, 69, DateTimeKind.Utc).AddTicks(7079) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(1542), new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(1682) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(1970), new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(1970) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(7666), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(8410), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(8411) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(8415), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(8415) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(9601), new DateTime(2025, 5, 2, 12, 45, 46, 72, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(148), new DateTime(2025, 5, 2, 12, 45, 46, 73, DateTimeKind.Utc).AddTicks(148) });
        }
    }
}
