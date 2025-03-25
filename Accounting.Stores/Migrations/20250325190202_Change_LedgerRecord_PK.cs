using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Change_LedgerRecord_PK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerRecordAttachments_LedgerRecords_LedgerRecordId",
                table: "LedgerRecordAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LedgerRecords",
                table: "LedgerRecords");

            migrationBuilder.DropIndex(
                name: "IX_LedgerRecordAttachments_LedgerRecordId",
                table: "LedgerRecordAttachments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "LedgerRecords");

            migrationBuilder.DropColumn(
                name: "LedgerRecordId",
                table: "LedgerRecordAttachments");

            migrationBuilder.AlterColumn<string>(
                name: "SourceChannelId",
                table: "LedgerRecords",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SourceChannelCode",
                table: "LedgerRecords",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LedgerRecordSourceChannelCode",
                table: "LedgerRecordAttachments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LedgerRecordSourceChannelId",
                table: "LedgerRecordAttachments",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LedgerRecords",
                table: "LedgerRecords",
                columns: new[] { "SourceChannelCode", "SourceChannelId" });

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordAttachments_LedgerRecordSourceChannelCode_Ledge~",
                table: "LedgerRecordAttachments",
                columns: new[] { "LedgerRecordSourceChannelCode", "LedgerRecordSourceChannelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerRecordAttachments_LedgerRecords_LedgerRecordSourceCha~",
                table: "LedgerRecordAttachments",
                columns: new[] { "LedgerRecordSourceChannelCode", "LedgerRecordSourceChannelId" },
                principalTable: "LedgerRecords",
                principalColumns: new[] { "SourceChannelCode", "SourceChannelId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LedgerRecordAttachments_LedgerRecords_LedgerRecordSourceCha~",
                table: "LedgerRecordAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LedgerRecords",
                table: "LedgerRecords");

            migrationBuilder.DropIndex(
                name: "IX_LedgerRecordAttachments_LedgerRecordSourceChannelCode_Ledge~",
                table: "LedgerRecordAttachments");

            migrationBuilder.DropColumn(
                name: "LedgerRecordSourceChannelCode",
                table: "LedgerRecordAttachments");

            migrationBuilder.DropColumn(
                name: "LedgerRecordSourceChannelId",
                table: "LedgerRecordAttachments");

            migrationBuilder.AlterColumn<string>(
                name: "SourceChannelId",
                table: "LedgerRecords",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SourceChannelCode",
                table: "LedgerRecords",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "LedgerRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "LedgerRecordId",
                table: "LedgerRecordAttachments",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LedgerRecords",
                table: "LedgerRecords",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordAttachments_LedgerRecordId",
                table: "LedgerRecordAttachments",
                column: "LedgerRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_LedgerRecordAttachments_LedgerRecords_LedgerRecordId",
                table: "LedgerRecordAttachments",
                column: "LedgerRecordId",
                principalTable: "LedgerRecords",
                principalColumn: "Id");
        }
    }
}
