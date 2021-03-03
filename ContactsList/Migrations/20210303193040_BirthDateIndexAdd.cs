using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactsList.Migrations
{
    public partial class BirthDateIndexAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
