using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Update_LedgerRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionAccount",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionMethod",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionStatus",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "LedgerRecords",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionAccount",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionMethod",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "LedgerRecords");
        }
    }
}
