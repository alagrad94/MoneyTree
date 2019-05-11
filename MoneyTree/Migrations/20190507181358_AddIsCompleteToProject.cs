using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class AddIsCompleteToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_CostItem_CostCategory_CostCategoryId",
            //    table: "CostItem");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
            //    table: "CostItem");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_CostPerUnit_CostItem_CostItemId",
            //    table: "CostPerUnit");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectCost_CostItem_CostItemId",
            //    table: "ProjectCost");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
            //    table: "ProjectCost");

            migrationBuilder.AddColumn<bool>(
                "IsComplete",
                "Project",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CostItem_CostCategory_CostCategoryId",
            //    table: "CostItem",
            //    column: "CostCategoryId",
            //    principalTable: "CostCategory",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
            //    table: "CostItem",
            //    column: "UnitOfMeasureId",
            //    principalTable: "UnitOfMeasure",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_CostPerUnit_CostItem_CostItemId",
            //    table: "CostPerUnit",
            //    column: "CostItemId",
            //    principalTable: "CostItem",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectCost_CostItem_CostItemId",
            //    table: "ProjectCost",
            //    column: "CostItemId",
            //    principalTable: "CostItem",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_ProjectCost_CostPerUnit_CostPerUnitId",
            //    table: "ProjectCost",
            //    column: "CostPerUnitId",
            //    principalTable: "CostPerUnit",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_CostItem_CostCategory_CostCategoryId",
                "CostItem");

            migrationBuilder.DropForeignKey(
                "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                "CostItem");

            migrationBuilder.DropForeignKey(
                "FK_CostPerUnit_CostItem_CostItemId",
                "CostPerUnit");

            migrationBuilder.DropForeignKey(
                "FK_ProjectCost_CostItem_CostItemId",
                "ProjectCost");

            migrationBuilder.DropForeignKey(
                "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                "ProjectCost");

            migrationBuilder.DropColumn(
                "IsComplete",
                "Project");

            migrationBuilder.AddForeignKey(
                "FK_CostItem_CostCategory_CostCategoryId",
                "CostItem",
                "CostCategoryId",
                "CostCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                "CostItem",
                "UnitOfMeasureId",
                "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_CostPerUnit_CostItem_CostItemId",
                "CostPerUnit",
                "CostItemId",
                "CostItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_ProjectCost_CostItem_CostItemId",
                "ProjectCost",
                "CostItemId",
                "CostItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_ProjectCost_CostPerUnit_CostPerUnitId",
                "ProjectCost",
                "CostPerUnitId",
                "CostPerUnit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
