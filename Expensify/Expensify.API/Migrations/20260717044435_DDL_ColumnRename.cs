using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_ColumnRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_budget_categories",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "identity",
                table: "users",
                newName: "password"
            );

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "identity",
                table: "users",
                newName: "email"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "identity",
                table: "users",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                schema: "identity",
                table: "users",
                newName: "profile_image_url"
            );

            migrationBuilder.RenameColumn(
                name: "IsEmailVerified",
                schema: "identity",
                table: "users",
                newName: "is_email_verified"
            );

            migrationBuilder.RenameColumn(
                name: "FullName",
                schema: "identity",
                table: "users",
                newName: "full_name"
            );

            migrationBuilder.RenameColumn(
                name: "EmailVerifiedAt",
                schema: "identity",
                table: "users",
                newName: "email_verified_at"
            );

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "finance",
                table: "transactions",
                newName: "type"
            );

            migrationBuilder.RenameColumn(
                name: "Tags",
                schema: "finance",
                table: "transactions",
                newName: "tags"
            );

            migrationBuilder.RenameColumn(
                name: "Status",
                schema: "finance",
                table: "transactions",
                newName: "status"
            );

            migrationBuilder.RenameColumn(
                name: "Notes",
                schema: "finance",
                table: "transactions",
                newName: "notes"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "finance",
                table: "transactions",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "finance",
                table: "transactions",
                newName: "amount"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "finance",
                table: "transactions",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finance",
                table: "transactions",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "finance",
                table: "transactions",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "TransactionDate",
                schema: "finance",
                table: "transactions",
                newName: "transaction_date"
            );

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                schema: "finance",
                table: "transactions",
                newName: "payment_method_id"
            );

            migrationBuilder.RenameColumn(
                name: "MerchantName",
                schema: "finance",
                table: "transactions",
                newName: "merchant_name"
            );

            migrationBuilder.RenameColumn(
                name: "LinkedTransactionId",
                schema: "finance",
                table: "transactions",
                newName: "linked_transaction_id"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "finance",
                table: "transactions",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsRecurring",
                schema: "finance",
                table: "transactions",
                newName: "is_recurring"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "finance",
                table: "transactions",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "finance",
                table: "transactions",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "finance",
                table: "transactions",
                newName: "category_id"
            );

            migrationBuilder.RenameColumn(
                name: "BudgetId",
                schema: "finance",
                table: "transactions",
                newName: "budget_id"
            );

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "finance",
                table: "transactions",
                newName: "account_id"
            );

            migrationBuilder.RenameColumn(
                name: "AccountBalanceAfterTransaction",
                schema: "finance",
                table: "transactions",
                newName: "account_balance_after_transaction"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "identity",
                table: "refresh_tokens",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "identity",
                table: "refresh_tokens",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "identity",
                table: "refresh_tokens",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                schema: "identity",
                table: "refresh_tokens",
                newName: "token_hash"
            );

            migrationBuilder.RenameColumn(
                name: "RevokedAt",
                schema: "identity",
                table: "refresh_tokens",
                newName: "revoked_at"
            );

            migrationBuilder.RenameColumn(
                name: "RevocationReason",
                schema: "identity",
                table: "refresh_tokens",
                newName: "revocation_reason"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "identity",
                table: "refresh_tokens",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                schema: "identity",
                table: "refresh_tokens",
                newName: "is_revoked"
            );

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                schema: "identity",
                table: "refresh_tokens",
                newName: "expires_at"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "identity",
                table: "refresh_tokens",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "identity",
                table: "refresh_tokens",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Type",
                schema: "finance",
                table: "recurring_transactions",
                newName: "type"
            );

            migrationBuilder.RenameColumn(
                name: "Notes",
                schema: "finance",
                table: "recurring_transactions",
                newName: "notes"
            );

            migrationBuilder.RenameColumn(
                name: "Frequency",
                schema: "finance",
                table: "recurring_transactions",
                newName: "frequency"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "finance",
                table: "recurring_transactions",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "finance",
                table: "recurring_transactions",
                newName: "amount"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "finance",
                table: "recurring_transactions",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "finance",
                table: "recurring_transactions",
                newName: "start_date"
            );

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "payment_method_id"
            );

            migrationBuilder.RenameColumn(
                name: "NextRunDate",
                schema: "finance",
                table: "recurring_transactions",
                newName: "next_run_date"
            );

            migrationBuilder.RenameColumn(
                name: "MerchantName",
                schema: "finance",
                table: "recurring_transactions",
                newName: "merchant_name"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "finance",
                table: "recurring_transactions",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "finance",
                table: "recurring_transactions",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "finance",
                table: "recurring_transactions",
                newName: "end_date"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "finance",
                table: "recurring_transactions",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "finance",
                table: "recurring_transactions",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "category_id"
            );

            migrationBuilder.RenameColumn(
                name: "BudgetId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "budget_id"
            );

            migrationBuilder.RenameColumn(
                name: "AccountId",
                schema: "finance",
                table: "recurring_transactions",
                newName: "account_id"
            );

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "finance",
                table: "payment_methods",
                newName: "name"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "finance",
                table: "payment_methods",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "finance",
                table: "payment_methods",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finance",
                table: "payment_methods",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "finance",
                table: "payment_methods",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "finance",
                table: "payment_methods",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsSystemDefault",
                schema: "finance",
                table: "payment_methods",
                newName: "is_system_default"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "finance",
                table: "payment_methods",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "finance",
                table: "payment_methods",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "finance",
                table: "payment_methods",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UsedAt",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "used_at"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "token_hash"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "is_revoked"
            );

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "expires_at"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UsedAt",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "used_at"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "TokenHash",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "token_hash"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsRevoked",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "is_revoked"
            );

            migrationBuilder.RenameColumn(
                name: "ExpiresAt",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "expires_at"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "reference",
                table: "categories",
                newName: "name"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "reference",
                table: "categories",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "reference",
                table: "categories",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "reference",
                table: "categories",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "reference",
                table: "categories",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "reference",
                table: "categories",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsSystemDefault",
                schema: "reference",
                table: "categories",
                newName: "is_system_default"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "reference",
                table: "categories",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "reference",
                table: "categories",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "reference",
                table: "categories",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Period",
                schema: "budgeting",
                table: "budgets",
                newName: "period"
            );

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "budgeting",
                table: "budgets",
                newName: "name"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "budgeting",
                table: "budgets",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "budgeting",
                table: "budgets",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "budgeting",
                table: "budgets",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "StartDate",
                schema: "budgeting",
                table: "budgets",
                newName: "start_date"
            );

            migrationBuilder.RenameColumn(
                name: "OwnerUserId",
                schema: "budgeting",
                table: "budgets",
                newName: "owner_user_id"
            );

            migrationBuilder.RenameColumn(
                name: "LimitAmount",
                schema: "budgeting",
                table: "budgets",
                newName: "limit_amount"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "budgeting",
                table: "budgets",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsShared",
                schema: "budgeting",
                table: "budgets",
                newName: "is_shared"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "budgeting",
                table: "budgets",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "EndDate",
                schema: "budgeting",
                table: "budgets",
                newName: "end_date"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "budgeting",
                table: "budgets",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "budgeting",
                table: "budgets",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "Role",
                schema: "budgeting",
                table: "budget_members",
                newName: "role"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "budgeting",
                table: "budget_members",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "budgeting",
                table: "budget_members",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "JoinedDate",
                schema: "budgeting",
                table: "budget_members",
                newName: "joined_date"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "budgeting",
                table: "budget_members",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "BudgetId",
                schema: "budgeting",
                table: "budget_members",
                newName: "budget_id"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "budgeting",
                table: "budget_categories",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "CategoryLimitAmount",
                schema: "budgeting",
                table: "budget_categories",
                newName: "category_limit_amount"
            );

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                schema: "budgeting",
                table: "budget_categories",
                newName: "category_id"
            );

            migrationBuilder.RenameColumn(
                name: "BudgetId",
                schema: "budgeting",
                table: "budget_categories",
                newName: "budget_id"
            );

            migrationBuilder.RenameColumn(
                name: "Notes",
                schema: "finance",
                table: "accounts",
                newName: "notes"
            );

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "finance",
                table: "accounts",
                newName: "name"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "finance",
                table: "accounts",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finance",
                table: "accounts",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "finance",
                table: "accounts",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "finance",
                table: "accounts",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "LastFourDigits",
                schema: "finance",
                table: "accounts",
                newName: "last_four_digits"
            );

            migrationBuilder.RenameColumn(
                name: "IsHidden",
                schema: "finance",
                table: "accounts",
                newName: "is_hidden"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "finance",
                table: "accounts",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "InterestRate",
                schema: "finance",
                table: "accounts",
                newName: "interest_rate"
            );

            migrationBuilder.RenameColumn(
                name: "InstitutionName",
                schema: "finance",
                table: "accounts",
                newName: "institution_name"
            );

            migrationBuilder.RenameColumn(
                name: "IncludeInNetWorth",
                schema: "finance",
                table: "accounts",
                newName: "include_in_net_worth"
            );

            migrationBuilder.RenameColumn(
                name: "CurrentBalance",
                schema: "finance",
                table: "accounts",
                newName: "current_balance"
            );

            migrationBuilder.RenameColumn(
                name: "CurrencyCode",
                schema: "finance",
                table: "accounts",
                newName: "currency_code"
            );

            migrationBuilder.RenameColumn(
                name: "CreditLimit",
                schema: "finance",
                table: "accounts",
                newName: "credit_limit"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "finance",
                table: "accounts",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "finance",
                table: "accounts",
                newName: "create_date"
            );

            migrationBuilder.RenameColumn(
                name: "ClosedDate",
                schema: "finance",
                table: "accounts",
                newName: "closed_date"
            );

            migrationBuilder.RenameColumn(
                name: "AvailableBalance",
                schema: "finance",
                table: "accounts",
                newName: "available_balance"
            );

            migrationBuilder.RenameColumn(
                name: "AccountTypeId",
                schema: "finance",
                table: "accounts",
                newName: "account_type_id"
            );

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "finance",
                table: "account_types",
                newName: "name"
            );

            migrationBuilder.RenameColumn(
                name: "Description",
                schema: "finance",
                table: "account_types",
                newName: "description"
            );

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "finance",
                table: "account_types",
                newName: "id"
            );

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "finance",
                table: "account_types",
                newName: "user_id"
            );

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "finance",
                table: "account_types",
                newName: "updated_date"
            );

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                schema: "finance",
                table: "account_types",
                newName: "last_updated_by"
            );

            migrationBuilder.RenameColumn(
                name: "IsSystemDefault",
                schema: "finance",
                table: "account_types",
                newName: "is_system_default"
            );

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "finance",
                table: "account_types",
                newName: "is_active"
            );

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "finance",
                table: "account_types",
                newName: "created_by"
            );

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "finance",
                table: "account_types",
                newName: "create_date"
            );

            migrationBuilder.AddPrimaryKey(
                name: "pk_budget_categories",
                schema: "budgeting",
                table: "budget_categories",
                column: "id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_budget_categories",
                schema: "budgeting",
                table: "budget_categories"
            );

            migrationBuilder.RenameColumn(
                name: "password",
                schema: "identity",
                table: "users",
                newName: "Password"
            );

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "identity",
                table: "users",
                newName: "Email"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "identity",
                table: "users",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "profile_image_url",
                schema: "identity",
                table: "users",
                newName: "ProfileImageUrl"
            );

            migrationBuilder.RenameColumn(
                name: "is_email_verified",
                schema: "identity",
                table: "users",
                newName: "IsEmailVerified"
            );

            migrationBuilder.RenameColumn(
                name: "full_name",
                schema: "identity",
                table: "users",
                newName: "FullName"
            );

            migrationBuilder.RenameColumn(
                name: "email_verified_at",
                schema: "identity",
                table: "users",
                newName: "EmailVerifiedAt"
            );

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "finance",
                table: "transactions",
                newName: "Type"
            );

            migrationBuilder.RenameColumn(
                name: "tags",
                schema: "finance",
                table: "transactions",
                newName: "Tags"
            );

            migrationBuilder.RenameColumn(
                name: "status",
                schema: "finance",
                table: "transactions",
                newName: "Status"
            );

            migrationBuilder.RenameColumn(
                name: "notes",
                schema: "finance",
                table: "transactions",
                newName: "Notes"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "finance",
                table: "transactions",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "finance",
                table: "transactions",
                newName: "Amount"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "finance",
                table: "transactions",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "finance",
                table: "transactions",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "finance",
                table: "transactions",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "transaction_date",
                schema: "finance",
                table: "transactions",
                newName: "TransactionDate"
            );

            migrationBuilder.RenameColumn(
                name: "payment_method_id",
                schema: "finance",
                table: "transactions",
                newName: "PaymentMethodId"
            );

            migrationBuilder.RenameColumn(
                name: "merchant_name",
                schema: "finance",
                table: "transactions",
                newName: "MerchantName"
            );

            migrationBuilder.RenameColumn(
                name: "linked_transaction_id",
                schema: "finance",
                table: "transactions",
                newName: "LinkedTransactionId"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "finance",
                table: "transactions",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_recurring",
                schema: "finance",
                table: "transactions",
                newName: "IsRecurring"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "finance",
                table: "transactions",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "finance",
                table: "transactions",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "finance",
                table: "transactions",
                newName: "CategoryId"
            );

            migrationBuilder.RenameColumn(
                name: "budget_id",
                schema: "finance",
                table: "transactions",
                newName: "BudgetId"
            );

            migrationBuilder.RenameColumn(
                name: "account_id",
                schema: "finance",
                table: "transactions",
                newName: "AccountId"
            );

            migrationBuilder.RenameColumn(
                name: "account_balance_after_transaction",
                schema: "finance",
                table: "transactions",
                newName: "AccountBalanceAfterTransaction"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "identity",
                table: "refresh_tokens",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "identity",
                table: "refresh_tokens",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "identity",
                table: "refresh_tokens",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "token_hash",
                schema: "identity",
                table: "refresh_tokens",
                newName: "TokenHash"
            );

            migrationBuilder.RenameColumn(
                name: "revoked_at",
                schema: "identity",
                table: "refresh_tokens",
                newName: "RevokedAt"
            );

            migrationBuilder.RenameColumn(
                name: "revocation_reason",
                schema: "identity",
                table: "refresh_tokens",
                newName: "RevocationReason"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "identity",
                table: "refresh_tokens",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_revoked",
                schema: "identity",
                table: "refresh_tokens",
                newName: "IsRevoked"
            );

            migrationBuilder.RenameColumn(
                name: "expires_at",
                schema: "identity",
                table: "refresh_tokens",
                newName: "ExpiresAt"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "identity",
                table: "refresh_tokens",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "identity",
                table: "refresh_tokens",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "type",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Type"
            );

            migrationBuilder.RenameColumn(
                name: "notes",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Notes"
            );

            migrationBuilder.RenameColumn(
                name: "frequency",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Frequency"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "amount",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Amount"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "finance",
                table: "recurring_transactions",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "finance",
                table: "recurring_transactions",
                newName: "StartDate"
            );

            migrationBuilder.RenameColumn(
                name: "payment_method_id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "PaymentMethodId"
            );

            migrationBuilder.RenameColumn(
                name: "next_run_date",
                schema: "finance",
                table: "recurring_transactions",
                newName: "NextRunDate"
            );

            migrationBuilder.RenameColumn(
                name: "merchant_name",
                schema: "finance",
                table: "recurring_transactions",
                newName: "MerchantName"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "finance",
                table: "recurring_transactions",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "finance",
                table: "recurring_transactions",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "finance",
                table: "recurring_transactions",
                newName: "EndDate"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "finance",
                table: "recurring_transactions",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "finance",
                table: "recurring_transactions",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "CategoryId"
            );

            migrationBuilder.RenameColumn(
                name: "budget_id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "BudgetId"
            );

            migrationBuilder.RenameColumn(
                name: "account_id",
                schema: "finance",
                table: "recurring_transactions",
                newName: "AccountId"
            );

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "finance",
                table: "payment_methods",
                newName: "Name"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "finance",
                table: "payment_methods",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "finance",
                table: "payment_methods",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "finance",
                table: "payment_methods",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "finance",
                table: "payment_methods",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "finance",
                table: "payment_methods",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_system_default",
                schema: "finance",
                table: "payment_methods",
                newName: "IsSystemDefault"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "finance",
                table: "payment_methods",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "finance",
                table: "payment_methods",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "finance",
                table: "payment_methods",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "used_at",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "UsedAt"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "token_hash",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "TokenHash"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_revoked",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "IsRevoked"
            );

            migrationBuilder.RenameColumn(
                name: "expires_at",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "ExpiresAt"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "identity",
                table: "password_reset_tokens",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "used_at",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "UsedAt"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "token_hash",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "TokenHash"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_revoked",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "IsRevoked"
            );

            migrationBuilder.RenameColumn(
                name: "expires_at",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "ExpiresAt"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "identity",
                table: "email_verification_tokens",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "reference",
                table: "categories",
                newName: "Name"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "reference",
                table: "categories",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "reference",
                table: "categories",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "reference",
                table: "categories",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "reference",
                table: "categories",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "reference",
                table: "categories",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_system_default",
                schema: "reference",
                table: "categories",
                newName: "IsSystemDefault"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "reference",
                table: "categories",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "reference",
                table: "categories",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "reference",
                table: "categories",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "period",
                schema: "budgeting",
                table: "budgets",
                newName: "Period"
            );

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "budgeting",
                table: "budgets",
                newName: "Name"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "budgeting",
                table: "budgets",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "budgeting",
                table: "budgets",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "budgeting",
                table: "budgets",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "budgeting",
                table: "budgets",
                newName: "StartDate"
            );

            migrationBuilder.RenameColumn(
                name: "owner_user_id",
                schema: "budgeting",
                table: "budgets",
                newName: "OwnerUserId"
            );

            migrationBuilder.RenameColumn(
                name: "limit_amount",
                schema: "budgeting",
                table: "budgets",
                newName: "LimitAmount"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "budgeting",
                table: "budgets",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_shared",
                schema: "budgeting",
                table: "budgets",
                newName: "IsShared"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "budgeting",
                table: "budgets",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "budgeting",
                table: "budgets",
                newName: "EndDate"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "budgeting",
                table: "budgets",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "budgeting",
                table: "budgets",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "role",
                schema: "budgeting",
                table: "budget_members",
                newName: "Role"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "budgeting",
                table: "budget_members",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "budgeting",
                table: "budget_members",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "joined_date",
                schema: "budgeting",
                table: "budget_members",
                newName: "JoinedDate"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "budgeting",
                table: "budget_members",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "budget_id",
                schema: "budgeting",
                table: "budget_members",
                newName: "BudgetId"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "budgeting",
                table: "budget_categories",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "category_limit_amount",
                schema: "budgeting",
                table: "budget_categories",
                newName: "CategoryLimitAmount"
            );

            migrationBuilder.RenameColumn(
                name: "category_id",
                schema: "budgeting",
                table: "budget_categories",
                newName: "CategoryId"
            );

            migrationBuilder.RenameColumn(
                name: "budget_id",
                schema: "budgeting",
                table: "budget_categories",
                newName: "BudgetId"
            );

            migrationBuilder.RenameColumn(
                name: "notes",
                schema: "finance",
                table: "accounts",
                newName: "Notes"
            );

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "finance",
                table: "accounts",
                newName: "Name"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "finance",
                table: "accounts",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "finance",
                table: "accounts",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "finance",
                table: "accounts",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "finance",
                table: "accounts",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "last_four_digits",
                schema: "finance",
                table: "accounts",
                newName: "LastFourDigits"
            );

            migrationBuilder.RenameColumn(
                name: "is_hidden",
                schema: "finance",
                table: "accounts",
                newName: "IsHidden"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "finance",
                table: "accounts",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "interest_rate",
                schema: "finance",
                table: "accounts",
                newName: "InterestRate"
            );

            migrationBuilder.RenameColumn(
                name: "institution_name",
                schema: "finance",
                table: "accounts",
                newName: "InstitutionName"
            );

            migrationBuilder.RenameColumn(
                name: "include_in_net_worth",
                schema: "finance",
                table: "accounts",
                newName: "IncludeInNetWorth"
            );

            migrationBuilder.RenameColumn(
                name: "current_balance",
                schema: "finance",
                table: "accounts",
                newName: "CurrentBalance"
            );

            migrationBuilder.RenameColumn(
                name: "currency_code",
                schema: "finance",
                table: "accounts",
                newName: "CurrencyCode"
            );

            migrationBuilder.RenameColumn(
                name: "credit_limit",
                schema: "finance",
                table: "accounts",
                newName: "CreditLimit"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "finance",
                table: "accounts",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "finance",
                table: "accounts",
                newName: "CreateDate"
            );

            migrationBuilder.RenameColumn(
                name: "closed_date",
                schema: "finance",
                table: "accounts",
                newName: "ClosedDate"
            );

            migrationBuilder.RenameColumn(
                name: "available_balance",
                schema: "finance",
                table: "accounts",
                newName: "AvailableBalance"
            );

            migrationBuilder.RenameColumn(
                name: "account_type_id",
                schema: "finance",
                table: "accounts",
                newName: "AccountTypeId"
            );

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "finance",
                table: "account_types",
                newName: "Name"
            );

            migrationBuilder.RenameColumn(
                name: "description",
                schema: "finance",
                table: "account_types",
                newName: "Description"
            );

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "finance",
                table: "account_types",
                newName: "Id"
            );

            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "finance",
                table: "account_types",
                newName: "UserId"
            );

            migrationBuilder.RenameColumn(
                name: "updated_date",
                schema: "finance",
                table: "account_types",
                newName: "UpdatedDate"
            );

            migrationBuilder.RenameColumn(
                name: "last_updated_by",
                schema: "finance",
                table: "account_types",
                newName: "LastUpdatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "is_system_default",
                schema: "finance",
                table: "account_types",
                newName: "IsSystemDefault"
            );

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "finance",
                table: "account_types",
                newName: "IsActive"
            );

            migrationBuilder.RenameColumn(
                name: "created_by",
                schema: "finance",
                table: "account_types",
                newName: "CreatedBy"
            );

            migrationBuilder.RenameColumn(
                name: "create_date",
                schema: "finance",
                table: "account_types",
                newName: "CreateDate"
            );

            migrationBuilder.AddPrimaryKey(
                name: "PK_budget_categories",
                schema: "budgeting",
                table: "budget_categories",
                column: "Id"
            );
        }
    }
}
