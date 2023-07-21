using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Venta.Data.Migrations
{
    /// <inheritdoc />
    public partial class _013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManufacturingClothing");

            migrationBuilder.AddColumn<int>(
                name: "StockUnitQuantity",
                table: "Material",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ManufacturingClothingSize",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturingId = table.Column<int>(type: "int", nullable: false),
                    ClothingId = table.Column<int>(type: "int", nullable: false),
                    ClothingSizeId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingClothingSize", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingClothingSize_ClothingSize_ClothingSizeId",
                        column: x => x.ClothingSizeId,
                        principalTable: "ClothingSize",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ManufacturingClothingSize_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManufacturingClothingSize_Manufacturing_ManufacturingId",
                        column: x => x.ManufacturingId,
                        principalTable: "Manufacturing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingClothingSize_ClothingId",
                table: "ManufacturingClothingSize",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingClothingSize_ClothingSizeId",
                table: "ManufacturingClothingSize",
                column: "ClothingSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingClothingSize_ManufacturingId",
                table: "ManufacturingClothingSize",
                column: "ManufacturingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManufacturingClothingSize");

            migrationBuilder.DropColumn(
                name: "StockUnitQuantity",
                table: "Material");

            migrationBuilder.CreateTable(
                name: "ManufacturingClothing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClothingId = table.Column<int>(type: "int", nullable: false),
                    ManufacturingId = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManufacturingClothing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ManufacturingClothing_Clothing_ClothingId",
                        column: x => x.ClothingId,
                        principalTable: "Clothing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManufacturingClothing_Manufacturing_ManufacturingId",
                        column: x => x.ManufacturingId,
                        principalTable: "Manufacturing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingClothing_ClothingId",
                table: "ManufacturingClothing",
                column: "ClothingId");

            migrationBuilder.CreateIndex(
                name: "IX_ManufacturingClothing_ManufacturingId",
                table: "ManufacturingClothing",
                column: "ManufacturingId");
        }
    }
}
