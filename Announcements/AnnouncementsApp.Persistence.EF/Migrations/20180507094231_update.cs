using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnnouncementsApp.Persistence.EF.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Announcements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_OwnerId",
                table: "Announcements",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_OwnerId",
                table: "Announcements",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_OwnerId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_OwnerId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Announcements");
        }
    }
}
