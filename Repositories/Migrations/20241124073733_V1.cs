using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifedDate",
                table: "PublishingHouses",
                newName: "LastModifedDate");

            migrationBuilder.RenameColumn(
                name: "ModifedDate",
                table: "Books",
                newName: "LastModifedDate");

            migrationBuilder.RenameColumn(
                name: "ModifedDate",
                table: "Authors",
                newName: "LastModifedDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PublishingHouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PublishingHouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PublishingHouses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PublishingHouses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "PublishingHouses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "PublishingHouses");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "PublishingHouses",
                newName: "ModifedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Books",
                newName: "ModifedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifedDate",
                table: "Authors",
                newName: "ModifedDate");
        }
    }
}
