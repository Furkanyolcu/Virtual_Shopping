using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Virtual_Shopping.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Customers",
                newName: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Customers",
                newName: "StudentID");
        }
    }
}
