using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingProcesses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillingDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalUsersToProcess = table.Column<int>(type: "int", nullable: false),
                    DiscountsCalculated = table.Column<int>(type: "int", nullable: false),
                    PaymentsProcessed = table.Column<int>(type: "int", nullable: false),
                    Completed = table.Column<int>(type: "int", nullable: false),
                    Failed = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingProcesses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingProcesses_BillingDate",
                table: "BillingProcesses",
                column: "BillingDate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillingProcesses_Status",
                table: "BillingProcesses",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingProcesses");
        }
    }
}
