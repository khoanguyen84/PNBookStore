using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class Seeding_AspNetRole_Customer_Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32ffd287-205f-43a2-9f0d-80bc5309fb47", "2", "Customer", "Customer" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "811870b3-2817-4fd8-a051-561781be09b9", "AQAAAAEAACcQAAAAEP7/dFMkMOZ9rAvXMCZoNpGDXAz6PFNfNnR3MkylM6NSa51jSv9NkwrbplubpuF4Yw==", "a22e9118-973e-400e-9f80-8f8dc56e9fd4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "32ffd287-205f-43a2-9f0d-80bc5309fb47");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f614c3a7-2103-4e50-b922-2ba19e35fb43", "AQAAAAEAACcQAAAAELc5uD4FxKSIsK8rMCM7WjwdpM9UydxVF3imaH13hFtiT56ZCixrOJdHX2QaWcFTOg==", "0eb21330-38e7-4d58-8b5c-987b791c7369" });
        }
    }
}
