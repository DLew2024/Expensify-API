using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DML_SeedAccountTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "finance",
                table: "account_types",
                columns: new[] { "id", "create_date", "created_by", "description", "is_active", "deletion_indicator", "is_system_default", "last_updated_by", "name", "updated_date", "user_id" },
                values: new object[,]
                {
                    { new Guid("21d970f4-bda1-48c5-8b04-f38408508238"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard savings account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Savings", 0L, null },
                    { new Guid("6f8332b7-cf15-4c41-b1b5-a85022859dd8"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Standard checking account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Checking", 0L, null },
                    { new Guid("81ac10c1-b977-45c9-a637-a00654ef07e7"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Physical cash account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Cash", 0L, null },
                    { new Guid("9de9f381-06e4-41f8-8269-f8cb76330cc9"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Brokerage or investment account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Investment", 0L, null },
                    { new Guid("c524f6bf-b7da-4985-a1af-f76222dfc89c"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Credit card or revolving credit account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Credit Card", 0L, null },
                    { new Guid("f95a4632-e98f-4c9b-b831-b14447e3e308"), 0L, new Guid("00000000-0000-0000-0000-000000000000"), "Loan or other debt account.", true, false, true, new Guid("00000000-0000-0000-0000-000000000000"), "Loan", 0L, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("21d970f4-bda1-48c5-8b04-f38408508238"));

            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("6f8332b7-cf15-4c41-b1b5-a85022859dd8"));

            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("81ac10c1-b977-45c9-a637-a00654ef07e7"));

            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("9de9f381-06e4-41f8-8269-f8cb76330cc9"));

            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("c524f6bf-b7da-4985-a1af-f76222dfc89c"));

            migrationBuilder.DeleteData(
                schema: "finance",
                table: "account_types",
                keyColumn: "id",
                keyValue: new Guid("f95a4632-e98f-4c9b-b831-b14447e3e308"));
        }
    }
}
