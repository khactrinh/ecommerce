using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddOutboxColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "retry_count",
                table: "outbox_messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "idx_outbox_unprocessed",
                table: "outbox_messages",
                column: "processed_on");

            migrationBuilder.CreateIndex(
                name: "ix_outbox_messages_processed_on_occurred_on",
                table: "outbox_messages",
                columns: new[] { "processed_on", "occurred_on" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_outbox_unprocessed",
                table: "outbox_messages");

            migrationBuilder.DropIndex(
                name: "ix_outbox_messages_processed_on_occurred_on",
                table: "outbox_messages");

            migrationBuilder.DropColumn(
                name: "retry_count",
                table: "outbox_messages");
        }
    }
}
