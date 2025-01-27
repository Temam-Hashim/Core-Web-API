using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProfilePictureToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20e0a0a0-2d4e-4822-9b7c-f8b8e75c9871");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5d72dd0-7f76-432d-840f-f1d957136c45");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5752cff8-abe2-4597-9bf8-ef4a8f0808e9", null, "user", "USER" },
                    { "fdbe19a0-eaed-4ad5-af9c-70044f68d924", null, "admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5752cff8-abe2-4597-9bf8-ef4a8f0808e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdbe19a0-eaed-4ad5-af9c-70044f68d924");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20e0a0a0-2d4e-4822-9b7c-f8b8e75c9871", null, "admin", "ADMIN" },
                    { "d5d72dd0-7f76-432d-840f-f1d957136c45", null, "user", "USER" }
                });
        }
    }
}
