using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReletedPersonConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersons_RelatedToPersonId",
                table: "RelatedPersons",
                column: "RelatedToPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedToPersonId",
                table: "RelatedPersons",
                column: "RelatedToPersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_RelatedPersons_Persons_RelatedToPersonId",
                table: "RelatedPersons");

            migrationBuilder.DropIndex(
                name: "IX_RelatedPersons_RelatedToPersonId",
                table: "RelatedPersons");

            migrationBuilder.AddForeignKey(
                name: "FK_RelatedPersons_Persons_PersonId",
                table: "RelatedPersons",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
