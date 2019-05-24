using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class CustomEstimateCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomEstimateCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(nullable: false),
                    EstimateId = table.Column<int>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    UnitOfMeasure = table.Column<string>(nullable: false),
                    CostPerUnit = table.Column<double>(nullable: false),
                    MarkupPercent = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomEstimateCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomEstimateCost_Estimate_EstimateId",
                        column: x => x.EstimateId,
                        principalTable: "Estimate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomEstimateCost_EstimateId",
                table: "CustomEstimateCost",
                column: "EstimateId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomProjectCost_Estimate_EstimateId",
                table: "CustomProjectCost");

            migrationBuilder.DropTable(
                name: "CustomEstimateCost");

            migrationBuilder.DropIndex(
                name: "IX_CustomProjectCost_EstimateId",
                table: "CustomProjectCost");

            migrationBuilder.DropColumn(
                name: "EstimateId",
                table: "CustomProjectCost");
        }
    }
}
