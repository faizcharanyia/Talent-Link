using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sp23Team09FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class ap182 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrDateID",
                table: "Positions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrDateID",
                table: "Companys",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrDateID",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_CurrDateID",
                table: "Positions",
                column: "CurrDateID");

            migrationBuilder.CreateIndex(
                name: "IX_Companys_CurrDateID",
                table: "Companys",
                column: "CurrDateID");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CurrDateID",
                table: "Applications",
                column: "CurrDateID");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_CurrDate_CurrDateID",
                table: "Applications",
                column: "CurrDateID",
                principalTable: "CurrDate",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Companys_CurrDate_CurrDateID",
                table: "Companys",
                column: "CurrDateID",
                principalTable: "CurrDate",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_CurrDate_CurrDateID",
                table: "Positions",
                column: "CurrDateID",
                principalTable: "CurrDate",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_CurrDate_CurrDateID",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_Companys_CurrDate_CurrDateID",
                table: "Companys");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_CurrDate_CurrDateID",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_CurrDateID",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Companys_CurrDateID",
                table: "Companys");

            migrationBuilder.DropIndex(
                name: "IX_Applications_CurrDateID",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CurrDateID",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "CurrDateID",
                table: "Companys");

            migrationBuilder.DropColumn(
                name: "CurrDateID",
                table: "Applications");
        }
    }
}
