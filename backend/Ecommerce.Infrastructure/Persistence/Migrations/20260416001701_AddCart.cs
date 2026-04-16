using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_carts_cart_id",
                table: "cart_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items");

            migrationBuilder.DropIndex(
                name: "ix_cart_items_cart_id",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "product_name",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "id",
                table: "carts");

            migrationBuilder.DropColumn(
                name: "id",
                table: "cart_items");

            migrationBuilder.DropColumn(
                name: "product_name",
                table: "cart_items");

            migrationBuilder.RenameColumn(
                name: "variant_id",
                table: "cart_items",
                newName: "cart_user_id");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "cart_items",
                newName: "product_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_carts",
                table: "carts",
                column: "user_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items",
                columns: new[] { "product_id", "cart_user_id" });

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_user_id",
                table: "cart_items",
                column: "cart_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_carts_cart_user_id",
                table: "cart_items",
                column: "cart_user_id",
                principalTable: "carts",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cart_items_carts_cart_user_id",
                table: "cart_items");

            migrationBuilder.DropPrimaryKey(
                name: "pk_carts",
                table: "carts");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items");

            migrationBuilder.DropIndex(
                name: "ix_cart_items_cart_user_id",
                table: "cart_items");

            migrationBuilder.RenameColumn(
                name: "cart_user_id",
                table: "cart_items",
                newName: "variant_id");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "cart_items",
                newName: "cart_id");

            migrationBuilder.AddColumn<string>(
                name: "product_name",
                table: "order_items",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "carts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "id",
                table: "cart_items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "product_name",
                table: "cart_items",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "pk_carts",
                table: "carts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cart_items",
                table: "cart_items",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.AddForeignKey(
                name: "fk_cart_items_carts_cart_id",
                table: "cart_items",
                column: "cart_id",
                principalTable: "carts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
