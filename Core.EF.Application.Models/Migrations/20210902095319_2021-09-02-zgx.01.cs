using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.EF.Application.Models.Migrations
{
    public partial class _20210902zgx01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERINFOS",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CREATETIME = table.Column<DateTime>(nullable: false),
                    REMOVED = table.Column<bool>(nullable: false),
                    USERCDE = table.Column<string>(maxLength: 20, nullable: false),
                    PASSWORD = table.Column<string>(maxLength: 100, nullable: false),
                    USERNAME = table.Column<string>(maxLength: 50, nullable: false),
                    EMAIL = table.Column<string>(maxLength: 100, nullable: true),
                    ISENABLED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERINFOS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USERINFOS");
        }
    }
}
