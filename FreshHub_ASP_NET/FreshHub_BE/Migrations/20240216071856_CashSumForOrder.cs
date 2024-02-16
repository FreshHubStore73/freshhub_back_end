using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshHub_BE.Migrations
{
    /// <inheritdoc />
    public partial class CashSumForOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CashSum",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashSum",
                table: "Orders");
        }
    }
}
