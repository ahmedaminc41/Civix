using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Civix.App.Repositories.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditColumnsToIssueTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Issues",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Issues",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Issues",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Issues",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Issues",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Issues_CreatedById",
                table: "Issues",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_UpdatedById",
                table: "Issues",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_CreatedById",
                table: "Issues",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AspNetUsers_UpdatedById",
                table: "Issues",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_CreatedById",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AspNetUsers_UpdatedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_CreatedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_UpdatedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Issues");

            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                table: "Issues",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                table: "Issues",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);
        }
    }
}
