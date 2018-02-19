using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CNetwork.Migrations
{
    public partial class INVERSEPROPERTY : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Users_AccepteridUser",
                table: "Invite");

            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Users_RequesteridUser",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_AccepteridUser",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_RequesteridUser",
                table: "Invite");

            migrationBuilder.DropColumn(
                name: "AccepteridUser",
                table: "Invite");

            migrationBuilder.DropColumn(
                name: "RequesteridUser",
                table: "Invite");

            migrationBuilder.RenameColumn(
                name: "idRequester",
                table: "Invite",
                newName: "RequesterId");

            migrationBuilder.RenameColumn(
                name: "idAccepter",
                table: "Invite",
                newName: "AccepterId");

            migrationBuilder.RenameColumn(
                name: "idInvite",
                table: "Invite",
                newName: "InviteId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_AccepterId",
                table: "Invite",
                column: "AccepterId");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_RequesterId",
                table: "Invite",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Users_AccepterId",
                table: "Invite",
                column: "AccepterId",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Users_RequesterId",
                table: "Invite",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Users_AccepterId",
                table: "Invite");

            migrationBuilder.DropForeignKey(
                name: "FK_Invite_Users_RequesterId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_AccepterId",
                table: "Invite");

            migrationBuilder.DropIndex(
                name: "IX_Invite_RequesterId",
                table: "Invite");

            migrationBuilder.RenameColumn(
                name: "RequesterId",
                table: "Invite",
                newName: "idRequester");

            migrationBuilder.RenameColumn(
                name: "AccepterId",
                table: "Invite",
                newName: "idAccepter");

            migrationBuilder.RenameColumn(
                name: "InviteId",
                table: "Invite",
                newName: "idInvite");

            migrationBuilder.AddColumn<int>(
                name: "AccepteridUser",
                table: "Invite",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequesteridUser",
                table: "Invite",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invite_AccepteridUser",
                table: "Invite",
                column: "AccepteridUser");

            migrationBuilder.CreateIndex(
                name: "IX_Invite_RequesteridUser",
                table: "Invite",
                column: "RequesteridUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Users_AccepteridUser",
                table: "Invite",
                column: "AccepteridUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invite_Users_RequesteridUser",
                table: "Invite",
                column: "RequesteridUser",
                principalTable: "Users",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
