using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    public partial class AddedOwnerAccountInRecipient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerAccount",
                table: "Recipients",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerAccount",
                table: "Recipients");
        }
    }
}
