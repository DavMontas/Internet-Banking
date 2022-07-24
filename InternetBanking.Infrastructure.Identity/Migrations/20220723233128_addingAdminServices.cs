using Microsoft.EntityFrameworkCore.Migrations;

namespace InternetBanking.Infrastructure.Identity.Migrations
{
    public partial class addingAdminServices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdCard",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                schema: "Identity",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TypeUser",
                schema: "Identity",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdCard",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TypeUser",
                schema: "Identity",
                table: "Users");
        }
    }
}
