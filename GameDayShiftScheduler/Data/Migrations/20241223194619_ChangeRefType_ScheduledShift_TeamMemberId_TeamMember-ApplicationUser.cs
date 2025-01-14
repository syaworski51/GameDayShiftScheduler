using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDayShiftScheduler.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRefType_ScheduledShift_TeamMemberId_TeamMemberApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledShifts_TeamMembers_TeamMemberId",
                table: "ScheduledShifts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledShifts_TeamMemberId",
                table: "ScheduledShifts");

            migrationBuilder.DropColumn(
                name: "TeamMemberId",
                table: "ScheduledShifts");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ScheduledShifts",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledShifts_UserId",
                table: "ScheduledShifts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_UserId",
                table: "ScheduledShifts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledShifts_AspNetUsers_UserId",
                table: "ScheduledShifts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledShifts_UserId",
                table: "ScheduledShifts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ScheduledShifts");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamMemberId",
                table: "ScheduledShifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledShifts_TeamMemberId",
                table: "ScheduledShifts",
                column: "TeamMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledShifts_TeamMembers_TeamMemberId",
                table: "ScheduledShifts",
                column: "TeamMemberId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
