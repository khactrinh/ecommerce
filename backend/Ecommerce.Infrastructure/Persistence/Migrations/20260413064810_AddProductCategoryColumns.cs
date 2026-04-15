using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductCategoryColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                table: "products");

            migrationBuilder.RenameIndex(
                name: "ix_products_category_id",
                table: "products",
                newName: "idx_products_category");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "categories",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "categories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "categories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "parent_id",
                table: "categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "slug",
                table: "categories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "categories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_products_name",
                table: "products",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "idx_products_price",
                table: "products",
                column: "price");

            migrationBuilder.CreateIndex(
                name: "ix_categories_name",
                table: "categories",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_categories_slug",
                table: "categories",
                column: "slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_products_categories_category_id",
                table: "products");

            migrationBuilder.DropIndex(
                name: "idx_products_name",
                table: "products");

            migrationBuilder.DropIndex(
                name: "idx_products_price",
                table: "products");

            migrationBuilder.DropIndex(
                name: "ix_categories_name",
                table: "categories");

            migrationBuilder.DropIndex(
                name: "ix_categories_slug",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "description",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "parent_id",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "slug",
                table: "categories");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "categories");

            migrationBuilder.RenameIndex(
                name: "idx_products_category",
                table: "products",
                newName: "ix_products_category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_products_categories_category_id",
                table: "products",
                column: "category_id",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
