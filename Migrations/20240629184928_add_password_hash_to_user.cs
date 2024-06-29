using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiPeople.Migrations
{
    /// <inheritdoc />
    public partial class add_password_hash_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Users",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 108, 57, 7, 34, 21, 127, 216, 7, 150, 237, 46, 66, 38, 0, 10, 110, 15, 234, 57, 180, 38, 237, 141, 222, 116, 229, 215, 193, 149, 232, 46, 155, 201, 167, 125, 78, 26, 136, 7, 133, 179, 251, 115, 45, 208, 218, 119, 9, 119, 246, 70, 83, 175, 213, 130, 89, 13, 8, 62, 159, 151, 107, 222, 46 }, new byte[] { 120, 146, 176, 28, 220, 31, 68, 108, 170, 170, 36, 52, 110, 155, 74, 167, 10, 218, 103, 117, 174, 62, 149, 241, 106, 107, 191, 3, 26, 155, 223, 155, 152, 195, 190, 24, 210, 226, 3, 102, 245, 234, 4, 117, 72, 166, 6, 85, 145, 132, 22, 83, 83, 237, 230, 150, 68, 68, 117, 33, 109, 220, 105, 46, 104, 71, 18, 58, 104, 253, 203, 81, 216, 94, 19, 63, 185, 196, 252, 183, 164, 86, 232, 99, 200, 193, 65, 242, 180, 110, 231, 190, 95, 179, 173, 14, 140, 131, 165, 105, 235, 127, 181, 129, 27, 54, 175, 129, 247, 225, 82, 222, 245, 109, 63, 2, 193, 203, 165, 37, 233, 146, 248, 149, 200, 183, 214, 94 } });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PasswordHash", "PasswordSalt" },
                values: new object[] { new byte[] { 155, 35, 137, 117, 52, 87, 70, 103, 94, 22, 145, 62, 90, 133, 234, 160, 42, 219, 71, 150, 60, 83, 61, 244, 40, 65, 129, 45, 58, 18, 210, 53, 173, 55, 119, 93, 103, 182, 193, 144, 163, 74, 5, 248, 166, 93, 147, 18, 0, 95, 99, 108, 18, 102, 191, 46, 76, 230, 36, 100, 144, 64, 165, 120 }, new byte[] { 252, 32, 80, 231, 190, 66, 33, 157, 161, 31, 151, 100, 52, 208, 48, 200, 35, 203, 255, 99, 175, 111, 182, 123, 58, 248, 81, 116, 89, 60, 205, 214, 228, 92, 23, 162, 127, 125, 231, 6, 145, 173, 31, 217, 150, 65, 24, 152, 188, 252, 188, 170, 198, 10, 188, 94, 71, 159, 150, 42, 158, 73, 26, 164, 66, 94, 176, 45, 11, 241, 65, 183, 151, 2, 160, 160, 3, 156, 88, 181, 68, 207, 15, 30, 109, 143, 208, 155, 126, 32, 126, 116, 126, 70, 241, 98, 110, 177, 70, 218, 178, 143, 116, 161, 205, 135, 17, 154, 201, 177, 41, 64, 183, 53, 180, 187, 118, 147, 199, 32, 249, 86, 234, 126, 164, 60, 123, 45 } });

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb410"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 14, 49, 27, 444, DateTimeKind.Local).AddTicks(5621));

            migrationBuilder.UpdateData(
                table: "task",
                keyColumn: "TaskId",
                keyValue: new Guid("fe2de405-c38e-4c90-ac52-da0540dfb411"),
                column: "created_at",
                value: new DateTime(2024, 6, 29, 14, 49, 27, 444, DateTimeKind.Local).AddTicks(5641));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Users");

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
    }
}
