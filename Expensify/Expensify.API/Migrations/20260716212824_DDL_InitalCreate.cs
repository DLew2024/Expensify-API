using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "public");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(
                        type: "character varying(200)",
                        maxLength: 200,
                        nullable: false
                    ),
                    Email = table.Column<string>(
                        type: "character varying(320)",
                        maxLength: 320,
                        nullable: false
                    ),
                    Password = table.Column<string>(
                        type: "character varying(500)",
                        maxLength: 500,
                        nullable: false
                    ),
                    ProfileImageUrl = table.Column<string>(
                        type: "character varying(2048)",
                        maxLength: 2048,
                        nullable: false
                    ),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false),
                    EmailVerifiedAt = table.Column<long>(type: "bigint", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "AccountType",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountType_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    OwnerUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    LimitAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsShared = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "fk_budgets_users_owner_user_id",
                        column: x => x.OwnerUserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "EmailVerificationTokens",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<long>(type: "bigint", nullable: false),
                    UsedAt = table.Column<long>(type: "bigint", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailVerificationTokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "PasswordResetTokens",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<long>(type: "bigint", nullable: false),
                    UsedAt = table.Column<long>(type: "bigint", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordResetTokens", x => x.Id);
                    table.ForeignKey(
                        name: "fk_password_reset_tokens_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsSystemDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Icon = table.Column<string>(type: "text", nullable: true),
                    Color = table.Column<string>(type: "text", nullable: true),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentMethod_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<long>(type: "bigint", nullable: false),
                    RevokedAt = table.Column<long>(type: "bigint", nullable: true),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: false),
                    RevocationReason = table.Column<string>(type: "text", nullable: true),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Account",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    AccountTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    InstitutionName = table.Column<string>(type: "text", nullable: true),
                    LastFourDigits = table.Column<string>(type: "text", nullable: true),
                    CurrencyCode = table.Column<int>(type: "integer", nullable: false),
                    CurrentBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    AvailableBalance = table.Column<decimal>(type: "numeric", nullable: false),
                    IncludeInNetWorth = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    CreditLimit = table.Column<decimal>(type: "numeric", nullable: true),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: true),
                    ClosedDate = table.Column<long>(type: "bigint", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Account_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalSchema: "public",
                        principalTable: "AccountType",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "fk_accounts_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "BudgetMember",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    JoinedDate = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetMember_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "public",
                        principalTable: "Budget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "fk_budget_memberships_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "BudgetCategory",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryLimitAmount = table.Column<decimal>(type: "numeric", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetCategory_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "public",
                        principalTable: "Budget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_BudgetCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "RecurringTransaction",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MerchantName = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    Frequency = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<long>(type: "bigint", nullable: false),
                    EndDate = table.Column<long>(type: "bigint", nullable: true),
                    NextRunDate = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecurringTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "public",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "public",
                        principalTable: "Budget",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Category",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_RecurringTransaction_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "public",
                        principalTable: "PaymentMethod",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "fk_recurring_transactions_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uuid", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    AccountBalanceAfterTransaction = table.Column<decimal>(
                        type: "numeric",
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TransactionDate = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    MerchantName = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    IsRecurring = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentMethodId = table.Column<Guid>(type: "uuid", nullable: true),
                    Tags = table.Column<List<string>>(type: "text[]", nullable: false),
                    LinkedTransactionId = table.Column<Guid>(type: "uuid", nullable: true),
                    deletion_indicator = table.Column<bool>(type: "boolean", nullable: false),
                    LastUpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateDate = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedDate = table.Column<long>(type: "bigint", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Account_AccountId",
                        column: x => x.AccountId,
                        principalSchema: "public",
                        principalTable: "Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "public",
                        principalTable: "Budget",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "Category",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalSchema: "public",
                        principalTable: "PaymentMethod",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Transactions_Transactions_LinkedTransactionId",
                        column: x => x.LinkedTransactionId,
                        principalSchema: "public",
                        principalTable: "Transactions",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "fk_transactions_users_user_id",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Account_AccountTypeId",
                schema: "public",
                table: "Account",
                column: "AccountTypeId"
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
                name: "IX_Budget_OwnerUserId",
                schema: "public",
                table: "Budget",
                column: "OwnerUserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_BudgetId",
                schema: "public",
                table: "BudgetCategory",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetCategory_CategoryId",
                schema: "public",
                table: "BudgetCategory",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMember_BudgetId",
                schema: "public",
                table: "BudgetMember",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_BudgetMember_UserId",
                schema: "public",
                table: "BudgetMember",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Category_UserId",
                schema: "public",
                table: "Category",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_TokenHash",
                schema: "public",
                table: "EmailVerificationTokens",
                column: "TokenHash",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_EmailVerificationTokens_UserId",
                schema: "public",
                table: "EmailVerificationTokens",
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
                name: "IX_PaymentMethod_UserId",
                schema: "public",
                table: "PaymentMethod",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_AccountId",
                schema: "public",
                table: "RecurringTransaction",
                column: "AccountId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_BudgetId",
                schema: "public",
                table: "RecurringTransaction",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_CategoryId",
                schema: "public",
                table: "RecurringTransaction",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_PaymentMethodId",
                schema: "public",
                table: "RecurringTransaction",
                column: "PaymentMethodId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_RecurringTransaction_UserId",
                schema: "public",
                table: "RecurringTransaction",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_TokenHash",
                schema: "public",
                table: "refresh_tokens",
                column: "TokenHash",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_UserId",
                schema: "public",
                table: "refresh_tokens",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AccountId",
                schema: "public",
                table: "Transactions",
                column: "AccountId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BudgetId",
                schema: "public",
                table: "Transactions",
                column: "BudgetId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                schema: "public",
                table: "Transactions",
                column: "CategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LinkedTransactionId",
                schema: "public",
                table: "Transactions",
                column: "LinkedTransactionId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentMethodId",
                schema: "public",
                table: "Transactions",
                column: "PaymentMethodId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                schema: "public",
                table: "Transactions",
                column: "UserId"
            );

            migrationBuilder.CreateIndex(
                name: "ux_users_email",
                schema: "public",
                table: "users",
                column: "Email",
                unique: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "BudgetCategory", schema: "public");

            migrationBuilder.DropTable(name: "BudgetMember", schema: "public");

            migrationBuilder.DropTable(name: "EmailVerificationTokens", schema: "public");

            migrationBuilder.DropTable(name: "PasswordResetTokens", schema: "public");

            migrationBuilder.DropTable(name: "RecurringTransaction", schema: "public");

            migrationBuilder.DropTable(name: "refresh_tokens", schema: "public");

            migrationBuilder.DropTable(name: "Transactions", schema: "public");

            migrationBuilder.DropTable(name: "Account", schema: "public");

            migrationBuilder.DropTable(name: "Budget", schema: "public");

            migrationBuilder.DropTable(name: "Category", schema: "public");

            migrationBuilder.DropTable(name: "PaymentMethod", schema: "public");

            migrationBuilder.DropTable(name: "AccountType", schema: "public");

            migrationBuilder.DropTable(name: "users", schema: "public");
        }
    }
}
