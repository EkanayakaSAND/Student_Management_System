using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbEnrolledModules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Student_Id = table.Column<int>(type: "INTEGER", nullable: false),
                    ModuleCode = table.Column<string>(type: "TEXT", nullable: false),
                    ModuleName = table.Column<string>(type: "TEXT", nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    Grade = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbEnrolledModules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DbModules",
                columns: table => new
                {
                    Mod_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Credit = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbModules", x => x.Mod_Id);
                });

            migrationBuilder.CreateTable(
                name: "DbStudents",
                columns: table => new
                {
                    Std_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    RegisteredDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    GurdianName = table.Column<string>(type: "TEXT", nullable: false),
                    GurdianPhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    GPA = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbStudents", x => x.Std_Id);
                });

            migrationBuilder.CreateTable(
                name: "DbUsers",
                columns: table => new
                {
                    Usr_Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    IsAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUsers", x => x.Usr_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbEnrolledModules");

            migrationBuilder.DropTable(
                name: "DbModules");

            migrationBuilder.DropTable(
                name: "DbStudents");

            migrationBuilder.DropTable(
                name: "DbUsers");
        }
    }
}
