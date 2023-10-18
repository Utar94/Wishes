using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logitar.Wishes.EntityFrameworkCore.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class CreateItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Summary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PictureUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Rank = table.Column<byte>(type: "tinyint", nullable: false),
                    RankCategory = table.Column<byte>(type: "tinyint", nullable: false),
                    AveragePrice = table.Column<double>(type: "float", nullable: true),
                    MinimumPrice = table.Column<double>(type: "float", nullable: true),
                    MaximumPrice = table.Column<double>(type: "float", nullable: true),
                    PriceCategory = table.Column<byte>(type: "tinyint", nullable: true),
                    ContentText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContentType = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Gallery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Links = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "WishlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_AveragePrice",
                table: "Items",
                column: "AveragePrice");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedBy",
                table: "Items",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedOn",
                table: "Items",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Items_DisplayName",
                table: "Items",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MaximumPrice",
                table: "Items",
                column: "MaximumPrice");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MinimumPrice",
                table: "Items",
                column: "MinimumPrice");

            migrationBuilder.CreateIndex(
                name: "IX_Items_PriceCategory",
                table: "Items",
                column: "PriceCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Rank",
                table: "Items",
                column: "Rank");

            migrationBuilder.CreateIndex(
                name: "IX_Items_RankCategory",
                table: "Items",
                column: "RankCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Summary",
                table: "Items",
                column: "Summary");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpdatedBy",
                table: "Items",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpdatedOn",
                table: "Items",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Version",
                table: "Items",
                column: "Version");

            migrationBuilder.CreateIndex(
                name: "IX_Items_WishlistId_Id",
                table: "Items",
                columns: new[] { "WishlistId", "Id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
