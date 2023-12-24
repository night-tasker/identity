using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NightTasker.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeNamesByConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_refresh_token_asp_net_users_user_id",
                table: "user_refresh_token");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_refresh_token",
                table: "user_refresh_token");

            migrationBuilder.RenameTable(
                name: "user_refresh_token",
                newName: "user_refresh_tokens");

            migrationBuilder.RenameTable(
                name: "asp_net_users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "asp_net_user_tokens",
                newName: "user_tokens");

            migrationBuilder.RenameTable(
                name: "asp_net_user_roles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "asp_net_user_logins",
                newName: "user_logins");

            migrationBuilder.RenameTable(
                name: "asp_net_user_claims",
                newName: "user_claims");

            migrationBuilder.RenameTable(
                name: "asp_net_roles",
                newName: "roles");

            migrationBuilder.RenameTable(
                name: "asp_net_role_claims",
                newName: "role_claims");

            migrationBuilder.RenameIndex(
                name: "ix_user_refresh_token_user_id",
                table: "user_refresh_tokens",
                newName: "ix_user_refresh_tokens_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_refresh_tokens",
                table: "user_refresh_tokens",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_refresh_tokens_users_user_id",
                table: "user_refresh_tokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_refresh_tokens_users_user_id",
                table: "user_refresh_tokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_refresh_tokens",
                table: "user_refresh_tokens");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "asp_net_users");

            migrationBuilder.RenameTable(
                name: "user_tokens",
                newName: "asp_net_user_tokens");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "asp_net_user_roles");

            migrationBuilder.RenameTable(
                name: "user_refresh_tokens",
                newName: "user_refresh_token");

            migrationBuilder.RenameTable(
                name: "user_logins",
                newName: "asp_net_user_logins");

            migrationBuilder.RenameTable(
                name: "user_claims",
                newName: "asp_net_user_claims");

            migrationBuilder.RenameTable(
                name: "roles",
                newName: "asp_net_roles");

            migrationBuilder.RenameTable(
                name: "role_claims",
                newName: "asp_net_role_claims");

            migrationBuilder.RenameIndex(
                name: "ix_user_refresh_tokens_user_id",
                table: "user_refresh_token",
                newName: "ix_user_refresh_token_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_refresh_token",
                table: "user_refresh_token",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_refresh_token_asp_net_users_user_id",
                table: "user_refresh_token",
                column: "user_id",
                principalTable: "asp_net_users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
