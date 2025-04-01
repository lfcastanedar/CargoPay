using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTransactionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeeRate",
                table: "Transactions",
                newName: "UFE");

            migrationBuilder.RenameColumn(
                name: "Fee",
                table: "Transactions",
                newName: "PaymentFee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UFE",
                table: "Transactions",
                newName: "FeeRate");

            migrationBuilder.RenameColumn(
                name: "PaymentFee",
                table: "Transactions",
                newName: "Fee");
        }
    }
}
