using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUniquenessOfBillingDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BillingProcesses_BillingDate",
                table: "BillingProcesses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BillingProcesses_BillingDate",
                table: "BillingProcesses",
                column: "BillingDate",
                unique: true);
        }
    }
}
