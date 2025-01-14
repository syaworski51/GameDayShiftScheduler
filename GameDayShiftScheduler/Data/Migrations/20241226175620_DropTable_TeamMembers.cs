using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDayShiftScheduler.Data.Migrations
{
    /// <inheritdoc />
    public partial class DropTable_TeamMembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_UserId",
                table: "ScheduledShifts");

            migrationBuilder.DropTable(
                name: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ScheduledShifts",
                newName: "TeamMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledShifts_UserId",
                table: "ScheduledShifts",
                newName: "IX_ScheduledShifts_TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_TeamMemberId",
                table: "ScheduledShifts",
                column: "TeamMemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_TeamMemberId",
                table: "ScheduledShifts");

            migrationBuilder.RenameColumn(
                name: "TeamMemberId",
                table: "ScheduledShifts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledShifts_TeamMemberId",
                table: "ScheduledShifts",
                newName: "IX_ScheduledShifts_UserId");

            migrationBuilder.CreateTable(
                name: "TeamMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMembers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_UserId",
                table: "ScheduledShifts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
