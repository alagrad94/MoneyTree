using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class QuantityToDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItem_CostCategory_CostCategoryId",
                table: "CostItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                table: "CostItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                table: "ProjectCost");

            migrationBuilder.AlterColumn<double>(
                name: "Quantity",
                table: "ProjectCost",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_CostCategory_CostCategoryId",
                table: "CostItem",
                column: "CostCategoryId",
                principalTable: "CostCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                table: "CostItem",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost",
                column: "CostItemId",
                principalTable: "CostItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                table: "ProjectCost",
                column: "CostPerUnitId",
                principalTable: "CostPerUnit",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItem_CostCategory_CostCategoryId",
                table: "CostItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                table: "CostItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                table: "ProjectCost");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "ProjectCost",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_CostCategory_CostCategoryId",
                table: "CostItem",
                column: "CostCategoryId",
                principalTable: "CostCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                table: "CostItem",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost",
                column: "CostItemId",
                principalTable: "CostItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                table: "ProjectCost",
                column: "CostPerUnitId",
                principalTable: "CostPerUnit",
                principalColumn: "Id");
        }
    }
}
