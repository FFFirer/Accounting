using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Change_AssetAccount_Type_DataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetAccounts_Ledgers_LedgerId",
                table: "AssetAccounts");

            migrationBuilder.DropIndex(
                name: "IX_AssetAccounts_LedgerId",
                table: "AssetAccounts");

            migrationBuilder.DropColumn(
                name: "LedgerId",
                table: "AssetAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AssetAccounts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "AssetAccountLedger",
                columns: table => new
                {
                    AssetAccountsId = table.Column<string>(type: "text", nullable: false),
                    LedgersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAccountLedger", x => new { x.AssetAccountsId, x.LedgersId });
                    table.ForeignKey(
                        name: "FK_AssetAccountLedger_AssetAccounts_AssetAccountsId",
                        column: x => x.AssetAccountsId,
                        principalTable: "AssetAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetAccountLedger_Ledgers_LedgersId",
                        column: x => x.LedgersId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetAccountLedger_LedgersId",
                table: "AssetAccountLedger",
                column: "LedgersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetAccountLedger");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "AssetAccounts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LedgerId",
                table: "AssetAccounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetAccounts_LedgerId",
                table: "AssetAccounts",
                column: "LedgerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetAccounts_Ledgers_LedgerId",
                table: "AssetAccounts",
                column: "LedgerId",
                principalTable: "Ledgers",
                principalColumn: "Id");
        }
    }
}
