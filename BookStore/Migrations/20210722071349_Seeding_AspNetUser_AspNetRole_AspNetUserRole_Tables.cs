using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class Seeding_AspNetUser_AspNetRole_AspNetUserRole_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0c6661b-0964-4e62-8083-3cac6a6741ec", "1", "SystemAdmin", "SystemAdmin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", 0, "/images/khoanguyen.jpg", "b81eef44-ace3-41a8-a8b3-b0a1c09c4f5d", "khoa.nguyen@codegym.vn", false, false, null, "khoa.nguyen@codegym.vn", "khoa.nguyen@codegym.vn", "AQAAAAEAACcQAAAAEMGD6q+hZzb3nZccyPEwf7QLrNW3GNWV3/H44JHHCzZe4dmbS8d8OGBfoHueKF7gEg==", null, false, "a0e7dcbc-89e4-4ec7-b124-495b0abd4e08", false, "khoa.nguyen@codegym.vn" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "Photo",
                value: "/images/no-photo.jpg");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0c6661b-0964-4e62-8083-3cac6a6741ec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2,
                column: "Photo",
                value: "images/no-photo.jpg");
        }
    }
}
