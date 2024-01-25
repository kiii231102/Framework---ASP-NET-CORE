using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Musclegym.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
    name: "NoHp",
    table: "Member",
    nullable: true,
    oldClrType: typeof(int));

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
