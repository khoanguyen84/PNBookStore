using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class SeedingDataBookTB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Authors", "BookName", "CategoryId", "Description", "IsDeleted", "Price", "PublishYear" },
                values: new object[] { 1, "Nam Thư", "Doraemon", 4, "Truyện về chú mèo máy Doraemon", false, 10000, 2000 });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Authors", "BookName", "CategoryId", "Description", "IsDeleted", "Price", "PublishYear" },
                values: new object[] { 2, "Bửu Ngọc", "Tiếng Anh 12", 2, "Sách giáo khoa Tiếng Anh", false, 12000, 2000 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);
        }
    }
}
