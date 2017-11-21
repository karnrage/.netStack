using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace weddingPlanner.Migrations
{
    public partial class firstmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    createAt = table.Column<DateTime>(nullable: false),
                    email = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "weddings",
                columns: table => new
                {
                    wedID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    address = table.Column<string>(nullable: false),
                    createAt = table.Column<DateTime>(nullable: false),
                    husband = table.Column<string>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    wedDate = table.Column<DateTime>(nullable: false),
                    wife = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weddings", x => x.wedID);
                    table.ForeignKey(
                        name: "FK_weddings_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "guests",
                columns: table => new
                {
                    guestId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userID = table.Column<int>(nullable: false),
                    wedID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guests", x => x.guestId);
                    table.ForeignKey(
                        name: "FK_guests_users_userID",
                        column: x => x.userID,
                        principalTable: "users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_guests_weddings_wedID",
                        column: x => x.wedID,
                        principalTable: "weddings",
                        principalColumn: "wedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_guests_userID",
                table: "guests",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_guests_wedID",
                table: "guests",
                column: "wedID");

            migrationBuilder.CreateIndex(
                name: "IX_weddings_userId",
                table: "weddings",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "guests");

            migrationBuilder.DropTable(
                name: "weddings");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
