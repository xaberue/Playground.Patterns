using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserBillingSAGATable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBillingSagas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    LastError = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBillingSagas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBillingSagas_BillingProcessId",
                table: "UserBillingSagas",
                column: "BillingProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBillingSagas_Status",
                table: "UserBillingSagas",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_UserBillingSagas_UserId",
                table: "UserBillingSagas",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBillingSagas");
        }
    }
}
