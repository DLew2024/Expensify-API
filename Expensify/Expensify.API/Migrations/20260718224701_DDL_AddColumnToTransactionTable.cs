using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AddColumnToTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "icon",
                schema: "finance",
                table: "transactions",
                type: "text",
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<Guid>(
                name: "recurring_transaction_id",
                schema: "finance",
                table: "transactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000")
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "icon", schema: "finance", table: "transactions");

            migrationBuilder.DropColumn(
                name: "recurring_transaction_id",
                schema: "finance",
                table: "transactions"
            );
        }
    }
}
