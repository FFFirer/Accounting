using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Add_ImportRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImportRecords",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FileId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Channel_Code = table.Column<string>(type: "text", nullable: true),
                    Channel_Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportRecords_FileInformations_FileId",
                        column: x => x.FileId,
                        principalTable: "FileInformations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportRecordItems",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ImportId = table.Column<long>(type: "bigint", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ImportRecordItems_ImportId",
                table: "ImportRecordItems",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportRecords_FileId",
                table: "ImportRecords",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportRecordItems");

            migrationBuilder.DropTable(
                name: "ImportRecords");
        }
    }
}
