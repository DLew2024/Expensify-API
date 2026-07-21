using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Expensify.API.Migrations
{
    /// <inheritdoc />
    public partial class DDL_AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "role_id",
                schema: "identity",
                table: "users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("7e04d972-5cde-4156-8ac4-b5659549236b")
            );

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(
                        type: "character varying(20)",
                        maxLength: 20,
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                }
            );

            migrationBuilder.InsertData(
                schema: "identity",
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { new Guid("7e04d972-5cde-4156-8ac4-b5659549236b"), "User" },
                    { new Guid("aa911248-931b-4cb2-9a65-451081aa3976"), "Admin" },
                }
            );

            migrationBuilder.CreateIndex(
                name: "ix_users_role_id",
                schema: "identity",
                table: "users",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "ux_roles_name",
                schema: "identity",
                table: "roles",
                column: "name",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "fk_users_roles_role_id",
                schema: "identity",
                table: "users",
                column: "role_id",
                principalSchema: "identity",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_roles_role_id",
                schema: "identity",
                table: "users"
            );

            migrationBuilder.DropTable(name: "roles", schema: "identity");

            migrationBuilder.DropIndex(
                name: "ix_users_role_id",
                schema: "identity",
                table: "users"
            );

            migrationBuilder.DropColumn(name: "role_id", schema: "identity", table: "users");
        }
    }
}
