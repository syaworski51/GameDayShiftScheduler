using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDayShiftScheduler.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewTable_Organizations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "Shifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "ScheduledShifts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganizationId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_OrganizationId",
                table: "Shifts",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledShifts_OrganizationId",
                table: "ScheduledShifts",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledShifts_Organizations_OrganizationId",
                table: "ScheduledShifts",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_Organizations_OrganizationId",
                table: "Shifts",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Organizations_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledShifts_Organizations_OrganizationId",
                table: "ScheduledShifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_Organizations_OrganizationId",
                table: "Shifts");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_OrganizationId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledShifts_OrganizationId",
                table: "ScheduledShifts");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_OrganizationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "ScheduledShifts");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "AspNetUsers");
        }
    }
}
