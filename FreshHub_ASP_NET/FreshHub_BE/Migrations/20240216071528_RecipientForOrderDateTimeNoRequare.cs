using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreshHub_BE.Migrations
{
    /// <inheritdoc />
    public partial class RecipientForOrderDateTimeNoRequare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "OrderTimeOnly",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "OrderDateOnly",
                table: "Orders",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDatails_Orders_OrderId",
                table: "OrderDatails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDatails_Orders_OrderId",
                table: "OrderDatails");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "OrderTimeOnly",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0),
                oldClrType: typeof(TimeOnly),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "OrderDateOnly",
                table: "Orders",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
