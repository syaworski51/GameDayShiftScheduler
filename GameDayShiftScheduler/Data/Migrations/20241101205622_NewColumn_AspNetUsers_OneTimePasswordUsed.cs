using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDayShiftScheduler.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewColumn_AspNetUsers_OneTimePasswordUsed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "OneTimePasswordUsed",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OneTimePasswordUsed",
                table: "AspNetUsers");
        }
    }
}
