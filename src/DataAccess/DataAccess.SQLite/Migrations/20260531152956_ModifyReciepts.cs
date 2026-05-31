using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class ModifyReciepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Receipts",
                newName: "Phone");

            migrationBuilder.AddColumn<string>(
                name: "CompanyEmail",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: "renting@domains24.ru");

            migrationBuilder.AddColumn<string>(
                name: "CompanyPhone",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: "+7 (xxx) xxx-xx-xx");

            migrationBuilder.AddColumn<int>(
                name: "INN",
                table: "Receipts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 1234567890);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyEmail",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CompanyPhone",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "INN",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Receipts",
                newName: "PhoneNumber");
        }
    }
}
