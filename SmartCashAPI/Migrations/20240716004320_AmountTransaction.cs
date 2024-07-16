using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCashAPI.Migrations
{
    /// <inheritdoc />
    public partial class AmountTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "Transactions",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");
        }
    }
}
