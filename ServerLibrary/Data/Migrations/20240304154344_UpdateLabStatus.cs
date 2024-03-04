using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLabStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1442ee19-193f-4629-b867-aea19a1f4e45"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("5bd5c2f7-a94c-4f02-aa06-b36430f730be"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("f706357f-8884-449c-89e7-94e903ee40bc"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("5bd5c2f7-a94c-4f02-aa06-b36430f730be"), new Guid("948f3047-5c1c-4e78-92c2-06128ded41b3") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("948f3047-5c1c-4e78-92c2-06128ded41b3"));

            migrationBuilder.AlterColumn<int>(
                name: "LabStatus",
                table: "Laboratories",
                type: "int",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("43ff56bc-a9c3-43df-b536-a13043eb657b"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("d4ebfbda-6c15-478b-8b7e-fb5b1cdafb78"), null, "Người dùng", "User", null },
                    { new Guid("f9986509-6f0b-4380-8a0f-4014df109c74"), null, "Quản lý", "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("43ff56bc-a9c3-43df-b536-a13043eb657b"), new Guid("6a1481f6-f9b0-41a6-9408-cbd6b87b1974") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("6a1481f6-f9b0-41a6-9408-cbd6b87b1974"), 0, null, "5d4aaa53-0245-484d-8669-b7ba42a05348", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEB5bnxT+V/F/p0Ds1QjwYrOk1D3iPJM/NWj1PQhktLyx57oHoJzskamRYJQgfgqYlw==", "123456789", false, "bf121144-3d2e-49ad-bca1-f98c9b2a0164", false, null, "superadmin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("43ff56bc-a9c3-43df-b536-a13043eb657b"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d4ebfbda-6c15-478b-8b7e-fb5b1cdafb78"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("f9986509-6f0b-4380-8a0f-4014df109c74"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("43ff56bc-a9c3-43df-b536-a13043eb657b"), new Guid("6a1481f6-f9b0-41a6-9408-cbd6b87b1974") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("6a1481f6-f9b0-41a6-9408-cbd6b87b1974"));

            migrationBuilder.AlterColumn<int>(
                name: "LabStatus",
                table: "Laboratories",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 1);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1442ee19-193f-4629-b867-aea19a1f4e45"), null, "Người dùng", "User", null },
                    { new Guid("5bd5c2f7-a94c-4f02-aa06-b36430f730be"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("f706357f-8884-449c-89e7-94e903ee40bc"), null, "Quản lý", "Manager", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("5bd5c2f7-a94c-4f02-aa06-b36430f730be"), new Guid("948f3047-5c1c-4e78-92c2-06128ded41b3") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("948f3047-5c1c-4e78-92c2-06128ded41b3"), 0, null, "8ac653c1-64b7-4cbe-bc24-a20073fe9c77", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEJv6Z0mUvAR8dIs06/uU6mcEl6+k1HgGljYr4znr/UFtEINKMmXYUSQvlTecy6RxVg==", "123456789", false, "06c3b09d-747b-4933-889f-761b45f61ae9", false, null, "superadmin" });
        }
    }
}
