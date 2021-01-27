using Microsoft.EntityFrameworkCore.Migrations;

namespace GOT.Migrations
{
    public partial class MoveIsFromAToBColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromAToB",
                table: "Paths");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromAToB",
                table: "PathTrips",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFromAToB",
                table: "PathTrips");

            migrationBuilder.AddColumn<bool>(
                name: "IsFromAToB",
                table: "Paths",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
