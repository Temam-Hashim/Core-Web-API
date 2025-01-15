using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAllPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3dc6b603-a99f-44bf-b002-a7af02e552de");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "be8ed69f-95f2-408c-8fe4-cc0ede2ba6b7");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "UserStock",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "663cc9c7-13f2-48fb-bbbe-29af9b3850a8", null, "admin", "ADMIN" },
                    { "bd064989-0d5b-4beb-ae42-5fcf2048d6c7", null, "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "663cc9c7-13f2-48fb-bbbe-29af9b3850a8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd064989-0d5b-4beb-ae42-5fcf2048d6c7");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3dc6b603-a99f-44bf-b002-a7af02e552de", null, "user", "USER" },
                    { "be8ed69f-95f2-408c-8fe4-cc0ede2ba6b7", null, "admin", "ADMIN" }
                });
        }
    }
}
