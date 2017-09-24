using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bazar.Data.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressID",
                table: "Orders",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Address_AddressID",
                table: "Orders",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Address_AddressID",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AddressID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Orders");
        }
    }
}
