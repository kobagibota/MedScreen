using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsUniqueForTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("4d6843db-6f24-423a-acea-ef3e793efb3c"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("500373c7-99e3-4ea4-bc96-194162304e8e"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("d14ce643-f244-470c-b7ea-239347058771"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("500373c7-99e3-4ea4-bc96-194162304e8e"), new Guid("38274e59-bd47-45ad-8316-a01ca7704250") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("38274e59-bd47-45ad-8316-a01ca7704250"));

            migrationBuilder.AlterColumn<string>(
                name: "TestQCName",
                table: "TestQCs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SupplyName",
                table: "Supplies",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StrainName",
                table: "Strains",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "MethodName",
                table: "Methods",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("40edc960-12a9-49b6-84ed-d7170adbda7c"), null, "Quản lý", "Manager", null },
                    { new Guid("417369a6-e37e-49cd-8ab8-e082fed8460f"), null, "Người dùng", "User", null },
                    { new Guid("db503110-2321-4702-9617-a5d692c4d4f2"), null, "Quản trị hệ thống", "Administrator", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("db503110-2321-4702-9617-a5d692c4d4f2"), new Guid("0ef7e5c6-5705-445e-a76b-29bf73e7d132") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("0ef7e5c6-5705-445e-a76b-29bf73e7d132"), 0, null, "abb3b4f9-68df-4a09-bd6e-3058ab39f24e", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEPcDxZKOLZaYkokY5XomWXPFumDpyQ5NJM/jdO/OHgiyyKvEKPrXo+JMKY5S/Ps+UQ==", "123456789", false, "ff98e932-9f5b-4f72-9806-2e26241fb222", false, null, "superadmin" });

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_TypeName",
                table: "TestTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestQCs_TestQCName",
                table: "TestQCs",
                column: "TestQCName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SupplyName",
                table: "Supplies",
                column: "SupplyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Strains_StrainName",
                table: "Strains",
                column: "StrainName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QCProfiles_QCName",
                table: "QCProfiles",
                column: "QCName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Methods_MethodName",
                table: "Methods",
                column: "MethodName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TestTypes_TypeName",
                table: "TestTypes");

            migrationBuilder.DropIndex(
                name: "IX_TestQCs_TestQCName",
                table: "TestQCs");

            migrationBuilder.DropIndex(
                name: "IX_Supplies_SupplyName",
                table: "Supplies");

            migrationBuilder.DropIndex(
                name: "IX_Strains_StrainName",
                table: "Strains");

            migrationBuilder.DropIndex(
                name: "IX_QCProfiles_QCName",
                table: "QCProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Methods_MethodName",
                table: "Methods");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("40edc960-12a9-49b6-84ed-d7170adbda7c"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("417369a6-e37e-49cd-8ab8-e082fed8460f"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("db503110-2321-4702-9617-a5d692c4d4f2"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("db503110-2321-4702-9617-a5d692c4d4f2"), new Guid("0ef7e5c6-5705-445e-a76b-29bf73e7d132") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("0ef7e5c6-5705-445e-a76b-29bf73e7d132"));

            migrationBuilder.AlterColumn<string>(
                name: "TestQCName",
                table: "TestQCs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SupplyName",
                table: "Supplies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StrainName",
                table: "Strains",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MethodName",
                table: "Methods",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("4d6843db-6f24-423a-acea-ef3e793efb3c"), null, "Quản lý", "Manager", null },
                    { new Guid("500373c7-99e3-4ea4-bc96-194162304e8e"), null, "Quản trị hệ thống", "Administrator", null },
                    { new Guid("d14ce643-f244-470c-b7ea-239347058771"), null, "Người dùng", "User", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("500373c7-99e3-4ea4-bc96-194162304e8e"), new Guid("38274e59-bd47-45ad-8316-a01ca7704250") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("38274e59-bd47-45ad-8316-a01ca7704250"), 0, null, "0298ae50-63ab-4895-b047-bde8840f5a5a", "superadmin@gmail.com", false, "Hoằng", null, 1, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "AQAAAAIAAYagAAAAEJ+5PMh5q65kQrxWwZAmAT8zZx0l95WkJAyQzzSe9q6WrsTJhqgQnoK4IW4gj404eg==", "123456789", false, "ab27b723-e166-4220-9fef-f45c1cb8d8d8", false, null, "superadmin" });
        }
    }
}
