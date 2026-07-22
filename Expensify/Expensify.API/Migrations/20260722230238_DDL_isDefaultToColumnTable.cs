using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_isDefaultToColumnTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_default",
                schema: "finance",
                table: "accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "ux_accounts_user_default",
                schema: "finance",
                table: "accounts",
                column: "user_id",
                unique: true,
                filter: "\"is_default\" = true");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ux_accounts_user_default",
                schema: "finance",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "is_default",
                schema: "finance",
                table: "accounts");
        }
    }
}
