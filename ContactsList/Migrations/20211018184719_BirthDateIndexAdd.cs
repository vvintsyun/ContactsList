using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsList.Migrations
{
    public partial class BirthDateIndexAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Contacts",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BirthDate",
                table: "Contacts",
                column: "BirthDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Contacts_BirthDate",
                table: "Contacts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "Contacts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);
        }
    }
}
