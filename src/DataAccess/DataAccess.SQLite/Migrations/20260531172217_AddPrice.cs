using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Receipts",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Receipts");
        }
    }
}
