using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class commentfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "PinComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "PinComments");
        }
    }
}
