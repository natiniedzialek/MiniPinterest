using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class propertychanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedDate",
                table: "Pins",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Boards",
                newName: "AuthorId");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Pins",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Pins");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Pins",
                newName: "PublishedDate");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Boards",
                newName: "UserId");
        }
    }
}
