using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopApp.Migrations
{
    /// <inheritdoc />
    public partial class Fixpropertyname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeoductId",
                table: "ProductImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeoductId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
