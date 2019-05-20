using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class CustomCosts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomProjectCost");

            migrationBuilder.CreateTable(
                name: "CustomProjectCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemName = table.Column<string>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    UnitOfMeasure = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    CostPerUnit = table.Column<double>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    DateUsed = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProjectCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomProjectCost_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomProjectCost_ProjectId",
                table: "CustomProjectCost",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomProjectCost");

            migrationBuilder.CreateTable(
                name: "CustomProjecCost",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CostPerUnit = table.Column<double>(nullable: false),
                    DateUsed = table.Column<DateTime>(nullable: false),
                    ItemName = table.Column<string>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomProjecCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomProjecCost_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomProjecCost_ProjectId",
                table: "CustomProjecCost",
                column: "ProjectId");
        }
    }
}
