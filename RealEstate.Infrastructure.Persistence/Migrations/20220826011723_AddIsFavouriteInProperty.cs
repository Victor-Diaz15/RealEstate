using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstate.Infrastructure.Persistence.Migrations
{
    public partial class AddIsFavouriteInProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Properties");
        }
    }
}
