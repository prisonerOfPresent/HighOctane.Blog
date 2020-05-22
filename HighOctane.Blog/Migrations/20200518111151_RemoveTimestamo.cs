using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HighOctane.Blog.Migrations
{
    public partial class RemoveTimestamo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "CreatedDate",
                table: "Tags",
                type: "bytea",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "CreatedDate",
                table: "Categories",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
