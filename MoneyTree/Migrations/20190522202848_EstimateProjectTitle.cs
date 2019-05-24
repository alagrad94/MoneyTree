using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneyTree.Migrations
{
    public partial class EstimateProjectTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_Customer_CustomerId",
                table: "Estimate");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Estimate",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectTitle",
                table: "Estimate",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_Customer_CustomerId",
                table: "Estimate",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimate_Customer_CustomerId",
                table: "Estimate");

            migrationBuilder.DropColumn(
                name: "ProjectTitle",
                table: "Estimate");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Estimate",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Estimate_Customer_CustomerId",
                table: "Estimate",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
