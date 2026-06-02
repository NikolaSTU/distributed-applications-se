using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedcascade2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Meals_MealEntryId",
                table: "FoodEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Meals_MealEntryId",
                table: "FoodEntries",
                column: "MealEntryId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FoodEntries_Meals_MealEntryId",
                table: "FoodEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_FoodEntries_Meals_MealEntryId",
                table: "FoodEntries",
                column: "MealEntryId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
