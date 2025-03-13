using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class Add_StorageBucket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BucketName",
                table: "FileInformations",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StorageBuckets",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatationTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageBuckets", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileInformations_BucketName",
                table: "FileInformations",
                column: "BucketName");

            migrationBuilder.AddForeignKey(
                name: "FK_FileInformations_StorageBuckets_BucketName",
                table: "FileInformations",
                column: "BucketName",
                principalTable: "StorageBuckets",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileInformations_StorageBuckets_BucketName",
                table: "FileInformations");

            migrationBuilder.DropTable(
                name: "StorageBuckets");

            migrationBuilder.DropIndex(
                name: "IX_FileInformations_BucketName",
                table: "FileInformations");

            migrationBuilder.DropColumn(
                name: "BucketName",
                table: "FileInformations");
        }
    }
}
