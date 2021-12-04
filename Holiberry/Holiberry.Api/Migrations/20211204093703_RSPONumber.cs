using Microsoft.EntityFrameworkCore.Migrations;

namespace Holiberry.Api.Migrations
{
    public partial class RSPONumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberRSPO",
                table: "Schools",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberRSPO",
                table: "Schools");
        }
    }
}
