using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TVShowTraker.Migrations
{
    public partial class AddUserFavourits1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourits_AspNetUsers_ApplicationUserId",
                table: "UserFavourits");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourits_TVShows_TVShowId",
                table: "UserFavourits");

            migrationBuilder.DropIndex(
                name: "IX_UserFavourits_ApplicationUserId",
                table: "UserFavourits");

            migrationBuilder.DropIndex(
                name: "IX_UserFavourits_TVShowId",
                table: "UserFavourits");

            migrationBuilder.AlterColumn<Guid>(
                name: "ApplicationUserId",
                table: "UserFavourits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "UserFavourits",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourits_ApplicationUserId1",
                table: "UserFavourits",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourits_AspNetUsers_ApplicationUserId1",
                table: "UserFavourits",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourits_AspNetUsers_ApplicationUserId1",
                table: "UserFavourits");

            migrationBuilder.DropIndex(
                name: "IX_UserFavourits_ApplicationUserId1",
                table: "UserFavourits");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "UserFavourits");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "UserFavourits",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourits_ApplicationUserId",
                table: "UserFavourits",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFavourits_TVShowId",
                table: "UserFavourits",
                column: "TVShowId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourits_AspNetUsers_ApplicationUserId",
                table: "UserFavourits",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourits_TVShows_TVShowId",
                table: "UserFavourits",
                column: "TVShowId",
                principalTable: "TVShows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
