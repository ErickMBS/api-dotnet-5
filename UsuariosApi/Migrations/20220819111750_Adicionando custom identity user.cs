using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class Adicionandocustomidentityuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "AspNetUsers",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "04c3f0aa-9032-46ef-a5c9-eb0a30266586");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "8b3644f6-ab8b-444c-9fa7-f3be64dc5670");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9e95f02-5b50-4fba-8710-d47b77bb438f", "AQAAAAEAACcQAAAAEPdYlWqP3tyftl58sBzx+ML6/NawR6OAQQZBaiJ32yCwnSN0oOMxjCPGqR6Vitn4Lw==", "0da59365-cf50-4a9a-b99a-37f307dd85ed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997,
                column: "ConcurrencyStamp",
                value: "c59e1e56-f829-48a2-9a2a-6b8ae8fb83cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "2affa5e2-a42b-43e4-a4a3-fb4895ae32be");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1d71b69-e48a-4374-8a7a-561fc26d60cc", "AQAAAAEAACcQAAAAECJMhiiZR+Mi+UiZr24ca5MlPLUpSIkeKoNBf1Cs4pDw834glKCfYuONyLBEdHa8VQ==", "e4ff5269-88d9-42df-b125-1f87efd7e57d" });
        }
    }
}
