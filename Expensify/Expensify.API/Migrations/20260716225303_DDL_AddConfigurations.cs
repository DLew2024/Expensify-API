using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AddConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_AccountType_AccountTypeId",
                schema: "public",
                table: "Account"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_AccountType_users_UserId",
                schema: "public",
                table: "AccountType"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategory_Budget_BudgetId",
                schema: "public",
                table: "BudgetCategory"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetCategory_Category_CategoryId",
                schema: "public",
                table: "BudgetCategory"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetMember_Budget_BudgetId",
                schema: "public",
                table: "BudgetMember"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_budget_memberships_users_user_id",
                schema: "public",
                table: "BudgetMember"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_EmailVerificationTokens_users_UserId",
                schema: "public",
                table: "EmailVerificationTokens"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentMethod_users_UserId",
                schema: "public",
                table: "PaymentMethod"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Account_AccountId",
                schema: "public",
                table: "RecurringTransaction"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Budget_BudgetId",
                schema: "public",
                table: "RecurringTransaction"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_Category_CategoryId",
                schema: "public",
                table: "RecurringTransaction"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_RecurringTransaction_PaymentMethod_PaymentMethodId",
                schema: "public",
                table: "RecurringTransaction"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_users_UserId",
                schema: "public",
                table: "refresh_tokens"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Account_AccountId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Budget_BudgetId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PaymentMethod_PaymentMethodId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Transactions_LinkedTransactionId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropTable(name: "Category", schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AccountId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LinkedTransactionId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                schema: "public",
                table: "Transactions"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_refresh_tokens",
                schema: "public",
                table: "refresh_tokens"
            );

            migrationBuilder.DropIndex(
                name: "IX_refresh_tokens_UserId",
                schema: "public",
                table: "refresh_tokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_RecurringTransaction",
                schema: "public",
                table: "RecurringTransaction"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                schema: "public",
                table: "PaymentMethod"
            );

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethod_UserId",
                schema: "public",
                table: "PaymentMethod"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_PasswordResetTokens",
                schema: "public",
                table: "PasswordResetTokens"
            );

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_TokenHash",
                schema: "public",
                table: "PasswordResetTokens"
            );

            migrationBuilder.DropIndex(
                name: "IX_PasswordResetTokens_UserId",
                schema: "public",
                table: "PasswordResetTokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmailVerificationTokens",
                schema: "public",
                table: "EmailVerificationTokens"
            );

            migrationBuilder.DropIndex(
                name: "IX_EmailVerificationTokens_UserId",
                schema: "public",
                table: "EmailVerificationTokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetMember",
                schema: "public",
                table: "BudgetMember"
            );

            migrationBuilder.DropIndex(
                name: "IX_BudgetMember_BudgetId",
                schema: "public",
                table: "BudgetMember"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetCategory",
                schema: "public",
                table: "BudgetCategory"
            );

            migrationBuilder.DropIndex(
                name: "IX_BudgetCategory_BudgetId",
                schema: "public",
                table: "BudgetCategory"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Budget", schema: "public", table: "Budget");

            migrationBuilder.DropIndex(
                name: "IX_Budget_OwnerUserId",
                schema: "public",
                table: "Budget"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountType",
                schema: "public",
                table: "AccountType"
            );

            migrationBuilder.DropIndex(
                name: "IX_AccountType_UserId",
                schema: "public",
                table: "AccountType"
            );

            migrationBuilder.DropPrimaryKey(name: "PK_Account", schema: "public", table: "Account");

            migrationBuilder.DropIndex(
                name: "IX_Account_UserId",
                schema: "public",
                table: "Account"
            );

            migrationBuilder.DropColumn(name: "Color", schema: "public", table: "PaymentMethod");

            migrationBuilder.DropColumn(name: "Icon", schema: "public", table: "PaymentMethod");

            migrationBuilder.DropColumn(name: "Color", schema: "public", table: "AccountType");

            migrationBuilder.DropColumn(name: "Icon", schema: "public", table: "AccountType");

            migrationBuilder.EnsureSchema(name: "finance");

            migrationBuilder.EnsureSchema(name: "budgeting");

            migrationBuilder.EnsureSchema(name: "reference");

            migrationBuilder.EnsureSchema(name: "identity");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "public",
                newName: "users",
                newSchema: "identity"
            );

            migrationBuilder.RenameTable(
                name: "Transactions",
                schema: "public",
                newName: "transactions",
                newSchema: "finance"
            );

            migrationBuilder.RenameTable(
                name: "refresh_tokens",
                schema: "public",
                newName: "refresh_tokens",
                newSchema: "identity"
            );

            migrationBuilder.RenameTable(
                name: "RecurringTransaction",
                schema: "public",
                newName: "recurring_transactions",
                newSchema: "finance"
            );

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                schema: "public",
                newName: "payment_methods",
                newSchema: "finance"
            );

            migrationBuilder.RenameTable(
                name: "PasswordResetTokens",
                schema: "public",
                newName: "password_reset_tokens",
                newSchema: "identity"
            );

            migrationBuilder.RenameTable(
                name: "EmailVerificationTokens",
                schema: "public",
                newName: "email_verification_tokens",
                newSchema: "identity"
            );

            migrationBuilder.RenameTable(
                name: "BudgetMember",
                schema: "public",
                newName: "budget_members",
                newSchema: "budgeting"
            );

            migrationBuilder.RenameTable(
                name: "BudgetCategory",
                schema: "public",
                newName: "budget_categories",
                newSchema: "budgeting"
            );

            migrationBuilder.RenameTable(
                name: "Budget",
                schema: "public",
                newName: "budgets",
                newSchema: "budgeting"
            );

            migrationBuilder.RenameTable(
                name: "AccountType",
                schema: "public",
                newName: "account_types",
                newSchema: "finance"
            );

            migrationBuilder.RenameTable(
                name: "Account",
                schema: "public",
                newName: "accounts",
                newSchema: "finance"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_PaymentMethodId",
                schema: "finance",
                table: "transactions",
                newName: "ix_transactions_payment_method_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CategoryId",
                schema: "finance",
                table: "transactions",
                newName: "ix_transactions_category_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_BudgetId",
                schema: "finance",
                table: "transactions",
                newName: "ix_transactions_budget_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_TokenHash",
                schema: "identity",
                table: "refresh_tokens",
                newName: "ux_refresh_tokens_token_hash"
            );

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_UserId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "ix_recurring_transactions_user_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_PaymentMethodId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "ix_recurring_transactions_payment_method_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_CategoryId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "ix_recurring_transactions_category_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_BudgetId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "ix_recurring_transactions_budget_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_RecurringTransaction_AccountId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "ix_recurring_transactions_account_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_EmailVerificationTokens_TokenHash",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "ux_email_verification_tokens_token_hash"
            );

            migrationBuilder.RenameIndex(
                name: "IX_BudgetMember_UserId",
                schema: "budgeting",
                table: "budget_members",
                newName: "ix_budget_members_user_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_BudgetCategory_CategoryId",
                schema: "budgeting",
                table: "budget_categories",
                newName: "ix_budget_categories_category_id"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Account_AccountTypeId",
                schema: "finance",
                table: "accounts",
                newName: "ix_accounts_account_type_id"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "finance",
                table: "transactions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "MerchantName",
                schema: "finance",
                table: "transactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "finance",
                table: "transactions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "finance",
                table: "transactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalanceAfterTransaction",
                schema: "finance",
                table: "transactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "refresh_tokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<string>(
                name: "RevocationReason",
                schema: "identity",
                table: "refresh_tokens",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "finance",
                table: "recurring_transactions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "MerchantName",
                schema: "finance",
                table: "recurring_transactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "finance",
                table: "recurring_transactions",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "finance",
                table: "recurring_transactions",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finance",
                table: "payment_methods",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "finance",
                table: "payment_methods",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "password_reset_tokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "identity",
                table: "email_verification_tokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CategoryLimitAmount",
                schema: "budgeting",
                table: "budget_categories",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "budgeting",
                table: "budgets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "LimitAmount",
                schema: "budgeting",
                table: "budgets",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "budgeting",
                table: "budgets",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "finance",
                table: "account_types",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finance",
                table: "account_types",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "finance",
                table: "account_types",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "finance",
                table: "accounts",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "finance",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text"
            );

            migrationBuilder.AlterColumn<string>(
                name: "LastFourDigits",
                schema: "finance",
                table: "accounts",
                type: "character varying(4)",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                schema: "finance",
                table: "accounts",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionName",
                schema: "finance",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                schema: "finance",
                table: "accounts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                schema: "finance",
                table: "accounts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableBalance",
                schema: "finance",
                table: "accounts",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric"
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTypeId",
                schema: "finance",
                table: "accounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_transactions",
                schema: "finance",
                table: "transactions",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_refresh_tokens",
                schema: "identity",
                table: "refresh_tokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_recurring_transactions",
                schema: "finance",
                table: "recurring_transactions",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_payment_methods",
                schema: "finance",
                table: "payment_methods",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_password_reset_tokens",
                schema: "identity",
                table: "password_reset_tokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_email_verification_tokens",
                schema: "identity",
                table: "email_verification_tokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_budget_members",
                schema: "budgeting",
                table: "budget_members",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_budget_categories",
                schema: "budgeting",
                table: "budget_categories",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_budgets",
                schema: "budgeting",
                table: "budgets",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_account_types",
                schema: "finance",
                table: "account_types",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_accounts",
                schema: "finance",
                table: "accounts",
                column: "Id"
            );

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "reference",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false
                    ),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Description = table.Column<string>(
                        type: "character varying(500)",
                        maxLength: 500,
                        nullable: true
                    ),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.Id);
                    table.ForeignKey(
                        name: "fk_categories_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_transactions_account_id_transaction_date",
                schema: "finance",
                table: "transactions",
                columns: new[] { "AccountId", "TransactionDate" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_transactions_user_id_status",
                schema: "finance",
                table: "transactions",
                columns: new[] { "UserId", "Status" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_transactions_user_id_transaction_date",
                schema: "finance",
                table: "transactions",
                columns: new[] { "UserId", "TransactionDate" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_transactions_linked_transaction_id",
                schema: "finance",
                table: "transactions",
                column: "LinkedTransactionId",
                unique: true,
                filter: "\"LinkedTransactionId\" IS NOT NULL"
            );

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "refresh_tokens",
                columns: new[] { "UserId", "IsRevoked", "ExpiresAt" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_recurring_transactions_is_active_next_run_date",
                schema: "finance",
                table: "recurring_transactions",
                columns: new[] { "IsActive", "NextRunDate" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_payment_methods_user_id_is_active",
                schema: "finance",
                table: "payment_methods",
                columns: new[] { "UserId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_payment_methods_user_id_name",
                schema: "finance",
                table: "payment_methods",
                columns: new[] { "UserId", "Name" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_password_reset_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "password_reset_tokens",
                columns: new[] { "UserId", "IsRevoked", "ExpiresAt" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_password_reset_tokens_token_hash",
                schema: "identity",
                table: "password_reset_tokens",
                column: "TokenHash",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_email_verification_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "email_verification_tokens",
                columns: new[] { "UserId", "IsRevoked", "ExpiresAt" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_budget_members_budget_id_is_active",
                schema: "budgeting",
                table: "budget_members",
                columns: new[] { "BudgetId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_budget_members_budget_id_user_id",
                schema: "budgeting",
                table: "budget_members",
                columns: new[] { "BudgetId", "UserId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ux_budget_categories_budget_id_category_id",
                schema: "budgeting",
                table: "budget_categories",
                columns: new[] { "BudgetId", "CategoryId" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_budgets_owner_user_id_is_active",
                schema: "budgeting",
                table: "budgets",
                columns: new[] { "OwnerUserId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_budgets_owner_user_id_name",
                schema: "budgeting",
                table: "budgets",
                columns: new[] { "OwnerUserId", "Name" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_account_types_user_id_is_active",
                schema: "finance",
                table: "account_types",
                columns: new[] { "UserId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_account_types_user_id_name",
                schema: "finance",
                table: "account_types",
                columns: new[] { "UserId", "Name" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_accounts_user_id_is_active",
                schema: "finance",
                table: "accounts",
                columns: new[] { "UserId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_accounts_user_id_is_hidden",
                schema: "finance",
                table: "accounts",
                columns: new[] { "UserId", "IsHidden" }
            );

            migrationBuilder.CreateIndex(
                name: "ix_categories_user_id_is_active",
                schema: "reference",
                table: "categories",
                columns: new[] { "UserId", "IsActive" }
            );

            migrationBuilder.CreateIndex(
                name: "ux_categories_user_id_name",
                schema: "reference",
                table: "categories",
                columns: new[] { "UserId", "Name" },
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_account_types_users_user_id",
                schema: "finance",
                table: "account_types",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                schema: "finance",
                table: "accounts",
                column: "AccountTypeId",
                principalSchema: "finance",
                principalTable: "account_types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "fk_budget_categories_budgets_budget_id",
                schema: "budgeting",
                table: "budget_categories",
                column: "BudgetId",
                principalSchema: "budgeting",
                principalTable: "budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_budget_categories_categories_category_id",
                schema: "budgeting",
                table: "budget_categories",
                column: "CategoryId",
                principalSchema: "reference",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_budget_members_budgets_budget_id",
                schema: "budgeting",
                table: "budget_members",
                column: "BudgetId",
                principalSchema: "budgeting",
                principalTable: "budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_budget_members_users_user_id",
                schema: "budgeting",
                table: "budget_members",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_email_verification_tokens_users_user_id",
                schema: "identity",
                table: "email_verification_tokens",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_payment_methods_users_user_id",
                schema: "finance",
                table: "payment_methods",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_recurring_transactions_accounts_account_id",
                schema: "finance",
                table: "recurring_transactions",
                column: "AccountId",
                principalSchema: "finance",
                principalTable: "accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "fk_recurring_transactions_budgets_budget_id",
                schema: "finance",
                table: "recurring_transactions",
                column: "BudgetId",
                principalSchema: "budgeting",
                principalTable: "budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "fk_recurring_transactions_categories_category_id",
                schema: "finance",
                table: "recurring_transactions",
                column: "CategoryId",
                principalSchema: "reference",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "fk_recurring_transactions_payment_methods_payment_method_id",
                schema: "finance",
                table: "recurring_transactions",
                column: "PaymentMethodId",
                principalSchema: "finance",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "fk_refresh_tokens_users_user_id",
                schema: "identity",
                table: "refresh_tokens",
                column: "UserId",
                principalSchema: "identity",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_accounts_account_id",
                schema: "finance",
                table: "transactions",
                column: "AccountId",
                principalSchema: "finance",
                principalTable: "accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_budgets_budget_id",
                schema: "finance",
                table: "transactions",
                column: "BudgetId",
                principalSchema: "budgeting",
                principalTable: "budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions",
                column: "CategoryId",
                principalSchema: "reference",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_payment_methods_payment_method_id",
                schema: "finance",
                table: "transactions",
                column: "PaymentMethodId",
                principalSchema: "finance",
                principalTable: "payment_methods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );

            migrationBuilder.AddForeignKey(
                name: "fk_transactions_transactions_linked_transaction_id",
                schema: "finance",
                table: "transactions",
                column: "LinkedTransactionId",
                principalSchema: "finance",
                principalTable: "transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_account_types_users_user_id",
                schema: "finance",
                table: "account_types"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_budget_categories_budgets_budget_id",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_budget_categories_categories_category_id",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_budget_members_budgets_budget_id",
                schema: "budgeting",
                table: "budget_members"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_budget_members_users_user_id",
                schema: "budgeting",
                table: "budget_members"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_email_verification_tokens_users_user_id",
                schema: "identity",
                table: "email_verification_tokens"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_payment_methods_users_user_id",
                schema: "finance",
                table: "payment_methods"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_recurring_transactions_accounts_account_id",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_recurring_transactions_budgets_budget_id",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_recurring_transactions_categories_category_id",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_recurring_transactions_payment_methods_payment_method_id",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_refresh_tokens_users_user_id",
                schema: "identity",
                table: "refresh_tokens"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_accounts_account_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_budgets_budget_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_categories_category_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_payment_methods_payment_method_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropForeignKey(
                name: "fk_transactions_transactions_linked_transaction_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropTable(name: "categories", schema: "reference");

            migrationBuilder.DropPrimaryKey(
                name: "pk_transactions",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropIndex(
                name: "ix_transactions_account_id_transaction_date",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropIndex(
                name: "ix_transactions_user_id_status",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropIndex(
                name: "ix_transactions_user_id_transaction_date",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropIndex(
                name: "ux_transactions_linked_transaction_id",
                schema: "finance",
                table: "transactions"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_refresh_tokens",
                schema: "identity",
                table: "refresh_tokens"
            );

            migrationBuilder.DropIndex(
                name: "ix_refresh_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "refresh_tokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_recurring_transactions",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropIndex(
                name: "ix_recurring_transactions_is_active_next_run_date",
                schema: "finance",
                table: "recurring_transactions"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_payment_methods",
                schema: "finance",
                table: "payment_methods"
            );

            migrationBuilder.DropIndex(
                name: "ix_payment_methods_user_id_is_active",
                schema: "finance",
                table: "payment_methods"
            );

            migrationBuilder.DropIndex(
                name: "ux_payment_methods_user_id_name",
                schema: "finance",
                table: "payment_methods"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_password_reset_tokens",
                schema: "identity",
                table: "password_reset_tokens"
            );

            migrationBuilder.DropIndex(
                name: "ix_password_reset_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "password_reset_tokens"
            );

            migrationBuilder.DropIndex(
                name: "ux_password_reset_tokens_token_hash",
                schema: "identity",
                table: "password_reset_tokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_email_verification_tokens",
                schema: "identity",
                table: "email_verification_tokens"
            );

            migrationBuilder.DropIndex(
                name: "ix_email_verification_tokens_user_id_is_revoked_expires_at",
                schema: "identity",
                table: "email_verification_tokens"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_budgets",
                schema: "budgeting",
                table: "budgets"
            );

            migrationBuilder.DropIndex(
                name: "ix_budgets_owner_user_id_is_active",
                schema: "budgeting",
                table: "budgets"
            );

            migrationBuilder.DropIndex(
                name: "ux_budgets_owner_user_id_name",
                schema: "budgeting",
                table: "budgets"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_budget_members",
                schema: "budgeting",
                table: "budget_members"
            );

            migrationBuilder.DropIndex(
                name: "ix_budget_members_budget_id_is_active",
                schema: "budgeting",
                table: "budget_members"
            );

            migrationBuilder.DropIndex(
                name: "ux_budget_members_budget_id_user_id",
                schema: "budgeting",
                table: "budget_members"
            );

            migrationBuilder.DropPrimaryKey(
                name: "PK_budget_categories",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.DropIndex(
                name: "ux_budget_categories_budget_id_category_id",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_accounts",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropIndex(
                name: "ix_accounts_user_id_is_active",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropIndex(
                name: "ix_accounts_user_id_is_hidden",
                schema: "finance",
                table: "accounts"
            );

            migrationBuilder.DropPrimaryKey(
                name: "pk_account_types",
                schema: "finance",
                table: "account_types"
            );

            migrationBuilder.DropIndex(
                name: "ix_account_types_user_id_is_active",
                schema: "finance",
                table: "account_types"
            );

            migrationBuilder.DropIndex(
                name: "ux_account_types_user_id_name",
                schema: "finance",
                table: "account_types"
            );

            migrationBuilder.EnsureSchema(name: "public");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "identity",
                newName: "users",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "transactions",
                schema: "finance",
                newName: "Transactions",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "refresh_tokens",
                schema: "identity",
                newName: "refresh_tokens",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "recurring_transactions",
                schema: "finance",
                newName: "RecurringTransaction",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "payment_methods",
                schema: "finance",
                newName: "PaymentMethod",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "password_reset_tokens",
                schema: "identity",
                newName: "PasswordResetTokens",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "email_verification_tokens",
                schema: "identity",
                newName: "EmailVerificationTokens",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "budgets",
                schema: "budgeting",
                newName: "Budget",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "budget_members",
                schema: "budgeting",
                newName: "BudgetMember",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "budget_categories",
                schema: "budgeting",
                newName: "BudgetCategory",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "accounts",
                schema: "finance",
                newName: "Account",
                newSchema: "public"
            );

            migrationBuilder.RenameTable(
                name: "account_types",
                schema: "finance",
                newName: "AccountType",
                newSchema: "public"
            );

            migrationBuilder.RenameIndex(
                name: "ix_transactions_payment_method_id",
                schema: "public",
                table: "Transactions",
                newName: "IX_Transactions_PaymentMethodId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_transactions_category_id",
                schema: "public",
                table: "Transactions",
                newName: "IX_Transactions_CategoryId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_transactions_budget_id",
                schema: "public",
                table: "Transactions",
                newName: "IX_Transactions_BudgetId"
            );

            migrationBuilder.RenameIndex(
                name: "ux_refresh_tokens_token_hash",
                schema: "public",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_TokenHash"
            );

            migrationBuilder.RenameIndex(
                name: "ix_recurring_transactions_user_id",
                schema: "public",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_UserId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_recurring_transactions_payment_method_id",
                schema: "public",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_PaymentMethodId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_recurring_transactions_category_id",
                schema: "public",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_CategoryId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_recurring_transactions_budget_id",
                schema: "public",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_BudgetId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_recurring_transactions_account_id",
                schema: "public",
                table: "RecurringTransaction",
                newName: "IX_RecurringTransaction_AccountId"
            );

            migrationBuilder.RenameIndex(
                name: "ux_email_verification_tokens_token_hash",
                schema: "public",
                table: "EmailVerificationTokens",
                newName: "IX_EmailVerificationTokens_TokenHash"
            );

            migrationBuilder.RenameIndex(
                name: "ix_budget_members_user_id",
                schema: "public",
                table: "BudgetMember",
                newName: "IX_BudgetMember_UserId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_budget_categories_category_id",
                schema: "public",
                table: "BudgetCategory",
                newName: "IX_BudgetCategory_CategoryId"
            );

            migrationBuilder.RenameIndex(
                name: "ix_accounts_account_type_id",
                schema: "public",
                table: "Account",
                newName: "IX_Account_AccountTypeId"
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "public",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "MerchantName",
                schema: "public",
                table: "Transactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Transactions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "public",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalanceAfterTransaction",
                schema: "public",
                table: "Transactions",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "public",
                table: "refresh_tokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128
            );

            migrationBuilder.AlterColumn<string>(
                name: "RevocationReason",
                schema: "public",
                table: "refresh_tokens",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "public",
                table: "RecurringTransaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "MerchantName",
                schema: "public",
                table: "RecurringTransaction",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "RecurringTransaction",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "public",
                table: "RecurringTransaction",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "public",
                table: "PaymentMethod",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "PaymentMethod",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "public",
                table: "PaymentMethod",
                type: "text",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "public",
                table: "PaymentMethod",
                type: "text",
                nullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "public",
                table: "PasswordResetTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128
            );

            migrationBuilder.AlterColumn<string>(
                name: "TokenHash",
                schema: "public",
                table: "EmailVerificationTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "public",
                table: "Budget",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "LimitAmount",
                schema: "public",
                table: "Budget",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Budget",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CategoryLimitAmount",
                schema: "public",
                table: "BudgetCategory",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                schema: "public",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "public",
                table: "Account",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100
            );

            migrationBuilder.AlterColumn<string>(
                name: "LastFourDigits",
                schema: "public",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldMaxLength: 4,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                schema: "public",
                table: "Account",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "InstitutionName",
                schema: "public",
                table: "Account",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance",
                schema: "public",
                table: "Account",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "CreditLimit",
                schema: "public",
                table: "Account",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true
            );

            migrationBuilder.AlterColumn<decimal>(
                name: "AvailableBalance",
                schema: "public",
                table: "Account",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTypeId",
                schema: "public",
                table: "Account",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid"
            );

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "public",
                table: "AccountType",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "public",
                table: "AccountType",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100
            );

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "AccountType",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "public",
                table: "AccountType",
                type: "text",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "public",
                table: "AccountType",
                type: "text",
                nullable: true
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                schema: "public",
                table: "Transactions",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_refresh_tokens",
                schema: "public",
                table: "refresh_tokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecurringTransaction",
                schema: "public",
                table: "RecurringTransaction",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                schema: "public",
                table: "PaymentMethod",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_PasswordResetTokens",
                schema: "public",
                table: "PasswordResetTokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmailVerificationTokens",
                schema: "public",
                table: "EmailVerificationTokens",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Budget",
                schema: "public",
                table: "Budget",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetMember",
                schema: "public",
                table: "BudgetMember",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetCategory",
                schema: "public",
                table: "BudgetCategory",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                schema: "public",
                table: "Account",
                column: "Id"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountType",
                schema: "public",
                table: "AccountType",
                column: "Id"
            );

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "fk_categories_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                schema: "public",
                table: "Transactions",
                column: "AccountId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LinkedTransactionId",
                schema: "public",
                table: "Transactions",
                column: "LinkedTransactionId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                schema: "public",
                table: "Transactions",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId",
                schema: "public",
                table: "refresh_tokens",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethod_UserId",
                schema: "public",
                table: "PaymentMethod",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_TokenHash",
                schema: "public",
                table: "PasswordResetTokens",
                column: "TokenHash"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PasswordResetTokens_UserId",
                schema: "public",
                table: "PasswordResetTokens",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserId",
                schema: "public",
                table: "EmailVerificationTokens",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Budget_OwnerUserId",
                schema: "public",
                table: "Budget",
                column: "OwnerUserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMember_BudgetId",
                schema: "public",
                table: "BudgetMember",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_BudgetId",
                schema: "public",
                table: "BudgetCategory",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Account_UserId",
                schema: "public",
                table: "Account",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_UserId",
                schema: "public",
                table: "AccountType",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                schema: "public",
                table: "Category",
                column: "UserId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Account_AccountType_AccountTypeId",
                schema: "public",
                table: "Account",
                column: "AccountTypeId",
                principalSchema: "public",
                principalTable: "AccountType",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_AccountType_users_UserId",
                schema: "public",
                table: "AccountType",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategory_Budget_BudgetId",
                schema: "public",
                table: "BudgetCategory",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetCategory_Category_CategoryId",
                schema: "public",
                table: "BudgetCategory",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetMember_Budget_BudgetId",
                schema: "public",
                table: "BudgetMember",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budget",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "fk_budget_memberships_users_user_id",
                schema: "public",
                table: "BudgetMember",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_EmailVerificationTokens_users_UserId",
                schema: "public",
                table: "EmailVerificationTokens",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethod_users_UserId",
                schema: "public",
                table: "PaymentMethod",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Account_AccountId",
                schema: "public",
                table: "RecurringTransaction",
                column: "AccountId",
                principalSchema: "public",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Budget_BudgetId",
                schema: "public",
                table: "RecurringTransaction",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budget",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_Category_CategoryId",
                schema: "public",
                table: "RecurringTransaction",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Category",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_RecurringTransaction_PaymentMethod_PaymentMethodId",
                schema: "public",
                table: "RecurringTransaction",
                column: "PaymentMethodId",
                principalSchema: "public",
                principalTable: "PaymentMethod",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_users_UserId",
                schema: "public",
                table: "refresh_tokens",
                column: "UserId",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Account_AccountId",
                schema: "public",
                table: "Transactions",
                column: "AccountId",
                principalSchema: "public",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Budget_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId",
                principalSchema: "public",
                principalTable: "Budget",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Category_CategoryId",
                schema: "public",
                table: "Transactions",
                column: "CategoryId",
                principalSchema: "public",
                principalTable: "Category",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PaymentMethod_PaymentMethodId",
                schema: "public",
                table: "Transactions",
                column: "PaymentMethodId",
                principalSchema: "public",
                principalTable: "PaymentMethod",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Transactions_LinkedTransactionId",
                schema: "public",
                table: "Transactions",
                column: "LinkedTransactionId",
                principalSchema: "public",
                principalTable: "Transactions",
                principalColumn: "Id"
            );
        }
    }
}
