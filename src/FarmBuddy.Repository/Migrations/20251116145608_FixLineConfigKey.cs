using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class FixLineConfigKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 8,
                column: "Key",
                value: "LineConfig:ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 8,
                column: "Key",
                value: "LineConfig:ChannelID");
        }
    }
}
