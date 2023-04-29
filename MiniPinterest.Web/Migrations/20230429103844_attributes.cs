using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class attributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pins_Boards_BoardId",
                table: "Pins");

            migrationBuilder.DropIndex(
                name: "IX_Pins_BoardId",
                table: "Pins");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "BoardId",
                table: "Pins");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Boards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BoardPin",
                columns: table => new
                {
                    BoardsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PinsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPin", x => new { x.BoardsId, x.PinsID });
                    table.ForeignKey(
                        name: "FK_BoardPin_Boards_BoardsId",
                        column: x => x.BoardsId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardPin_Pins_PinsID",
                        column: x => x.PinsID,
                        principalTable: "Pins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardPin_PinsID",
                table: "BoardPin",
                column: "PinsID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardPin");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Boards");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "BoardId",
                table: "Pins",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
