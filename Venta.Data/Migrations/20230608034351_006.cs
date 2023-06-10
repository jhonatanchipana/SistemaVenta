using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingModel");

            migrationBuilder.AddColumn<int>(
                name: "CampaignId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CampaignId",
                table: "Sales",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Campaign_CampaignId",
                table: "Sales",
                column: "CampaignId",
                principalTable: "Campaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Campaign_CampaignId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_CampaignId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Sales");

            migrationBuilder.CreateTable(
                name: "ClothingModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothingCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingModel_ClothingCategory_ClothingCategoryId",
                        column: x => x.ClothingCategoryId,
                        principalTable: "ClothingCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingModel_ClothingCategoryId",
                table: "ClothingModel",
                column: "ClothingCategoryId");
        }
    }
}
