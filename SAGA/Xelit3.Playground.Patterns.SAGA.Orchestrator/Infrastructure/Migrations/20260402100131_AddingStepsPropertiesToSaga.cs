using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Xelit3.Playground.Patterns.SAGA.Orchestrator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingStepsPropertiesToSaga : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PaymentAmount",
                table: "UserBillingSagas",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentResultCode",
                table: "UserBillingSagas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaymentSuccessful",
                table: "UserBillingSagas",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlanStatus",
                table: "UserBillingSagas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentAmount",
                table: "UserBillingSagas");

            migrationBuilder.DropColumn(
                name: "PaymentResultCode",
                table: "UserBillingSagas");

            migrationBuilder.DropColumn(
                name: "PaymentSuccessful",
                table: "UserBillingSagas");

            migrationBuilder.DropColumn(
                name: "PlanStatus",
                table: "UserBillingSagas");
        }
    }
}
