using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class FixPhoneNumberAndMakeTablesSingular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostItems_CostCategories_CostCategoryId",
                table: "CostItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CostItems_UnitOfMeasures_UnitOfMeasureId",
                table: "CostItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CostPerUnits_CostItems_CostItemId",
                table: "CostPerUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCosts_CostItems_CostItemId",
                table: "ProjectCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCosts_Projects_ProjectId",
                table: "ProjectCosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId1",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitOfMeasures",
                table: "UnitOfMeasures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Projects",
                table: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCosts",
                table: "ProjectCosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostPerUnits",
                table: "CostPerUnits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItems",
                table: "CostItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostCategories",
                table: "CostCategories");

            migrationBuilder.RenameTable(
                name: "UnitOfMeasures",
                newName: "UnitOfMeasure");

            migrationBuilder.RenameTable(
                name: "Projects",
                newName: "Project");

            migrationBuilder.RenameTable(
                name: "ProjectCosts",
                newName: "ProjectCost");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customer");

            migrationBuilder.RenameTable(
                name: "CostPerUnits",
                newName: "CostPerUnit");

            migrationBuilder.RenameTable(
                name: "CostItems",
                newName: "CostItem");

            migrationBuilder.RenameTable(
                name: "CostCategories",
                newName: "CostCategory");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_UserId1",
                table: "Project",
                newName: "IX_Project_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CustomerId",
                table: "Project",
                newName: "IX_Project_CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCosts_ProjectId",
                table: "ProjectCost",
                newName: "IX_ProjectCost_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCosts_CostItemId",
                table: "ProjectCost",
                newName: "IX_ProjectCost_CostItemId");

            migrationBuilder.RenameColumn(
                name: "PhohneNumber",
                table: "Customer",
                newName: "PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_CostPerUnits_CostItemId",
                table: "CostPerUnit",
                newName: "IX_CostPerUnit_CostItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CostItems_UnitOfMeasureId",
                table: "CostItem",
                newName: "IX_CostItem_UnitOfMeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_CostItems_CostCategoryId",
                table: "CostItem",
                newName: "IX_CostItem_CostCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitOfMeasure",
                table: "UnitOfMeasure",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Project",
                table: "Project",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCost",
                table: "ProjectCost",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                table: "Customer",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostPerUnit",
                table: "CostPerUnit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostCategory",
                table: "CostCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_CostCategory_CostCategoryId",
                table: "CostItem",
                column: "CostCategoryId",
                principalTable: "CostCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostItem_UnitOfMeasure_UnitOfMeasureId",
                table: "CostItem",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostPerUnit_CostItem_CostItemId",
                table: "CostPerUnit",
                column: "CostItemId",
                principalTable: "CostItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Customer_CustomerId",
                table: "Project",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_AspNetUsers_UserId1",
                table: "Project",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost",
                column: "CostItemId",
                principalTable: "CostItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCost_Project_ProjectId",
                table: "ProjectCost",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
                name: "FK_CostPerUnit_CostItem_CostItemId",
                table: "CostPerUnit");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Customer_CustomerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_AspNetUsers_UserId1",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_CostItem_CostItemId",
                table: "ProjectCost");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCost_Project_ProjectId",
                table: "ProjectCost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UnitOfMeasure",
                table: "UnitOfMeasure");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCost",
                table: "ProjectCost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Project",
                table: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                table: "Customer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostPerUnit",
                table: "CostPerUnit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostItem",
                table: "CostItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CostCategory",
                table: "CostCategory");

            migrationBuilder.RenameTable(
                name: "UnitOfMeasure",
                newName: "UnitOfMeasures");

            migrationBuilder.RenameTable(
                name: "ProjectCost",
                newName: "ProjectCosts");

            migrationBuilder.RenameTable(
                name: "Project",
                newName: "Projects");

            migrationBuilder.RenameTable(
                name: "Customer",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "CostPerUnit",
                newName: "CostPerUnits");

            migrationBuilder.RenameTable(
                name: "CostItem",
                newName: "CostItems");

            migrationBuilder.RenameTable(
                name: "CostCategory",
                newName: "CostCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCost_ProjectId",
                table: "ProjectCosts",
                newName: "IX_ProjectCosts_ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectCost_CostItemId",
                table: "ProjectCosts",
                newName: "IX_ProjectCosts_CostItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Project_UserId1",
                table: "Projects",
                newName: "IX_Projects_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Project_CustomerId",
                table: "Projects",
                newName: "IX_Projects_CustomerId");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "PhohneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_CostPerUnit_CostItemId",
                table: "CostPerUnits",
                newName: "IX_CostPerUnits_CostItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CostItem_UnitOfMeasureId",
                table: "CostItems",
                newName: "IX_CostItems_UnitOfMeasureId");

            migrationBuilder.RenameIndex(
                name: "IX_CostItem_CostCategoryId",
                table: "CostItems",
                newName: "IX_CostItems_CostCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UnitOfMeasures",
                table: "UnitOfMeasures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCosts",
                table: "ProjectCosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Projects",
                table: "Projects",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostPerUnits",
                table: "CostPerUnits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostItems",
                table: "CostItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CostCategories",
                table: "CostCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CostItems_CostCategories_CostCategoryId",
                table: "CostItems",
                column: "CostCategoryId",
                principalTable: "CostCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostItems_UnitOfMeasures_UnitOfMeasureId",
                table: "CostItems",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CostPerUnits_CostItems_CostItemId",
                table: "CostPerUnits",
                column: "CostItemId",
                principalTable: "CostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCosts_CostItems_CostItemId",
                table: "ProjectCosts",
                column: "CostItemId",
                principalTable: "CostItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCosts_Projects_ProjectId",
                table: "ProjectCosts",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Customers_CustomerId",
                table: "Projects",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId1",
                table: "Projects",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
