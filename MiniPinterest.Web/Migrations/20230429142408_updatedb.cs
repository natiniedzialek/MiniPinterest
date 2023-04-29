using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniPinterest.Web.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardPin_Pins_PinsID",
                table: "BoardPin");

            migrationBuilder.DropTable(
                name: "PinTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.RenameColumn(
                name: "AuthorID",
                table: "Pins",
                newName: "AuthorId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Pins",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PinsID",
                table: "BoardPin",
                newName: "PinsId");

            migrationBuilder.RenameIndex(
                name: "IX_BoardPin_PinsID",
                table: "BoardPin",
                newName: "IX_BoardPin_PinsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardPin_Pins_PinsId",
                table: "BoardPin",
                column: "PinsId",
                principalTable: "Pins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardPin_Pins_PinsId",
                table: "BoardPin");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Pins",
                newName: "AuthorID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Pins",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PinsId",
                table: "BoardPin",
                newName: "PinsID");

            migrationBuilder.RenameIndex(
                name: "IX_BoardPin_PinsId",
                table: "BoardPin",
                newName: "IX_BoardPin_PinsID");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PinTag",
                columns: table => new
                {
                    PinsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinTag", x => new { x.PinsID, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PinTag_Pins_PinsID",
                        column: x => x.PinsID,
                        principalTable: "Pins",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PinTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PinTag_TagsId",
                table: "PinTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardPin_Pins_PinsID",
                table: "BoardPin",
                column: "PinsID",
                principalTable: "Pins",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
