using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Archived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sedan" },
                    { 2, "Coupe" },
                    { 3, "Roadster" },
                    { 4, "Convertible" },
                    { 5, "Gran Coupe" },
                    { 6, "Station wagon" },
                    { 7, "Hatchback" },
                    { 8, "Crossover" },
                    { 9, "SUV" },
                    { 10, "Roadster" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Archived", "CategoryId", "Description", "ImageUrl", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, false, 1, null, "https://www.bmw.ua/content/dam/bmw/common/all-models/m-series/m3-sedan/2023/highlights/bmw-3-series-cs-m-automobiles-gallery-impressions-m3-competition-02_890.jpg", "BMW M3", 100000m, 3 },
                    { 2, false, 1, null, "https://www.bmw.tj/content/dam/bmw/common/all-models/m-series/m5-sedan/2021/Overview/bmw-m5-cs-onepager-gallery-m5-core-02-wallpaper.jpg", "BMW M5", 90000m, 5 },
                    { 3, false, 2, null, "https://carnetwork.s3.ap-southeast-1.amazonaws.com/file/8647cc8284b349178fd78c46e65daa36.jpg", "BMW M4", 105000m, 2 },
                    { 4, false, 5, null, "https://www.bmw.ua/content/dam/bmw/common/all-models/m-series/m8-gran-coupe/2022/onepager/bmw-m8-gran-coupe-onepager-gallery-m8-gc-thumbnail-01.jpg", "BMW M8", 120000m, 2 },
                    { 5, false, 8, null, "https://static.tcimg.net/vehicles/primary/b98f3827e42dc106/2024-BMW-X5_M-white-full_color-driver_side_front_quarter.png", "BMW X5", 70000m, 3 },
                    { 6, false, 1, null, "https://jaegersentrum.no/wp-content/uploads/2022/06/bildekort.png", "BMW M760e", 1670000m, 1 },
                    { 7, false, 8, null, "https://www.motortrend.com/uploads/2022/09/2023-BMW-XM-1.jpg?w=768&width=768&q=75&format=webp", "BMW XM", 240000m, 1 },
                    { 8, false, 9, null, "https://img.tipcars.com/fotky_velke/26141816_1/0/E/bmw-x7-xdrive40i.jpg", "BMW X7", 150000m, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CategoryId",
                table: "Cars",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
