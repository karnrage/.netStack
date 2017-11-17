using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace weddingPlanner.Migrations
{
    public partial class sixthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "wedding");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "createdBy",
                table: "wedding",
                nullable: false,
                defaultValue: 0);
        }
    }
}
