using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Add_Ledger_And_AssetAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ledgers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    MonthStartDay = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ledgers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AssetAccounts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    IncludedInTotalAssets = table.Column<bool>(type: "boolean", nullable: false),
                    LedgerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetAccounts_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LedgerCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LedgerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerCategories_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    LedgerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerTags_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LedgerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    RecordTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FlowDirection = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Remark = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AssetAccountId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerRecords_AssetAccounts_AssetAccountId",
                        column: x => x.AssetAccountId,
                        principalTable: "AssetAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LedgerRecords_LedgerCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "LedgerCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LedgerRecords_Ledgers_LedgerId",
                        column: x => x.LedgerId,
                        principalTable: "Ledgers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LedgerRecordAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LastModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    LedgerRecordId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerRecordAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerRecordAttachments_FileInformations_FileId",
                        column: x => x.FileId,
                        principalTable: "FileInformations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LedgerRecordAttachments_LedgerRecords_LedgerRecordId",
                        column: x => x.LedgerRecordId,
                        principalTable: "LedgerRecords",
                        principalColumn: "Id");
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
                name: "IX_AssetAccounts_LedgerId",
                table: "AssetAccounts",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerCategories_LedgerId",
                table: "LedgerCategories",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerCategories_Name_LedgerId",
                table: "LedgerCategories",
                columns: new[] { "Name", "LedgerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordAttachments_FileId",
                table: "LedgerRecordAttachments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordAttachments_LedgerRecordId",
                table: "LedgerRecordAttachments",
                column: "LedgerRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecordLedgerTag_TagsId",
                table: "LedgerRecordLedgerTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecords_AssetAccountId",
                table: "LedgerRecords",
                column: "AssetAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecords_CategoryId",
                table: "LedgerRecords",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerRecords_LedgerId",
                table: "LedgerRecords",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTags_LedgerId",
                table: "LedgerTags",
                column: "LedgerId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerTags_Name_LedgerId",
                table: "LedgerTags",
                columns: new[] { "Name", "LedgerId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LedgerRecordAttachments");

            migrationBuilder.DropTable(
                name: "LedgerRecordLedgerTag");

            migrationBuilder.DropTable(
                name: "LedgerRecords");

            migrationBuilder.DropTable(
                name: "LedgerTags");

            migrationBuilder.DropTable(
                name: "AssetAccounts");

            migrationBuilder.DropTable(
                name: "LedgerCategories");

            migrationBuilder.DropTable(
                name: "Ledgers");
        }
    }
}
