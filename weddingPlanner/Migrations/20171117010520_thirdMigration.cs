using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace weddingPlanner.Migrations
{
    public partial class thirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userid",
                table: "users",
                newName: "userID");

            migrationBuilder.RenameColumn(
                name: "createTime",
                table: "users",
                newName: "createAt");

            migrationBuilder.CreateTable(
                name: "wedding",
                columns: table => new
                {
                    wedID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address = table.Column<string>(nullable: true),
                    createAt = table.Column<DateTime>(nullable: false),
                    createdBy = table.Column<int>(nullable: false),
                    husband = table.Column<string>(nullable: false),
                    userID = table.Column<int>(nullable: true),
                    wedDate = table.Column<DateTime>(nullable: false),
                    wife = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wedding", x => x.wedID);
                    table.ForeignKey(
                        name: "FK_wedding_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "guest",
                columns: table => new
                {
                    guestId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userID = table.Column<int>(nullable: false),
                    wedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guest", x => x.guestId);
                    table.ForeignKey(
                        name: "FK_guest_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guest_wedding_wedID",
                        column: x => x.wedID,
                        principalTable: "wedding",
                        principalColumn: "wedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guest_userID",
                table: "guest",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_guest_wedID",
                table: "guest",
                column: "wedID");

            migrationBuilder.CreateIndex(
                name: "IX_wedding_userID",
                table: "wedding",
                column: "userID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guest");

            migrationBuilder.DropTable(
                name: "wedding");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "users",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "createAt",
                table: "users",
                newName: "createTime");
        }
    }
}
