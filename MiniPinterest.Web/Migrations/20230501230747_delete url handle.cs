using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class deleteurlhandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Pins");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Pins",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
