using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IkhsanovAPI.Migrations
{
    /// <inheritdoc />
    public partial class firstblazorapi2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageUrl",
                table: "Movies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageUrl",
                table: "Movies");
        }
    }
}
