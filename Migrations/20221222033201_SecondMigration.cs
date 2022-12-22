using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomFacts.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Facts",
                newName: "fact");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Facts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Facts",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Facts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Associations",
                columns: table => new
                {
                    AssociationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FactId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associations", x => x.AssociationId);
                    table.ForeignKey(
                        name: "FK_Associations_Facts_FactId",
                        column: x => x.FactId,
                        principalTable: "Facts",
                        principalColumn: "FactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Associations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Facts_UserId",
                table: "Facts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_FactId",
                table: "Associations",
                column: "FactId");

            migrationBuilder.CreateIndex(
                name: "IX_Associations_UserId",
                table: "Associations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facts_Users_UserId",
                table: "Facts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facts_Users_UserId",
                table: "Facts");

            migrationBuilder.DropTable(
                name: "Associations");

            migrationBuilder.DropIndex(
                name: "IX_Facts_UserId",
                table: "Facts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Facts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Facts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Facts");

            migrationBuilder.RenameColumn(
                name: "fact",
                table: "Facts",
                newName: "Text");
        }
    }
}
