using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStockUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663cc9c7-13f2-48fb-bbbe-29af9b3850a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd064989-0d5b-4beb-ae42-5fcf2048d6c7");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserStock");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "63fb62e0-b60b-42bd-b3e4-fe86c5ebe8cb", null, "admin", "ADMIN" },
                    { "ab0205a9-ca3c-4a69-b4af-ed1dd41b8e04", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63fb62e0-b60b-42bd-b3e4-fe86c5ebe8cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ab0205a9-ca3c-4a69-b4af-ed1dd41b8e04");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserStock",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "663cc9c7-13f2-48fb-bbbe-29af9b3850a8", null, "admin", "ADMIN" },
                    { "bd064989-0d5b-4beb-ae42-5fcf2048d6c7", null, "user", "USER" }
                });
        }
    }
}
