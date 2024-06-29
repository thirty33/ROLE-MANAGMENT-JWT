using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeople.Migrations
{
    /// <inheritdoc />
    public partial class add_username_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Username",
                value: "John");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Username",
                value: "Jane");

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 14, 31, 34, 571, DateTimeKind.Local).AddTicks(2311));

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 14, 31, 34, 571, DateTimeKind.Local).AddTicks(2325));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

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
    }
}
