using Microsoft.EntityFrameworkCore.Migrations;

namespace UsuariosApi.Migrations
{
    public partial class CriarRoleRegular : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "2affa5e2-a42b-43e4-a4a3-fb4895ae32be");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 99997, "c59e1e56-f829-48a2-9a2a-6b8ae8fb83cf", "regular", "REGULAR" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1d71b69-e48a-4374-8a7a-561fc26d60cc", "AQAAAAEAACcQAAAAECJMhiiZR+Mi+UiZr24ca5MlPLUpSIkeKoNBf1Cs4pDw834glKCfYuONyLBEdHa8VQ==", "e4ff5269-88d9-42df-b125-1f87efd7e57d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99997);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 99999,
                column: "ConcurrencyStamp",
                value: "148d6330-9056-429e-ac16-b5c985b8fd8d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 99999,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "945a2e96-d464-4a54-a1c8-5f1dfaeea4ce", "AQAAAAEAACcQAAAAEFn9fQAVZ0IZYnK/NzPWpIXCAoradpRlLujF+74ub891XT4/s2d0nwmD8/Bf0X5I8Q==", "eb696425-c891-4b31-9cac-b502b8586be1" });
        }
    }
}
