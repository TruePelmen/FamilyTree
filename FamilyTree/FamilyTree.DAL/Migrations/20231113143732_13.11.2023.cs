#nullable disable

namespace FamilyTree.DAL.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class _13112023 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "event_person_id_fkey",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "media_event_media_id_fkey",
                table: "Media_Event");

            migrationBuilder.DropForeignKey(
                name: "media_person_media_id_fkey",
                table: "Media_Person");

            migrationBuilder.DropForeignKey(
                name: "media_person_person_id_fkey",
                table: "Media_Person");

            migrationBuilder.DropForeignKey(
                name: "relationship_person_id1_fkey",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "relationship_person_id2_fkey",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "special_record_event_id_fkey",
                table: "Special_record");

            migrationBuilder.DropForeignKey(
                name: "tree_person_person_id_fkey",
                table: "Tree_Person");

            migrationBuilder.DropForeignKey(
                name: "tree_person_tree_id_fkey",
                table: "Tree_Person");

            migrationBuilder.DropForeignKey(
                name: "user_tree_tree_id_fkey",
                table: "User_Tree");

            migrationBuilder.DropForeignKey(
                name: "user_tree_user_login_fkey",
                table: "User_Tree");

            migrationBuilder.DropColumn(
                name: "primary_person",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "event_id",
                table: "Special_record",
                newName: "media_id");

            migrationBuilder.RenameIndex(
                name: "IX_Special_record_event_id",
                table: "Special_record",
                newName: "IX_Special_record_media_id");

            migrationBuilder.AddColumn<int>(
                name: "primary_person",
                table: "Tree",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "Person",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Person",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "event_type",
                table: "Event",
                type: "character varying",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "event_person_id_fkey",
                table: "Event",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "media_event_media_id_fkey",
                table: "Media_Event",
                column: "media_id",
                principalTable: "Media",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "media_person_media_id_fkey",
                table: "Media_Person",
                column: "media_id",
                principalTable: "Media",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "media_person_person_id_fkey",
                table: "Media_Person",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "relationship_person_id1_fkey",
                table: "Relationship",
                column: "person_id1",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "relationship_person_id2_fkey",
                table: "Relationship",
                column: "person_id2",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "special_record_event_id_fkey",
                table: "Special_record",
                column: "media_id",
                principalTable: "Event",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "tree_person_person_id_fkey",
                table: "Tree_Person",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "tree_person_tree_id_fkey",
                table: "Tree_Person",
                column: "tree_id",
                principalTable: "Tree",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "user_tree_tree_id_fkey",
                table: "User_Tree",
                column: "tree_id",
                principalTable: "Tree",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "user_tree_user_login_fkey",
                table: "User_Tree",
                column: "user_login",
                principalTable: "Users",
                principalColumn: "login",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "event_person_id_fkey",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "media_event_media_id_fkey",
                table: "Media_Event");

            migrationBuilder.DropForeignKey(
                name: "media_person_media_id_fkey",
                table: "Media_Person");

            migrationBuilder.DropForeignKey(
                name: "media_person_person_id_fkey",
                table: "Media_Person");

            migrationBuilder.DropForeignKey(
                name: "relationship_person_id1_fkey",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "relationship_person_id2_fkey",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "special_record_event_id_fkey",
                table: "Special_record");

            migrationBuilder.DropForeignKey(
                name: "tree_person_person_id_fkey",
                table: "Tree_Person");

            migrationBuilder.DropForeignKey(
                name: "tree_person_tree_id_fkey",
                table: "Tree_Person");

            migrationBuilder.DropForeignKey(
                name: "user_tree_tree_id_fkey",
                table: "User_Tree");

            migrationBuilder.DropForeignKey(
                name: "user_tree_user_login_fkey",
                table: "User_Tree");

            migrationBuilder.DropColumn(
                name: "primary_person",
                table: "Tree");

            migrationBuilder.RenameColumn(
                name: "media_id",
                table: "Special_record",
                newName: "event_id");

            migrationBuilder.RenameIndex(
                name: "IX_Special_record_media_id",
                table: "Special_record",
                newName: "IX_Special_record_event_id");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "Person",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Person",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AddColumn<bool>(
                name: "primary_person",
                table: "Person",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "event_type",
                table: "Event",
                type: "character varying",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying");

            migrationBuilder.AddForeignKey(
                name: "event_person_id_fkey",
                table: "Event",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "media_event_media_id_fkey",
                table: "Media_Event",
                column: "media_id",
                principalTable: "Media",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "media_person_media_id_fkey",
                table: "Media_Person",
                column: "media_id",
                principalTable: "Media",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "media_person_person_id_fkey",
                table: "Media_Person",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "relationship_person_id1_fkey",
                table: "Relationship",
                column: "person_id1",
                principalTable: "Person",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "relationship_person_id2_fkey",
                table: "Relationship",
                column: "person_id2",
                principalTable: "Person",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "special_record_event_id_fkey",
                table: "Special_record",
                column: "event_id",
                principalTable: "Event",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "tree_person_person_id_fkey",
                table: "Tree_Person",
                column: "person_id",
                principalTable: "Person",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "tree_person_tree_id_fkey",
                table: "Tree_Person",
                column: "tree_id",
                principalTable: "Tree",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "user_tree_tree_id_fkey",
                table: "User_Tree",
                column: "tree_id",
                principalTable: "Tree",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "user_tree_user_login_fkey",
                table: "User_Tree",
                column: "user_login",
                principalTable: "Users",
                principalColumn: "login");
        }
    }
}
