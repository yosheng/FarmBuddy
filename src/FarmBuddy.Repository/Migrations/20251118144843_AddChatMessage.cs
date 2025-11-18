using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmBuddy.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddChatMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "主鍵")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "用戶ID (對應 Line UserId)"),
                    Role = table.Column<int>(type: "int", nullable: false, comment: "角色：User (用戶) 或 Assistant (AI)"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "聊天內容"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "建立時間 (用於排序)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessages", x => x.Id);
                },
                comment: "聊天紀錄");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_UserId",
                table: "ChatMessages",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMessages");
        }
    }
}
