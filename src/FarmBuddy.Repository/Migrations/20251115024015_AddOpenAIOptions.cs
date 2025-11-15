using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddOpenAIOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "SystemSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "配置值");

            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "Description", "Key", "Value" },
                values: new object[,]
                {
                    { 3, null, "OpenAIOption:ChatModelId", "gpt-4" },
                    { 4, null, "OpenAIOption:ApiKey", "YOUR_API_KEY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "SystemSetting",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "配置值");
        }
    }
}
