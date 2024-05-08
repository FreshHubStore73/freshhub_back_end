using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshHub_BE.Migrations
{
    /// <inheritdoc />
    public partial class dataOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"UPDATE Orders SET DeliveryTime = '{DateTime.MinValue.ToString("yyyy-MM-dd hh:mm:ss")}' WHERE DeliveryTime is null or LENGTH(DeliveryTime) = 0");
            migrationBuilder.Sql($"UPDATE Orders SET CreatedDate = '{DateTime.MinValue.ToString("yyyy-MM-dd hh:mm:ss")}' WHERE CreatedDate is null or LENGTH(CreatedDate) = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
