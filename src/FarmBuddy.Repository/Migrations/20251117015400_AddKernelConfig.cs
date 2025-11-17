using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddKernelConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "SystemSetting",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "配置值");

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 7,
                column: "Key",
                value: "KernelConfig:AiModelType");

            migrationBuilder.InsertData(
                table: "SystemSetting",
                columns: new[] { "Id", "Description", "Key", "Value" },
                values: new object[,]
                {
                    { 10, "系統提示詞", "KernelConfig:SystemMessage", "YOUR_SystemMessage" },
                    { 11, "助手提示詞", "KernelConfig:AssistantMessage", "YOUR_AssistantMessage" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "SystemSetting",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "配置值",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000,
                oldComment: "配置值");

            migrationBuilder.UpdateData(
                table: "SystemSetting",
                keyColumn: "Id",
                keyValue: 7,
                column: "Key",
                value: "AiModelType");
        }
    }
}
