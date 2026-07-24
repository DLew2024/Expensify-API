using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AddCurrencyCodeReferenceData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropIndex(
                name: "ix_categories_user_id_is_active",
                schema: "reference",
                table: "categories"
            );

            migrationBuilder.DropIndex(
                name: "ux_categories_user_id_name",
                schema: "reference",
                table: "categories"
            );

            migrationBuilder.DropColumn(
                name: "currency_code",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.EnsureSchema(name: "reference_data");

            migrationBuilder.RenameTable(
                name: "payment_methods",
                schema: "finance",
                newName: "payment_methods",
                newSchema: "reference_data"
            );

            migrationBuilder.RenameTable(
                name: "categories",
                schema: "reference",
                newName: "categories",
                newSchema: "reference_data"
            );

            migrationBuilder.AddColumn<Guid>(
                name: "currency_code_id",
                schema: "finance",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("16628839-2150-475b-9108-8c223e60c0a3")
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                schema: "reference_data",
                table: "payment_methods",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid"
            );

            migrationBuilder.CreateTable(
                name: "currency_codes",
                schema: "reference_data",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    code = table.Column<string>(
                        type: "character varying(3)",
                        maxLength: 3,
                        nullable: false
                    ),
                    symbol = table.Column<string>(
                        type: "character varying(10)",
                        maxLength: 10,
                        nullable: false
                    ),
                    decimal_places = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(
                        type: "boolean",
                        nullable: false,
                        defaultValue: true
                    ),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    last_updated_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    updated_date = table.Column<long>(type: "bigint", nullable: false),
                    create_date = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_codes", x => x.id);
                }
            );

            migrationBuilder.InsertData(
                schema: "reference_data",
                table: "currency_codes",
                columns: new[]
                {
                    "id",
                    "code",
                    "create_date",
                    "created_by",
                    "decimal_places",
                    "is_active",
                    "deletion_indicator",
                    "last_updated_by",
                    "name",
                    "symbol",
                    "updated_date",
                },
                values: new object[,]
                {
                    {
                        new Guid("16628839-2150-475b-9108-8c223e60c0a3"),
                        "USD",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "United States Dollar",
                        "$",
                        0L,
                    },
                    {
                        new Guid("1f78aa2a-b6a4-4d43-91bf-31b2a977b842"),
                        "INR",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Indian Rupee",
                        "₹",
                        0L,
                    },
                    {
                        new Guid("24144803-4020-4dc1-9765-f433bc4dd7c9"),
                        "JPY",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        0,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Japanese Yen",
                        "¥",
                        0L,
                    },
                    {
                        new Guid("4c5021eb-a30f-4446-a759-e2a30d814fb3"),
                        "GBP",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "British Pound Sterling",
                        "£",
                        0L,
                    },
                    {
                        new Guid("7ec564e1-4de2-4cfb-9d42-d4cc5b743cb8"),
                        "MXN",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Mexican Peso",
                        "$",
                        0L,
                    },
                    {
                        new Guid("87ee8fa5-2031-4730-bdbf-0a0998ac6ab3"),
                        "AUD",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Australian Dollar",
                        "$",
                        0L,
                    },
                    {
                        new Guid("b7ce746f-f54c-4517-9bf4-ddaa76b363ef"),
                        "CNY",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Chinese Yuan Renminbi",
                        "¥",
                        0L,
                    },
                    {
                        new Guid("c03746aa-8504-4425-b16e-181de3400c3e"),
                        "CHF",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Swiss Franc",
                        "CHF",
                        0L,
                    },
                    {
                        new Guid("f0dbd1c0-4663-437b-a4e9-6098712c5ede"),
                        "EUR",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Euro",
                        "€",
                        0L,
                    },
                    {
                        new Guid("f654313c-d52f-4574-8ebd-beb68f18d4cb"),
                        "CAD",
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        2,
                        true,
                        false,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Canadian Dollar",
                        "$",
                        0L,
                    },
                }
            );

            migrationBuilder.InsertData(
                schema: "reference_data",
                table: "payment_methods",
                columns: new[]
                {
                    "id",
                    "create_date",
                    "created_by",
                    "description",
                    "is_active",
                    "deletion_indicator",
                    "is_system_default",
                    "last_updated_by",
                    "name",
                    "updated_date",
                    "user_id",
                },
                values: new object[,]
                {
                    {
                        new Guid("30a94670-364a-4515-aedc-04c4e59e70ac"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made using physical cash.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Cash",
                        0L,
                        null,
                    },
                    {
                        new Guid("64f59d3d-810f-4814-a951-5daeb7866237"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made using a debit card.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Debit Card",
                        0L,
                        null,
                    },
                    {
                        new Guid("9017dba1-62df-4661-8d33-de7f37df8a7a"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made using a digital wallet.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Digital Wallet",
                        0L,
                        null,
                    },
                    {
                        new Guid("984931ed-5f87-43d9-a9df-5439b528f321"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made through a bank transfer.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Bank Transfer",
                        0L,
                        null,
                    },
                    {
                        new Guid("c620d7dc-20be-4306-a76f-b33b62ee35b4"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made using a check.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Check",
                        0L,
                        null,
                    },
                    {
                        new Guid("e28f1e04-06fd-45cc-8749-25427024c8db"),
                        0L,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Payment made using a credit card.",
                        true,
                        false,
                        true,
                        new Guid("00000000-0000-0000-0000-000000000000"),
                        "Credit Card",
                        0L,
                        null,
                    },
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_accounts_currency_code_id",
                schema: "finance",
                table: "accounts",
                column: "currency_code_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_categories_user_id_type_is_active",
                schema: "reference_data",
                table: "categories",
                columns: new[] { "user_id", "type", "is_active" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_categories_user_id_name_type",
                schema: "reference_data",
                table: "categories",
                columns: new[] { "user_id", "name", "type" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "uq_currency_codes_code",
                schema: "reference_data",
                table: "currency_codes",
                column: "code",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_currency_codes_currency_code_id",
                schema: "finance",
                table: "accounts",
                column: "currency_code_id",
                principalSchema: "reference_data",
                principalTable: "currency_codes",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions",
                column: "category_id",
                principalSchema: "reference_data",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_currency_codes_currency_code_id",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropTable(name: "currency_codes", schema: "reference_data");

            migrationBuilder.DropIndex(
                name: "ix_accounts_currency_code_id",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropIndex(
                name: "ix_categories_user_id_type_is_active",
                schema: "reference_data",
                table: "categories"
            );

            migrationBuilder.DropIndex(
                name: "ux_categories_user_id_name_type",
                schema: "reference_data",
                table: "categories"
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("30a94670-364a-4515-aedc-04c4e59e70ac")
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("64f59d3d-810f-4814-a951-5daeb7866237")
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("9017dba1-62df-4661-8d33-de7f37df8a7a")
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("984931ed-5f87-43d9-a9df-5439b528f321")
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("c620d7dc-20be-4306-a76f-b33b62ee35b4")
            );

            migrationBuilder.DeleteData(
                schema: "reference_data",
                table: "payment_methods",
                keyColumn: "id",
                keyValue: new Guid("e28f1e04-06fd-45cc-8749-25427024c8db")
            );

            migrationBuilder.DropColumn(
                name: "currency_code_id",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.EnsureSchema(name: "reference");

            migrationBuilder.RenameTable(
                name: "payment_methods",
                schema: "reference_data",
                newName: "payment_methods",
                newSchema: "finance"
            );

            migrationBuilder.RenameTable(
                name: "categories",
                schema: "reference_data",
                newName: "categories",
                newSchema: "reference"
            );

            migrationBuilder.AddColumn<int>(
                name: "currency_code",
                schema: "finance",
                table: "accounts",
                type: "integer",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                schema: "finance",
                table: "payment_methods",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_categories_user_id_is_active",
                schema: "reference",
                table: "categories",
                columns: new[] { "user_id", "is_active" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_categories_user_id_name",
                schema: "reference",
                table: "categories",
                columns: new[] { "user_id", "name" },
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions",
                column: "category_id",
                principalSchema: "reference",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );
        }
    }
}
