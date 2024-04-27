using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCodersWebApi.Migrations
{
    public partial class AddRefreshTokenFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rank_RankId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "06da5a10-b41c-496b-b4d5-d2749ca1214e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5e221a7-83fd-4894-9049-2d85f2fce688");

            migrationBuilder.AlterColumn<int>(
                name: "RankId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2910c011-5360-43cd-86e1-b3cc34c5fa6b", "4229aa7f-ecc4-4417-937f-9b66094fc940", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4fb07f9e-0ae8-4f43-9649-e639e668df59", "60b07ee8-1784-4794-99ec-592352e98784", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalSchema: "user",
                principalTable: "Gender",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rank_RankId",
                table: "AspNetUsers",
                column: "RankId",
                principalSchema: "user",
                principalTable: "Rank",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rank_RankId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2910c011-5360-43cd-86e1-b3cc34c5fa6b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fb07f9e-0ae8-4f43-9649-e639e668df59");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "RankId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "06da5a10-b41c-496b-b4d5-d2749ca1214e", "1af84cae-2931-43fd-a61f-4eda8f909904", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d5e221a7-83fd-4894-9049-2d85f2fce688", "363ad310-3c8d-4b29-a6cc-4c96fc05f9b3", "User", "USER" });

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Gender_GenderId",
                table: "AspNetUsers",
                column: "GenderId",
                principalSchema: "user",
                principalTable: "Gender",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rank_RankId",
                table: "AspNetUsers",
                column: "RankId",
                principalSchema: "user",
                principalTable: "Rank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
