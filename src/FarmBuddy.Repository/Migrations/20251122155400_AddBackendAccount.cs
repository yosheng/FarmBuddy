using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddBackendAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackendAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "主鍵")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "使用者名稱"),
                    PasswordHash = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false, comment: "密碼雜湊值"),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, comment: "顯示名稱"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true, comment: "是否啟用"),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "最後登入時間"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()", comment: "建立時間")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackendAccounts", x => x.Id);
                },
                comment: "後端帳戶");

            migrationBuilder.CreateIndex(
                name: "IX_BackendAccount_Username",
                table: "BackendAccounts",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackendAccounts");
        }
    }
}
