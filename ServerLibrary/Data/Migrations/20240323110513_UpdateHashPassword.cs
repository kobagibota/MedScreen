using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHashPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("3aedd1b8-4e79-4ae2-996f-d01f27149fb1"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4edb9200-95aa-439f-bcaf-a240773d2e87"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f58333d-ce81-4742-addc-54e2903c3f89"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3aedd1b8-4e79-4ae2-996f-d01f27149fb1"), new Guid("ec8e188e-6ff1-40ee-8245-7e1e88bf91b9") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("ec8e188e-6ff1-40ee-8245-7e1e88bf91b9"));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("05657df1-8a9e-4376-bd4b-c826c6370275"), null, "Quản lý", "Manager", null },
                    { new Guid("1e079928-89ae-4f28-9956-dbe25a8a3c08"), null, "Người dùng", "User", null },
                    { new Guid("49d08331-6e86-495e-8715-c5cc1a249491"), null, "Quản trị hệ thống", "Administrator", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("49d08331-6e86-495e-8715-c5cc1a249491"), new Guid("197dd8d3-f822-41d4-b54a-0488ef9680cd") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("197dd8d3-f822-41d4-b54a-0488ef9680cd"), 0, null, "560d0a86-d2e8-4966-843e-719af78b49f3", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "$2a$11$/g8NsPRlneRYBLjw23MWPecLyGqYPFBCzBp5xg8BLQTQwvnkZUyGS", "123456789", false, "fd424ce8-47aa-4a5d-ab45-059dbb41bd3f", false, null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("05657df1-8a9e-4376-bd4b-c826c6370275"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1e079928-89ae-4f28-9956-dbe25a8a3c08"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("49d08331-6e86-495e-8715-c5cc1a249491"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("49d08331-6e86-495e-8715-c5cc1a249491"), new Guid("197dd8d3-f822-41d4-b54a-0488ef9680cd") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("197dd8d3-f822-41d4-b54a-0488ef9680cd"));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("3aedd1b8-4e79-4ae2-996f-d01f27149fb1"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("4edb9200-95aa-439f-bcaf-a240773d2e87"), null, "Người dùng", "User", null },
                    { new Guid("7f58333d-ce81-4742-addc-54e2903c3f89"), null, "Quản lý", "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("3aedd1b8-4e79-4ae2-996f-d01f27149fb1"), new Guid("ec8e188e-6ff1-40ee-8245-7e1e88bf91b9") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("ec8e188e-6ff1-40ee-8245-7e1e88bf91b9"), 0, null, "093a4542-7436-4fe9-8f54-1d299604957d", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAELpK5q19iCbL52dJrW3rZCutGWQzlHqowna1z+DNQfETHGJWC5ykoAPfzswNXnelmw==", "123456789", false, "0f8bf042-ce95-48d9-9559-534d2baeb75c", false, null, "superadmin" });
        }
    }
}
