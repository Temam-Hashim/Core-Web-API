using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21c27ecd-cda1-4b3f-9e05-1ccb61c60608");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50e4931a-59a8-4413-8e05-6a7ffaa8af79");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5d90bf10-5b26-4edb-b039-f33795c6d40d", null, "user", "USER" },
                    { "c33ca8a1-fc6a-4d7f-b3a5-80a80787432c", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d90bf10-5b26-4edb-b039-f33795c6d40d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c33ca8a1-fc6a-4d7f-b3a5-80a80787432c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21c27ecd-cda1-4b3f-9e05-1ccb61c60608", null, "user", "USER" },
                    { "50e4931a-59a8-4413-8e05-6a7ffaa8af79", null, "admin", "ADMIN" }
                });
        }
    }
}
