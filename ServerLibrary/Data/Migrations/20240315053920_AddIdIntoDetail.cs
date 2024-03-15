using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIdIntoDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StrainTypes",
                table: "StrainTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QCProfileDetails",
                table: "QCProfileDetails");

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

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "StrainTypes",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "QCProfileDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StrainTypes",
                table: "StrainTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QCProfileDetails",
                table: "QCProfileDetails",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_StrainTypes_StrainId",
                table: "StrainTypes",
                column: "StrainId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfileDetails_QCProfileId",
                table: "QCProfileDetails",
                column: "QCProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StrainTypes",
                table: "StrainTypes");

            migrationBuilder.DropIndex(
                name: "IX_StrainTypes_StrainId",
                table: "StrainTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QCProfileDetails",
                table: "QCProfileDetails");

            migrationBuilder.DropIndex(
                name: "IX_QCProfileDetails_QCProfileId",
                table: "QCProfileDetails");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StrainTypes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "QCProfileDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StrainTypes",
                table: "StrainTypes",
                columns: new[] { "StrainId", "CategoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_QCProfileDetails",
                table: "QCProfileDetails",
                columns: new[] { "QCProfileId", "StandardDetailId" });

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
    }
}
