using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class EstimateCostRemoveFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateCost_CostCategory_CostCategoryId",
                table: "EstimateCost");

            migrationBuilder.DropForeignKey(
                name: "FK_EstimateCost_UnitOfMeasure_UnitOfMeasureId",
                table: "EstimateCost");

            migrationBuilder.DropIndex(
                name: "IX_EstimateCost_CostCategoryId",
                table: "EstimateCost");

            migrationBuilder.DropIndex(
                name: "IX_EstimateCost_UnitOfMeasureId",
                table: "EstimateCost");

            migrationBuilder.DropColumn(
                name: "CostCategoryId",
                table: "EstimateCost");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "EstimateCost");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostCategoryId",
                table: "EstimateCost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasureId",
                table: "EstimateCost",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstimateId",
                table: "CustomProjectCost",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstimateId",
                table: "CostCategory",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_CostCategoryId",
                table: "EstimateCost",
                column: "CostCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EstimateCost_UnitOfMeasureId",
                table: "EstimateCost",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomProjectCost_EstimateId",
                table: "CustomProjectCost",
                column: "EstimateId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCategory_EstimateId",
                table: "CostCategory",
                column: "EstimateId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostCategory_Estimate_EstimateId",
                table: "CostCategory",
                column: "EstimateId",
                principalTable: "Estimate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomProjectCost_Estimate_EstimateId",
                table: "CustomProjectCost",
                column: "EstimateId",
                principalTable: "Estimate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateCost_CostCategory_CostCategoryId",
                table: "EstimateCost",
                column: "CostCategoryId",
                principalTable: "CostCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EstimateCost_UnitOfMeasure_UnitOfMeasureId",
                table: "EstimateCost",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
