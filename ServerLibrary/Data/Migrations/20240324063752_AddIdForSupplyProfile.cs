using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdForSupplyProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplyProfiles",
                table: "SupplyProfiles");

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SupplyProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplyProfiles",
                table: "SupplyProfiles",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("191d2401-ce54-4aec-89da-5cdb393558b4"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("5851e421-0a75-410a-9bc0-e8a5219b0ddf"), null, "Người dùng", "User", null },
                    { new Guid("a4205ee0-8d4b-4a1c-bba2-0fc5c81a79c8"), null, "Quản lý", "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("191d2401-ce54-4aec-89da-5cdb393558b4"), new Guid("827f5530-eda5-4262-b04f-77fcf56a1773") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("827f5530-eda5-4262-b04f-77fcf56a1773"), 0, null, "8c579124-1d4d-48d3-a8a9-b65d8b657be7", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "$2a$11$ZUM//0kRv7hp7aKm9NR.TOeDQm1O35z.Nao5v8VqFtyelSHDbOPtK", "123456789", false, "63459320-e64f-4b0a-a50d-72fc58229606", false, null, "superadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_SupplyProfiles_SupplyId",
                table: "SupplyProfiles",
                column: "SupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SupplyProfiles",
                table: "SupplyProfiles");

            migrationBuilder.DropIndex(
                name: "IX_SupplyProfiles_SupplyId",
                table: "SupplyProfiles");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("191d2401-ce54-4aec-89da-5cdb393558b4"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5851e421-0a75-410a-9bc0-e8a5219b0ddf"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a4205ee0-8d4b-4a1c-bba2-0fc5c81a79c8"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("191d2401-ce54-4aec-89da-5cdb393558b4"), new Guid("827f5530-eda5-4262-b04f-77fcf56a1773") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("827f5530-eda5-4262-b04f-77fcf56a1773"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SupplyProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SupplyProfiles",
                table: "SupplyProfiles",
                columns: new[] { "SupplyId", "QCProfileId" });

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
    }
}
