using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Logitar.Wishes.EntityFrameworkCore.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Type = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.ActorId);
                });

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    WishlistId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PictureUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    ItemCount = table.Column<int>(type: "integer", nullable: false),
                    AggregateId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.WishlistId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WishlistId = table.Column<int>(type: "integer", nullable: false),
                    Id = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Summary = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PictureUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    Rank = table.Column<byte>(type: "smallint", nullable: false),
                    RankCategory = table.Column<byte>(type: "smallint", nullable: false),
                    AveragePrice = table.Column<double>(type: "double precision", nullable: true),
                    MinimumPrice = table.Column<double>(type: "double precision", nullable: true),
                    MaximumPrice = table.Column<double>(type: "double precision", nullable: true),
                    PriceCategory = table.Column<byte>(type: "smallint", nullable: true),
                    ContentText = table.Column<string>(type: "text", nullable: true),
                    ContentType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    Gallery = table.Column<string>(type: "text", nullable: true),
                    Links = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "IX_Actors_DisplayName",
                table: "Actors",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_EmailAddress",
                table: "Actors",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Id",
                table: "Actors",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Actors_IsDeleted",
                table: "Actors",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_Type",
                table: "Actors",
                column: "Type");

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

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AggregateId",
                table: "Wishlists",
                column: "AggregateId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CreatedBy",
                table: "Wishlists",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CreatedOn",
                table: "Wishlists",
                column: "CreatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_DisplayName",
                table: "Wishlists",
                column: "DisplayName");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_ItemCount",
                table: "Wishlists",
                column: "ItemCount");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UpdatedBy",
                table: "Wishlists",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UpdatedOn",
                table: "Wishlists",
                column: "UpdatedOn");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_Version",
                table: "Wishlists",
                column: "Version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Wishlists");
        }
    }
}
