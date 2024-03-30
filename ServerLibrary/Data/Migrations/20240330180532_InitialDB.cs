using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.RoleId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Laboratories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LabName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LabStatus = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Methods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Methods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QCActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCActions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Standards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StandardName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Standards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StrainGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrainGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LaboratoryId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_Laboratories_LaboratoryId",
                        column: x => x.LaboratoryId,
                        principalTable: "Laboratories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QCProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    QCName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Hide = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QCProfiles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QCProfiles_Laboratories_LabId",
                        column: x => x.LabId,
                        principalTable: "Laboratories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QCProfiles_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MethodId = table.Column<int>(type: "int", nullable: false),
                    SupplyName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplies_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Strains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    StrainName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strains", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Strains_StrainGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "StrainGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestProfiles_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestProfiles_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestProfiles_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestQCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestTypeId = table.Column<int>(type: "int", nullable: false),
                    TestQCName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestQCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestQCs_TestTypes_TestTypeId",
                        column: x => x.TestTypeId,
                        principalTable: "TestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    LogAction = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppLogs_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QCs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QCProfileId = table.Column<int>(type: "int", nullable: false),
                    QCDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReQCId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QCs_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QCs_Laboratories_LabId",
                        column: x => x.LabId,
                        principalTable: "Laboratories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QCs_QCProfiles_QCProfileId",
                        column: x => x.QCProfileId,
                        principalTable: "QCProfiles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_QCs_QCs_ReQCId",
                        column: x => x.ReQCId,
                        principalTable: "QCs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LotSupplies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyId = table.Column<int>(type: "int", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotSupplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotSupplies_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupplyProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplyId = table.Column<int>(type: "int", nullable: false),
                    QCProfileId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true),
                    InUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplyProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplyProfiles_QCProfiles_QCProfileId",
                        column: x => x.QCProfileId,
                        principalTable: "QCProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SupplyProfiles_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StrainTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    StrainId = table.Column<int>(type: "int", nullable: false),
                    InUse = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrainTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrainTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrainTypes_Strains_StrainId",
                        column: x => x.StrainId,
                        principalTable: "Strains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LotTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestQCId = table.Column<int>(type: "int", nullable: false),
                    LotNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ExpDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Default = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotTests_TestQCs_TestQCId",
                        column: x => x.TestQCId,
                        principalTable: "TestQCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandardDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    MethodId = table.Column<int>(type: "int", nullable: false),
                    StandardId = table.Column<int>(type: "int", nullable: false),
                    TestQCId = table.Column<int>(type: "int", nullable: false),
                    StrainId = table.Column<int>(type: "int", nullable: false),
                    Concentration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Threshold = table.Column<int>(type: "int", nullable: false),
                    LimitMin = table.Column<float>(type: "real", nullable: true),
                    LimitMax = table.Column<float>(type: "real", nullable: true),
                    Normal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qualitative = table.Column<bool>(type: "bit", nullable: true),
                    ResultType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardDetails_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardDetails_Methods_MethodId",
                        column: x => x.MethodId,
                        principalTable: "Methods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardDetails_Standards_StandardId",
                        column: x => x.StandardId,
                        principalTable: "Standards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardDetails_Strains_StrainId",
                        column: x => x.StrainId,
                        principalTable: "Strains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StandardDetails_TestQCs_TestQCId",
                        column: x => x.TestQCId,
                        principalTable: "TestQCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UseWiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCId = table.Column<int>(type: "int", nullable: false),
                    SupplyId = table.Column<int>(type: "int", nullable: false),
                    LotSupplyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseWiths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UseWiths_LotSupplies_LotSupplyId",
                        column: x => x.LotSupplyId,
                        principalTable: "LotSupplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UseWiths_QCs_QCId",
                        column: x => x.QCId,
                        principalTable: "QCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UseWiths_Supplies_SupplyId",
                        column: x => x.SupplyId,
                        principalTable: "Supplies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QCProfileDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCProfileId = table.Column<int>(type: "int", nullable: false),
                    StandardDetailId = table.Column<int>(type: "int", nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QCProfileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QCProfileDetails_QCProfiles_QCProfileId",
                        column: x => x.QCProfileId,
                        principalTable: "QCProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QCProfileDetails_StandardDetails_StandardDetailId",
                        column: x => x.StandardDetailId,
                        principalTable: "StandardDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QCId = table.Column<int>(type: "int", nullable: false),
                    StandardDetailId = table.Column<int>(type: "int", nullable: false),
                    LotTestId = table.Column<int>(type: "int", nullable: true),
                    QCResult = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evaluate = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_AppUsers_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_LotTests_LotTestId",
                        column: x => x.LotTestId,
                        principalTable: "LotTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_QCs_QCId",
                        column: x => x.QCId,
                        principalTable: "QCs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Results_StandardDetails_StandardDetailId",
                        column: x => x.StandardDetailId,
                        principalTable: "StandardDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("30be6250-de6a-4c19-826f-f145d1ffbb47"), null, "Quản lý", "Manager", null },
                    { new Guid("6e2cb26b-707b-44a1-906d-13037799e7a5"), null, "Người dùng", "User", null },
                    { new Guid("f50888b1-b708-47f6-b73c-6715fed033c4"), null, "Quản trị hệ thống", "Administrator", null }
                });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("f50888b1-b708-47f6-b73c-6715fed033c4"), new Guid("29dbbd3d-6077-4ba8-9406-72855ba1e52f") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "IpAddress", "LabId", "LaboratoryId", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserAgent", "UserName" },
                values: new object[] { new Guid("29dbbd3d-6077-4ba8-9406-72855ba1e52f"), 0, null, "dc4768dc-bed3-426f-bf3a-6e8dac50bcba", "superadmin@gmail.com", false, "Hoằng", null, 0, null, "Nguyễn Tấn", false, null, "SUPERADMIN@GMAIL.COM", "SUPERADMIN", "$2a$11$6rRiMGzDOgc2/OZ7XK6qiuL3bF02ABwc.YrOw8/52oArru6dulLq6", "123456789", false, "b075807a-edda-47c2-9bba-c5dd20691e6a", false, null, "superadmin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Định danh" },
                    { 2, "Thử nghiệm" },
                    { 3, "Kháng sinh đồ" }
                });

            migrationBuilder.InsertData(
                table: "Laboratories",
                columns: new[] { "Id", "Address", "Email", "LabName", "LabStatus", "Logo", "OrganizationName", "PhoneNumber" },
                values: new object[] { 1, "315 Nguyễn Văn Linh - An Khánh - Ninh Kiều - Tp. Cần Thơ", null, "Khoa Xét Nghiệm", 1, null, "BVĐKTƯ Cần Thơ", null });

            migrationBuilder.InsertData(
                table: "Methods",
                columns: new[] { "Id", "MethodName" },
                values: new object[,]
                {
                    { 1, "Thủ công" },
                    { 2, "Tự động trên máy Vitek" },
                    { 3, "Tự động trên máy BD" }
                });

            migrationBuilder.InsertData(
                table: "StrainGroups",
                columns: new[] { "Id", "GroupName" },
                values: new object[,]
                {
                    { 1, "Gram âm" },
                    { 2, "Gram dương" },
                    { 3, "Vi nấm" }
                });

            migrationBuilder.InsertData(
                table: "TestTypes",
                columns: new[] { "Id", "TypeName", "Unit" },
                values: new object[,]
                {
                    { 1, "Kháng sinh đĩa giấy", "mm" },
                    { 2, "Kháng sinh E-test", null },
                    { 3, "Kháng sinh tự động", "µg/ml" },
                    { 4, "Đĩa thử nghiệm", "mm" },
                    { 5, "Định danh tự động", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLogs_UserId",
                table: "AppLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_LaboratoryId",
                table: "AppUsers",
                column: "LaboratoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserName",
                table: "AppUsers",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Laboratories_OrganizationName",
                table: "Laboratories",
                column: "OrganizationName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LotSupplies_SupplyId",
                table: "LotSupplies",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_LotTests_TestQCId",
                table: "LotTests",
                column: "TestQCId");

            migrationBuilder.CreateIndex(
                name: "IX_Methods_MethodName",
                table: "Methods",
                column: "MethodName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QCActions_ActionName",
                table: "QCActions",
                column: "ActionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QCProfileDetails_QCProfileId",
                table: "QCProfileDetails",
                column: "QCProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfileDetails_StandardDetailId",
                table: "QCProfileDetails",
                column: "StandardDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfiles_CategoryId",
                table: "QCProfiles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfiles_LabId",
                table: "QCProfiles",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfiles_MethodId",
                table: "QCProfiles",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_QCProfiles_QCName",
                table: "QCProfiles",
                column: "QCName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QCs_LabId",
                table: "QCs",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_QCs_QCProfileId",
                table: "QCs",
                column: "QCProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QCs_ReQCId",
                table: "QCs",
                column: "ReQCId");

            migrationBuilder.CreateIndex(
                name: "IX_QCs_UserId",
                table: "QCs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_LotTestId",
                table: "Results",
                column: "LotTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_QCId",
                table: "Results",
                column: "QCId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_StandardDetailId",
                table: "Results",
                column: "StandardDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_UpdatedByUserId",
                table: "Results",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardDetails_CategoryId",
                table: "StandardDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardDetails_MethodId",
                table: "StandardDetails",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardDetails_StandardId",
                table: "StandardDetails",
                column: "StandardId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardDetails_StrainId",
                table: "StandardDetails",
                column: "StrainId");

            migrationBuilder.CreateIndex(
                name: "IX_StandardDetails_TestQCId",
                table: "StandardDetails",
                column: "TestQCId");

            migrationBuilder.CreateIndex(
                name: "IX_Standards_StandardName",
                table: "Standards",
                column: "StandardName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StrainGroups_GroupName",
                table: "StrainGroups",
                column: "GroupName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Strains_GroupId",
                table: "Strains",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Strains_StrainName",
                table: "Strains",
                column: "StrainName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StrainTypes_CategoryId",
                table: "StrainTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StrainTypes_StrainId",
                table: "StrainTypes",
                column: "StrainId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_MethodId",
                table: "Supplies",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplies_SupplyName",
                table: "Supplies",
                column: "SupplyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplyProfiles_QCProfileId",
                table: "SupplyProfiles",
                column: "QCProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplyProfiles_SupplyId",
                table: "SupplyProfiles",
                column: "SupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_TestProfiles_CategoryId",
                table: "TestProfiles",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestProfiles_MethodId",
                table: "TestProfiles",
                column: "MethodId");

            migrationBuilder.CreateIndex(
                name: "IX_TestProfiles_TestTypeId",
                table: "TestProfiles",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQCs_TestQCName",
                table: "TestQCs",
                column: "TestQCName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestQCs_TestTypeId",
                table: "TestQCs",
                column: "TestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TestTypes_TypeName",
                table: "TestTypes",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UseWiths_LotSupplyId",
                table: "UseWiths",
                column: "LotSupplyId");

            migrationBuilder.CreateIndex(
                name: "IX_UseWiths_QCId",
                table: "UseWiths",
                column: "QCId");

            migrationBuilder.CreateIndex(
                name: "IX_UseWiths_SupplyId",
                table: "UseWiths",
                column: "SupplyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLogs");

            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "QCActions");

            migrationBuilder.DropTable(
                name: "QCProfileDetails");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "StrainTypes");

            migrationBuilder.DropTable(
                name: "SupplyProfiles");

            migrationBuilder.DropTable(
                name: "TestProfiles");

            migrationBuilder.DropTable(
                name: "UseWiths");

            migrationBuilder.DropTable(
                name: "LotTests");

            migrationBuilder.DropTable(
                name: "StandardDetails");

            migrationBuilder.DropTable(
                name: "LotSupplies");

            migrationBuilder.DropTable(
                name: "QCs");

            migrationBuilder.DropTable(
                name: "Standards");

            migrationBuilder.DropTable(
                name: "Strains");

            migrationBuilder.DropTable(
                name: "TestQCs");

            migrationBuilder.DropTable(
                name: "Supplies");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "QCProfiles");

            migrationBuilder.DropTable(
                name: "StrainGroups");

            migrationBuilder.DropTable(
                name: "TestTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Laboratories");

            migrationBuilder.DropTable(
                name: "Methods");
        }
    }
}
