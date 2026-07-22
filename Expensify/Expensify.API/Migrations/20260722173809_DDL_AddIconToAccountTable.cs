using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AddIconToAccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                schema: "finance",
                table: "accounts",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "icon", schema: "finance", table: "accounts");
        }
    }
}
