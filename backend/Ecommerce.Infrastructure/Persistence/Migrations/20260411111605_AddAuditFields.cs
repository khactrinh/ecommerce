using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });
            
            var defaultCategoryId = Guid.NewGuid();

            migrationBuilder.Sql($@"
                INSERT INTO categories (id, name)
                VALUES ('{defaultCategoryId}', 'Default');
            ");
            
            migrationBuilder.AddColumn<Guid>(
                name: "category_id",
                table: "products",
                type: "uuid",
                nullable: true); // 🔥 cho phép null tạm thời
            
            migrationBuilder.Sql($@"
                UPDATE products
                SET category_id = '{defaultCategoryId}'
                WHERE category_id IS NULL;
            ");
            
            migrationBuilder.AlterColumn<Guid>(
                name: "category_id",
                table: "products",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_at",
                table: "products",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql(@"
                ALTER TABLE orders 
                ALTER COLUMN status TYPE integer 
                USING CASE
                    WHEN status = 'Pending' THEN 0
                    WHEN status = 'Paid' THEN 1
                    WHEN status = 'Cancelled' THEN 2
                    ELSE 0
                END;
            ");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            

            migrationBuilder.CreateIndex(
                name: "ix_products_category_id",
                table: "products",
                column: "category_id");

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
                name: "ix_products_category_id",
                table: "products");
            
            migrationBuilder.DropColumn(
                name: "category_id",
                table: "products");
            
            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "products");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "products");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "orders");

            migrationBuilder.Sql(@"
                ALTER TABLE orders 
                ALTER COLUMN status TYPE text 
                USING CASE
                    WHEN status = 0 THEN 'Pending'
                    WHEN status = 1 THEN 'Paid'
                    WHEN status = 2 THEN 'Cancelled'
                    ELSE 'Pending'
                END;
            ");
        }
    }
}
