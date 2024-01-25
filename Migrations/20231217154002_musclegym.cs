using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musclegym.Migrations
{
    /// <inheritdoc />
    public partial class musclegym : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TanggalTransaksi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoHp = table.Column<int>(type: "int", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JenisKelamin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Kelas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cabang = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");
        }
    }
}
