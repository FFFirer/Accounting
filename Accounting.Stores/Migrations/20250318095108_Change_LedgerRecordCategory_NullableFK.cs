using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Change_LedgerRecordCategory_NullableFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "LedgerRecords",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords",
                column: "CategoryId",
                principalTable: "LedgerCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "LedgerRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords",
                column: "CategoryId",
                principalTable: "LedgerCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
