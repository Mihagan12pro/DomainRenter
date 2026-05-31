using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class ModifyReciepts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: "г. N, ул. n, д. 1");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: "ООО «Домены24»");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyAddress",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Receipts");
        }
    }
}
