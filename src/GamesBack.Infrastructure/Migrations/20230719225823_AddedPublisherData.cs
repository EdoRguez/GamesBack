using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesBack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedPublisherData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("10bdf658-4d44-4b08-9e9d-6e428c0d7599"), "Ubisoft" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("10bdf658-4d44-4b08-9e9d-6e428c0d7599"));
        }
    }
}
