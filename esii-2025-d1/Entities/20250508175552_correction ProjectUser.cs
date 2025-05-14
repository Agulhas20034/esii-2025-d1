using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class correctionProjectUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ProjectUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "InviterId",
                table: "ProjectUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(993), new DateTime(2025, 5, 15, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(665), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(485), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1143) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1450), new DateTime(2025, 5, 14, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1449), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1449), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1451) });

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "EndDate", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1457), new DateTime(2025, 5, 11, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1456), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1456), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(1457) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(3054), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(3211) });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(3514), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(3514) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 552, DateTimeKind.Utc).AddTicks(4500), new DateTime(2025, 5, 8, 17, 55, 51, 552, DateTimeKind.Utc).AddTicks(5035), new DateTime(2025, 5, 8, 17, 55, 51, 552, DateTimeKind.Utc).AddTicks(5202) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 556, DateTimeKind.Utc).AddTicks(94), new DateTime(2025, 5, 8, 17, 55, 51, 556, DateTimeKind.Utc).AddTicks(237) });

            migrationBuilder.UpdateData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 556, DateTimeKind.Utc).AddTicks(519), new DateTime(2025, 5, 8, 17, 55, 51, 556, DateTimeKind.Utc).AddTicks(519) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(6660), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(6798) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(7094), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(7094) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(7098), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(7098) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(8268), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(8411) });

            migrationBuilder.UpdateData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(8700), new DateTime(2025, 5, 8, 17, 55, 51, 555, DateTimeKind.Utc).AddTicks(8701) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ProjectUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "InviterId",
                table: "ProjectUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
