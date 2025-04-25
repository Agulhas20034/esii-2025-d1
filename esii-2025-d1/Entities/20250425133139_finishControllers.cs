using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace esii2025d1.Entities
{
    /// <inheritdoc />
    public partial class finishControllers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Reports_ReportId",
                table: "Media");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectUsers_ProjectUserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectUserId",
                table: "Projects");

            migrationBuilder.DeleteData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "ProjectUserId",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "Updated_at",
                table: "Media",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Deleted_at",
                table: "Media",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "Created_at",
                table: "Media",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProjectUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Media",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "ProjectId", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7378), new DateTime(2025, 5, 2, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7003), 1, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(6818), new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7589) });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Email", "Name", "PhoneNumber", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(9673), null, "test@gmail.com", "Test Customer", "123456789", new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(9833) },
                    { 2, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(154), null, "test2@gmail.com", "Test Customer2", "923456789", new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(154) }
                });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 25, 13, 31, 38, 843, DateTimeKind.Utc).AddTicks(1774), new DateTime(2025, 4, 25, 13, 31, 38, 843, DateTimeKind.Utc).AddTicks(2300), new DateTime(2025, 4, 25, 13, 31, 38, 843, DateTimeKind.Utc).AddTicks(2474) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DailyWorkHours", "DeletedAt", "Description", "HourlyRate", "Name", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3032), 1, 8, null, "Test project description", 14f, "Test Project", 0, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3174), 1 },
                    { 2, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3481), 1, 4, null, "Test project description2", 16f, "Test Project2", 0, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3482), 1 },
                    { 3, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3485), 1, 4, null, "Test project description3", 16f, "Test Project3", 0, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(3486), 1 }
                });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "EndDate", "HourlyRate", "ProjectId", "StartDate", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7947), null, "Test assignment2", new DateTime(2025, 5, 1, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7946), 20f, 1, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7945), 0, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7947), 1 },
                    { 3, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7954), null, "Test assignment3", new DateTime(2025, 4, 28, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7953), 20f, 2, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7953), 0, new DateTime(2025, 4, 25, 13, 31, 38, 845, DateTimeKind.Utc).AddTicks(7954), 2 }
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "Path", "ProjectId", "ReportId", "Type", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(6614), null, "test", "test.jpg", 1, null, "image", new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(6766) });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ProjectId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(4648), null, 1, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(4797), 1 },
                    { 2, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(5098), null, 2, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(5099), 2 }
                });

            migrationBuilder.InsertData(
                table: "Media",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "Path", "ProjectId", "ReportId", "Type", "UpdatedAt" },
                values: new object[] { 2, new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(7073), null, "test2", "test2.jpg", 2, 2, "Report", new DateTime(2025, 4, 25, 13, 31, 38, 846, DateTimeKind.Utc).AddTicks(7074) });

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Reports_ReportId",
                table: "Media",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Media_Reports_ReportId",
                table: "Media");

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Media",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reports",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProjectUsers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Media",
                newName: "Updated_at");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "Media",
                newName: "Deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Media",
                newName: "Created_at");

            migrationBuilder.AddColumn<int>(
                name: "ProjectUserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                table: "Media",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Assignments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "EndDate", "ProjectId", "StartDate", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 4, 22, 8, 50, 15, 978, DateTimeKind.Utc).AddTicks(4633), new DateTime(2025, 4, 29, 8, 50, 15, 978, DateTimeKind.Utc).AddTicks(4278), null, new DateTime(2025, 4, 22, 8, 50, 15, 978, DateTimeKind.Utc).AddTicks(4103), new DateTime(2025, 4, 22, 8, 50, 15, 978, DateTimeKind.Utc).AddTicks(4791) });

            migrationBuilder.UpdateData(
                table: "Joms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Date", "created_at", "updated_at" },
                values: new object[] { new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(5068), new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(5642), new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(5813) });

            migrationBuilder.InsertData(
                table: "Joms",
                columns: new[] { "Id", "Date", "IsDone", "Label", "TestNumber", "created_at", "deleted_at", "updated_at" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6140), true, "Joms2", 7.5f, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6141), null, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6141) },
                    { 3, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6143), true, "Joms3", 10f, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6144), null, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6144) },
                    { 4, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6146), false, "Joms4", 62f, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6147), null, new DateTime(2025, 4, 22, 8, 50, 15, 974, DateTimeKind.Utc).AddTicks(6147) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectUserId",
                table: "Projects",
                column: "ProjectUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Media_Reports_ReportId",
                table: "Media",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectUsers_ProjectUserId",
                table: "Projects",
                column: "ProjectUserId",
                principalTable: "ProjectUsers",
                principalColumn: "Id");
        }
    }
}
