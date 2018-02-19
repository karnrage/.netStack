using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CNetwork.Migrations
{
    public partial class YourMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    idUser = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    idFriends = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UsersidUser = table.Column<int>(nullable: true),
                    idFriend = table.Column<int>(nullable: false),
                    idUser = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.idFriends);
                    table.ForeignKey(
                        name: "FK_Friends_Users_UsersidUser",
                        column: x => x.UsersidUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_Users_idUser",
                        column: x => x.idUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invite",
                columns: table => new
                {
                    idInvite = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccepteridUser = table.Column<int>(nullable: true),
                    FriendsidFriends = table.Column<int>(nullable: true),
                    RequesteridUser = table.Column<int>(nullable: true),
                    idAccepter = table.Column<int>(nullable: false),
                    idRequester = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invite", x => x.idInvite);
                    table.ForeignKey(
                        name: "FK_Invite_Users_AccepteridUser",
                        column: x => x.AccepteridUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invite_Friends_FriendsidFriends",
                        column: x => x.FriendsidFriends,
                        principalTable: "Friends",
                        principalColumn: "idFriends",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invite_Users_RequesteridUser",
                        column: x => x.RequesteridUser,
                        principalTable: "Users",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_UsersidUser",
                table: "Friends",
                column: "UsersidUser");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_idUser",
                table: "Friends",
                column: "idUser");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_AccepteridUser",
                table: "Invite",
                column: "AccepteridUser");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_FriendsidFriends",
                table: "Invite",
                column: "FriendsidFriends");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_RequesteridUser",
                table: "Invite",
                column: "RequesteridUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invite");

            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
