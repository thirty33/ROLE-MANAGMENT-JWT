using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApiPeople.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "Username" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John Doe", new byte[] { 7, 63, 4, 81, 169, 90, 56, 118, 171, 253, 126, 7, 149, 22, 161, 180, 92, 175, 127, 138, 4, 146, 207, 183, 15, 234, 201, 219, 195, 168, 241, 225, 136, 116, 53, 224, 24, 82, 112, 84, 227, 215, 82, 74, 37, 213, 89, 92, 174, 196, 193, 128, 216, 35, 238, 42, 123, 131, 69, 26, 163, 191, 110, 176 }, new byte[] { 243, 14, 222, 90, 86, 108, 29, 28, 186, 21, 20, 1, 125, 110, 30, 153, 31, 97, 230, 103, 169, 232, 43, 95, 238, 31, 5, 154, 200, 121, 158, 189, 204, 90, 67, 209, 175, 175, 186, 129, 159, 245, 235, 175, 132, 80, 77, 240, 129, 129, 135, 162, 97, 255, 121, 102, 200, 218, 106, 215, 190, 44, 206, 62, 22, 144, 171, 122, 17, 226, 251, 16, 11, 106, 203, 114, 94, 107, 118, 168, 150, 105, 0, 136, 190, 151, 93, 126, 227, 117, 63, 9, 83, 41, 110, 81, 13, 216, 66, 33, 233, 121, 137, 74, 174, 112, 222, 8, 220, 91, 27, 155, 128, 171, 185, 53, 251, 85, 158, 253, 103, 128, 157, 70, 157, 72, 173, 100 }, "John" },
                    { 2, "jane.smith@example.com", "Jane Smith", new byte[] { 214, 117, 28, 126, 157, 41, 94, 219, 206, 20, 5, 237, 33, 150, 60, 63, 237, 27, 176, 115, 243, 177, 8, 75, 128, 56, 1, 238, 1, 75, 86, 249, 228, 212, 19, 144, 250, 254, 23, 139, 1, 83, 19, 115, 144, 252, 91, 63, 248, 178, 151, 96, 166, 185, 107, 153, 143, 160, 220, 184, 196, 210, 199, 168 }, new byte[] { 51, 48, 177, 59, 177, 67, 44, 24, 249, 34, 31, 144, 30, 41, 155, 240, 70, 47, 249, 144, 193, 153, 14, 125, 110, 139, 36, 46, 81, 124, 242, 88, 97, 104, 79, 145, 202, 72, 156, 143, 201, 70, 226, 157, 17, 150, 42, 211, 222, 101, 24, 64, 109, 46, 6, 237, 164, 134, 213, 80, 139, 152, 196, 195, 224, 115, 239, 108, 134, 225, 97, 6, 88, 189, 227, 234, 72, 60, 204, 181, 184, 70, 200, 122, 115, 206, 188, 119, 39, 162, 36, 55, 70, 21, 162, 126, 86, 55, 63, 30, 218, 26, 214, 54, 161, 138, 253, 11, 133, 255, 6, 183, 255, 105, 245, 72, 153, 216, 225, 73, 72, 17, 140, 124, 207, 244, 104, 96 }, "Jane" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
