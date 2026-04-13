using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateResourceFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_Resources_ResourceId",
                table: "Features");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceFeatures",
                table: "ResourceFeatures");

            migrationBuilder.DropIndex(
                name: "IX_Features_ResourceId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ResourceFeatures");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ResourceFeatures",
                newName: "FeatureId");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "ResourceFeatures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceFeatures",
                table: "ResourceFeatures",
                columns: new[] { "ResourceId", "FeatureId" });

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CategoryId",
                table: "Resources",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Resources_LocationId",
                table: "Resources",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ResourceFeatures_FeatureId",
                table: "ResourceFeatures",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceFeatures_Features_FeatureId",
                table: "ResourceFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceFeatures_Resources_ResourceId",
                table: "ResourceFeatures",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Categories_CategoryId",
                table: "Resources",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Locations_LocationId",
                table: "Resources",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResourceFeatures_Features_FeatureId",
                table: "ResourceFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceFeatures_Resources_ResourceId",
                table: "ResourceFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Categories_CategoryId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Locations_LocationId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_CategoryId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_LocationId",
                table: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ResourceFeatures",
                table: "ResourceFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ResourceFeatures_FeatureId",
                table: "ResourceFeatures");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "ResourceFeatures");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "ResourceFeatures",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ResourceFeatures",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "Features",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ResourceFeatures",
                table: "ResourceFeatures",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ResourceId",
                table: "Features",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_Resources_ResourceId",
                table: "Features",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }
    }
}
