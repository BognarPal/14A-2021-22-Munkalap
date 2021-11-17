using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace munkalap.service.Migrations
{
    public partial class Initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Failures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Issuer = table.Column<string>(maxLength: 100, nullable: false),
                    IssueTimeStamp = table.Column<DateTime>(nullable: false),
                    Room = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    AssignedId = table.Column<int>(nullable: true),
                    AssignTimeStamp = table.Column<DateTime>(nullable: true),
                    AssignComment = table.Column<string>(maxLength: 200, nullable: true),
                    WorkStarted = table.Column<DateTime>(nullable: true),
                    WorkFinished = table.Column<DateTime>(nullable: true),
                    FinishComment = table.Column<string>(maxLength: 200, nullable: true),
                    IsChecked = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Failures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Failures_Employee_AssignedId",
                        column: x => x.AssignedId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 1, false, "Béla" });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 2, false, "Géza" });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Name",
                table: "Employee",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Failures_AssignedId",
                table: "Failures",
                column: "AssignedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Failures");

            migrationBuilder.DropTable(
                name: "Employee");
        }
    }
}
