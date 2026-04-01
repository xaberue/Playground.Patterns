using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBillingProcessIdToJobId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BillingProcessId",
                table: "UserBillingSagas",
                newName: "JobId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBillingSagas_BillingProcessId",
                table: "UserBillingSagas",
                newName: "IX_UserBillingSagas_JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "UserBillingSagas",
                newName: "BillingProcessId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBillingSagas_JobId",
                table: "UserBillingSagas",
                newName: "IX_UserBillingSagas_BillingProcessId");
        }
    }
}
