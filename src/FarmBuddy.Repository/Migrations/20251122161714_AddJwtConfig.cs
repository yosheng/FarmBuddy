using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddJwtConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "Description", "Key", "Value" },
                values: new object[,]
                {
                    { 12, "JWT頒發者", "JwtConfig:Issuer", "farm-buddy-api" },
                    { 13, "JWT客戶端", "JwtConfig:Audience", "farm-buddy-api" },
                    { 14, "JWT密鑰", "JwtConfig:Key", "u5w/8x+A1b2C3d4E5f6G7h8I9j0K1l2M3n4O5p6Q7r8=" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
