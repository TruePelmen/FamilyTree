#nullable disable

namespace FamilyTree.DAL.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "event_id_seq");

            migrationBuilder.CreateSequence(
                name: "media_event_id_seq");

            migrationBuilder.CreateSequence(
                name: "media_id_seq");

            migrationBuilder.CreateSequence(
                name: "media_person_id_seq");

            migrationBuilder.CreateSequence(
                name: "person_id_seq");

            migrationBuilder.CreateSequence(
                name: "relationship_id_seq");

            migrationBuilder.CreateSequence(
                name: "special_record_id_seq");

            migrationBuilder.CreateSequence(
                name: "tree_id_seq");

            migrationBuilder.CreateSequence(
                name: "tree_person_id_seq");

            migrationBuilder.CreateSequence(
                name: "user_tree_id_seq");

            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('media_id_seq'::regclass)"),
                    media_type = table.Column<string>(type: "character varying", nullable: true),
                    file_path = table.Column<string>(type: "character varying", nullable: true),
                    tagged_person = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    place = table.Column<string>(type: "character varying", nullable: true),
                    main_photo = table.Column<bool>(type: "boolean", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("media_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('person_id_seq'::regclass)"),
                    gender = table.Column<string>(type: "character varying", nullable: true),
                    last_name = table.Column<string>(type: "character varying", nullable: true),
                    maiden_name = table.Column<string>(type: "character varying", nullable: true),
                    first_name = table.Column<string>(type: "character varying", nullable: true),
                    other_name_variants = table.Column<string>(type: "character varying", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    death_date = table.Column<DateOnly>(type: "date", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("person_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tree",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('tree_id_seq'::regclass)"),
                    name = table.Column<string>(type: "character varying", nullable: true),
                    primary_person = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("tree_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    login = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('event_id_seq'::regclass)"),
                    event_type = table.Column<string>(type: "character varying", nullable: true),
                    event_date = table.Column<DateOnly>(type: "date", nullable: true),
                    event_place = table.Column<string>(type: "character varying", nullable: true),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("event_pkey", x => x.id);
                    table.ForeignKey(
                        name: "event_person_id_fkey",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Media_Person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('media_person_id_seq'::regclass)"),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    media_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("media_person_pkey", x => x.id);
                    table.ForeignKey(
                        name: "media_person_media_id_fkey",
                        column: x => x.media_id,
                        principalTable: "Media",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "media_person_person_id_fkey",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Relationship",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('relationship_id_seq'::regclass)"),
                    person_id1 = table.Column<int>(type: "integer", nullable: false),
                    person_id2 = table.Column<int>(type: "integer", nullable: false),
                    relationship_type = table.Column<string>(type: "character varying", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("relationship_pkey", x => x.id);
                    table.ForeignKey(
                        name: "relationship_person_id1_fkey",
                        column: x => x.person_id1,
                        principalTable: "Person",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "relationship_person_id2_fkey",
                        column: x => x.person_id2,
                        principalTable: "Person",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Tree_Person",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('tree_person_id_seq'::regclass)"),
                    tree_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("tree_person_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tree_person_person_id_fkey",
                        column: x => x.person_id,
                        principalTable: "Person",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "tree_person_tree_id_fkey",
                        column: x => x.tree_id,
                        principalTable: "Tree",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "User_Tree",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('user_tree_id_seq'::regclass)"),
                    user_login = table.Column<string>(type: "character varying", nullable: true),
                    tree_id = table.Column<int>(type: "integer", nullable: false),
                    access_type = table.Column<string>(type: "character varying", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_tree_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_tree_tree_id_fkey",
                        column: x => x.tree_id,
                        principalTable: "Tree",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_tree_user_login_fkey",
                        column: x => x.user_login,
                        principalTable: "Users",
                        principalColumn: "login");
                });

            migrationBuilder.CreateTable(
                name: "Media_Event",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('media_event_id_seq'::regclass)"),
                    event_id = table.Column<int>(type: "integer", nullable: true),
                    media_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("media_event_pkey", x => x.id);
                    table.ForeignKey(
                        name: "media_event_event_id_fkey",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "media_event_media_id_fkey",
                        column: x => x.media_id,
                        principalTable: "Media",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Special_record",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "nextval('special_record_id_seq'::regclass)"),
                    record_type = table.Column<string>(type: "character varying", nullable: true),
                    house_number = table.Column<int>(type: "integer", nullable: true),
                    priest = table.Column<string>(type: "character varying", nullable: true),
                    record = table.Column<string>(type: "character varying", nullable: true),
                    event_id = table.Column<int>(type: "integer", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("special_record_pkey", x => x.id);
                    table.ForeignKey(
                        name: "special_record_event_id_fkey",
                        column: x => x.event_id,
                        principalTable: "Event",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Event_person_id",
                table: "Event",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Event_event_id",
                table: "Media_Event",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Event_media_id",
                table: "Media_Event",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Person_media_id",
                table: "Media_Person",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "IX_Media_Person_person_id",
                table: "Media_Person",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_person_id1",
                table: "Relationship",
                column: "person_id1");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_person_id2",
                table: "Relationship",
                column: "person_id2");

            migrationBuilder.CreateIndex(
                name: "IX_Special_record_event_id",
                table: "Special_record",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tree_Person_person_id",
                table: "Tree_Person",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tree_Person_tree_id",
                table: "Tree_Person",
                column: "tree_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Tree_tree_id",
                table: "User_Tree",
                column: "tree_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_Tree_user_login",
                table: "User_Tree",
                column: "user_login");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Media_Event");

            migrationBuilder.DropTable(
                name: "Media_Person");

            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropTable(
                name: "Special_record");

            migrationBuilder.DropTable(
                name: "Tree_Person");

            migrationBuilder.DropTable(
                name: "User_Tree");

            migrationBuilder.DropTable(
                name: "Media");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Tree");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropSequence(
                name: "event_id_seq");

            migrationBuilder.DropSequence(
                name: "media_event_id_seq");

            migrationBuilder.DropSequence(
                name: "media_id_seq");

            migrationBuilder.DropSequence(
                name: "media_person_id_seq");

            migrationBuilder.DropSequence(
                name: "person_id_seq");

            migrationBuilder.DropSequence(
                name: "relationship_id_seq");

            migrationBuilder.DropSequence(
                name: "special_record_id_seq");

            migrationBuilder.DropSequence(
                name: "tree_id_seq");

            migrationBuilder.DropSequence(
                name: "tree_person_id_seq");

            migrationBuilder.DropSequence(
                name: "user_tree_id_seq");
        }
    }
}
