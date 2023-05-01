using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReview.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdFromJoinTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonOwners");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PokemonCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PokemonOwners",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "PokemonCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
