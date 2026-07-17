using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AdjustColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "merchant_name",
                schema: "finance",
                table: "transactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "reference",
                table: "categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                schema: "budgeting",
                table: "budget_members",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<long>(
                name: "create_date",
                schema: "budgeting",
                table: "budget_members",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "created_by",
                schema: "budgeting",
                table: "budget_members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "deletion_indicator",
                schema: "budgeting",
                table: "budget_members",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "last_updated_by",
                schema: "budgeting",
                table: "budget_members",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "budgeting",
                table: "budget_members",
                type: "text",
                nullable: false,
                defaultValue: "")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AddColumn<long>(
                name: "updated_date",
                schema: "budgeting",
                table: "budget_members",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "finance",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100)
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                schema: "finance",
                table: "accounts",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AddColumn<bool>(
                name: "deletion_indicator",
                schema: "finance",
                table: "accounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "finance",
                table: "accounts",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                schema: "reference",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "create_date",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "created_by",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "deletion_indicator",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "last_updated_by",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "updated_date",
                schema: "budgeting",
                table: "budget_members");

            migrationBuilder.DropColumn(
                name: "deletion_indicator",
                schema: "finance",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "finance",
                table: "accounts");

            migrationBuilder.AlterColumn<string>(
                name: "merchant_name",
                schema: "finance",
                table: "transactions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                schema: "budgeting",
                table: "budget_members",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "finance",
                table: "accounts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100)
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<Guid>(
                name: "id",
                schema: "finance",
                table: "accounts",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .OldAnnotation("Relational:ColumnOrder", 1);
        }
    }
}
