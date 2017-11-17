using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weddingPlanner.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wedding_users_userID",
                table: "wedding");

            migrationBuilder.RenameColumn(
                name: "userID",
                table: "wedding",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_wedding_userID",
                table: "wedding",
                newName: "IX_wedding_userId");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "wedding",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "wedding",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_wedding_users_userId",
                table: "wedding",
                column: "userId",
                principalTable: "users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_wedding_users_userId",
                table: "wedding");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "wedding",
                newName: "userID");

            migrationBuilder.RenameIndex(
                name: "IX_wedding_userId",
                table: "wedding",
                newName: "IX_wedding_userID");

            migrationBuilder.AlterColumn<int>(
                name: "userID",
                table: "wedding",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "wedding",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_wedding_users_userID",
                table: "wedding",
                column: "userID",
                principalTable: "users",
                principalColumn: "userID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
