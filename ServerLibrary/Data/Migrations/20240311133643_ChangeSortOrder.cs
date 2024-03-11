using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSortOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("0c6fbf31-d6ca-4b30-a6f8-f0ebc0e4a127"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("bcc99b0a-dd64-49c2-a52d-e6b6136129e4"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d30d9d57-9ebc-484a-affc-27b4fa2175a9"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("bcc99b0a-dd64-49c2-a52d-e6b6136129e4"), new Guid("fb7ea668-0faf-4abb-bc13-9443323b8225") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("fb7ea668-0faf-4abb-bc13-9443323b8225"));

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "Supplies");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "SupplyProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("13caaacb-c550-4842-ae74-5c96a41516da"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("697394bd-ec8c-43d4-b61b-2d4e327aede6"), null, "Quản lý", "Manager", null },
                    { new Guid("981c2565-2ce4-403a-a136-a62e74658c46"), null, "Người dùng", "User", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("13caaacb-c550-4842-ae74-5c96a41516da"), new Guid("bafa2a30-e702-42f4-826a-96380dfb802d") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("bafa2a30-e702-42f4-826a-96380dfb802d"), 0, null, "1fecb166-4a19-4f4d-81f3-dcc062921a3d", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEMzo5XkXnOoO4dWZp25yscdqkto/XlPqXLuecobQjBUkzP/ktZ8VZlWicCT1/gu2tA==", "123456789", false, "84a8455e-e4e3-4b3d-80be-0dc9ad8ebc32", false, null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("13caaacb-c550-4842-ae74-5c96a41516da"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("697394bd-ec8c-43d4-b61b-2d4e327aede6"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("981c2565-2ce4-403a-a136-a62e74658c46"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("13caaacb-c550-4842-ae74-5c96a41516da"), new Guid("bafa2a30-e702-42f4-826a-96380dfb802d") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("bafa2a30-e702-42f4-826a-96380dfb802d"));

            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "SupplyProfiles");

            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "Supplies",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("0c6fbf31-d6ca-4b30-a6f8-f0ebc0e4a127"), null, "Người dùng", "User", null },
                    { new Guid("bcc99b0a-dd64-49c2-a52d-e6b6136129e4"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("d30d9d57-9ebc-484a-affc-27b4fa2175a9"), null, "Quản lý", "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("bcc99b0a-dd64-49c2-a52d-e6b6136129e4"), new Guid("fb7ea668-0faf-4abb-bc13-9443323b8225") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("fb7ea668-0faf-4abb-bc13-9443323b8225"), 0, null, "358debdc-825b-4610-bdf1-d33a1c2fce14", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEDOFuRBJHX2JbLuiO0BlzFBZNVbq5MW92Z72l87U4c6ZFejXDB3jag30LLg7nxscmg==", "123456789", false, "be98d61f-d57b-457e-91cd-2be5aaf8230a", false, null, "superadmin" });
        }
    }
}
