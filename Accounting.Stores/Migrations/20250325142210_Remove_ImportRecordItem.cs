using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Remove_ImportRecordItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords");

            migrationBuilder.DropTable(
                name: "ImportRecordItems");

            migrationBuilder.DropTable(
                name: "LedgerRecordLedgerTag");

            migrationBuilder.DropIndex(
                name: "IX_LedgerRecords_CategoryId",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "LedgerRecords");

            migrationBuilder.RenameColumn(
                name: "RecordTime",
                table: "LedgerRecords",
                newName: "PayTime");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "LedgerRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceChannelCode",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceChannelId",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string[]>(
                name: "Tags",
                table: "LedgerRecords",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionContent",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionCreatedTime",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionParty",
                table: "LedgerRecords",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ImportRecords",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "SourceChannelCode",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "SourceChannelId",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionContent",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionCreatedTime",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "TransactionParty",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ImportRecords");

            migrationBuilder.RenameColumn(
                name: "PayTime",
                table: "LedgerRecords",
                newName: "RecordTime");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId",
                table: "LedgerRecords",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImportRecordItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    ImportId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportRecordItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportRecordItems_ImportRecords_ImportId",
                        column: x => x.ImportId,
                        principalTable: "ImportRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerRecordLedgerTag",
                columns: table => new
                {
                    LedgerRecordId = table.Column<long>(type: "bigint", nullable: false),
                    TagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerRecordLedgerTag", x => new { x.LedgerRecordId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_LedgerRecordLedgerTag_LedgerRecords_LedgerRecordId",
                        column: x => x.LedgerRecordId,
                        principalTable: "LedgerRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerRecordLedgerTag_LedgerTags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "LedgerTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecords_CategoryId",
                table: "LedgerRecords",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportRecordItems_ImportId",
                table: "ImportRecordItems",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordLedgerTag_TagsId",
                table: "LedgerRecordLedgerTag",
                column: "TagsId");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                table: "LedgerRecords",
                column: "CategoryId",
                principalTable: "LedgerCategories",
                principalColumn: "Id");
        }
    }
}
