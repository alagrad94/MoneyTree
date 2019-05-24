using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class Estimates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MarkupPercent",
                table: "CostCategory",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Estimate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(nullable: true),
                    EstimateDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estimate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estimate_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EstimateCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EstimateId = table.Column<int>(nullable: false),
                    CostItemId = table.Column<int>(nullable: false),
                    CostCategoryId = table.Column<int>(nullable: false),
                    UnitOfMeasureId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstimateCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstimateCost_CostCategory_CostCategoryId",
                        column: x => x.CostCategoryId,
                        principalTable: "CostCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstimateCost_CostItem_CostItemId",
                        column: x => x.CostItemId,
                        principalTable: "CostItem",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EstimateCost_Estimate_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstimateCost_UnitOfMeasure_UnitOfMeasureId",
                        column: x => x.UnitOfMeasureId,
                        principalTable: "UnitOfMeasure",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Estimate_CustomerId",
                table: "Estimate",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_CostCategoryId",
                table: "EstimateCost",
                column: "CostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_CostItemId",
                table: "EstimateCost",
                column: "CostItemId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_EstimateId",
                table: "EstimateCost",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_UnitOfMeasureId",
                table: "EstimateCost",
                column: "UnitOfMeasureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostCategory_Estimate_EstimateId",
                table: "CostCategory");

            migrationBuilder.DropTable(
                name: "EstimateCost");

            migrationBuilder.DropTable(
                name: "Estimate");

            migrationBuilder.DropIndex(
                name: "IX_CostCategory_EstimateId",
                table: "CostCategory");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "CustomProjectCost");

            migrationBuilder.DropColumn(
                name: "EstimateId",
                table: "CostCategory");

            migrationBuilder.DropColumn(
                name: "MarkupPercent",
                table: "CostCategory");
        }
    }
}
