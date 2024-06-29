using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeople.Migrations
{
    /// <inheritdoc />
    public partial class add_new_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 13, 17, 22, 15, DateTimeKind.Local).AddTicks(4307));

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 13, 17, 22, 15, DateTimeKind.Local).AddTicks(4323));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 12, 44, 8, 553, DateTimeKind.Local).AddTicks(3601));

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 12, 44, 8, 553, DateTimeKind.Local).AddTicks(3616));
        }
    }
}
