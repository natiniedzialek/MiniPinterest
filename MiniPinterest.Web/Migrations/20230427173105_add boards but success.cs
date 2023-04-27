using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class addboardsbutsuccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Pins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pins_BoardId",
                table: "Pins",
                column: "BoardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pins_Boards_BoardId",
                table: "Pins",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pins_Boards_BoardId",
                table: "Pins");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Pins_BoardId",
                table: "Pins");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Pins");
        }
    }
}
