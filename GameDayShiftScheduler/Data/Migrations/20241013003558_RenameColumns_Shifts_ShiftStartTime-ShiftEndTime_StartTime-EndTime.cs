using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameDayShiftScheduler.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameColumns_Shifts_ShiftStartTimeShiftEndTime_StartTimeEndTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShiftStartTime",
                table: "Shifts",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "ShiftEndTime",
                table: "Shifts",
                newName: "EndTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Shifts",
                newName: "ShiftStartTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Shifts",
                newName: "ShiftEndTime");
        }
    }
}
