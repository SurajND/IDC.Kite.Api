using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDC.Kite.DataModel.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KeyIndicators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Indicator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyIndicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationalCompanies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OperationalCompanyName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationalCompanies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PermissionType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ProjectName = table.Column<string>(nullable: true),
                    OperationalCompanyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projects_OperationalCompanies_OperationalCompanyId",
                        column: x => x.OperationalCompanyId,
                        principalTable: "OperationalCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.PermissionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectKeyIndicators",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Percentage = table.Column<float>(nullable: false),
                    Actual = table.Column<float>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    KeyIndicatorId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectKeyIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectKeyIndicators_KeyIndicators_KeyIndicatorId",
                        column: x => x.KeyIndicatorId,
                        principalTable: "KeyIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectKeyIndicators_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectKeyIndicatorYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Value = table.Column<float>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    KeyIndicatorId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: false),
                    UpdatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectKeyIndicatorYears", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectKeyIndicatorYears_KeyIndicators_KeyIndicatorId",
                        column: x => x.KeyIndicatorId,
                        principalTable: "KeyIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectKeyIndicatorYears_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FName = table.Column<string>(nullable: true),
                    LName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Token = table.Column<Guid>(nullable: false),
                    OperationalCompanyId = table.Column<Guid>(nullable: true),
                    RoleId = table.Column<Guid>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    LastLogin = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_OperationalCompanies_OperationalCompanyId",
                        column: x => x.OperationalCompanyId,
                        principalTable: "OperationalCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectKeyIndicators_KeyIndicatorId",
                table: "ProjectKeyIndicators",
                column: "KeyIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectKeyIndicators_ProjectId",
                table: "ProjectKeyIndicators",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectKeyIndicatorYears_KeyIndicatorId",
                table: "ProjectKeyIndicatorYears",
                column: "KeyIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectKeyIndicatorYears_ProjectId",
                table: "ProjectKeyIndicatorYears",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_OperationalCompanyId",
                table: "Projects",
                column: "OperationalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleId",
                table: "RolePermission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OperationalCompanyId",
                table: "Users",
                column: "OperationalCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProjectId",
                table: "Users",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectKeyIndicators");

            migrationBuilder.DropTable(
                name: "ProjectKeyIndicatorYears");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "KeyIndicators");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "OperationalCompanies");
        }
    }
}
