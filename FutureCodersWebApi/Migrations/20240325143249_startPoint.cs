using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FutureCodersWebApi.Migrations
{
    public partial class startPoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "blog");

            migrationBuilder.EnsureSchema(
                name: "course");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.EnsureSchema(
                name: "exam");

            migrationBuilder.CreateTable(
                name: "Blog",
                schema: "blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                schema: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseThumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRequire = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionType",
                schema: "exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                schema: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Course_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "course",
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                schema: "exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_QuestionType_QuestionTypeId",
                        column: x => x.QuestionTypeId,
                        principalSchema: "exam",
                        principalTable: "QuestionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseRank",
                schema: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseRank", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseRank_Course_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "course",
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseRank_Rank_RankId",
                        column: x => x.RankId,
                        principalSchema: "user",
                        principalTable: "Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthYear = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    RankId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Gender_GenderId",
                        column: x => x.GenderId,
                        principalSchema: "user",
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Rank_RankId",
                        column: x => x.RankId,
                        principalSchema: "user",
                        principalTable: "Rank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOption",
                schema: "exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTrue = table.Column<bool>(type: "bit", nullable: false),
                    OrderNumber = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionOption_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "exam",
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                schema: "course",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Course_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "course",
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Review_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserStep",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StepId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStep_Step_StepId",
                        column: x => x.StepId,
                        principalSchema: "course",
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserStep_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswer",
                schema: "exam",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionOptionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_QuestionOption_QuestionOptionId",
                        column: x => x.QuestionOptionId,
                        principalSchema: "exam",
                        principalTable: "QuestionOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionAnswer_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "course",
                table: "Course",
                columns: new[] { "Id", "CourseDescription", "CourseName", "CourseThumbnail", "IsRequire" },
                values: new object[,]
                {
                    { 1, "This course is a react course", "React", "React.path", true },
                    { 2, "This course is a flutter course", "Flutter", "Flutter.path", false },
                    { 3, "This course is a bootstrap 5 course", "Bootstrap", "Bootstrap.path", false }
                });

            migrationBuilder.InsertData(
                schema: "user",
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" },
                    { 3, "Other" }
                });

            migrationBuilder.InsertData(
                schema: "user",
                table: "Rank",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Beginner", true },
                    { 2, "Intermediate", true },
                    { 3, "Advanced", true }
                });

            migrationBuilder.InsertData(
                schema: "course",
                table: "CourseRank",
                columns: new[] { "Id", "CourseId", "RankId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 2 },
                    { 4, 3, 1 }
                });

            migrationBuilder.InsertData(
                schema: "course",
                table: "Step",
                columns: new[] { "Id", "CourseId", "Title", "VideoPath" },
                values: new object[,]
                {
                    { 1, 1, "React'e giris", "react1.mp4" },
                    { 2, 1, "React'e JX formati", "react2.mp4" },
                    { 3, 1, "React'e component", "react3.mp4" },
                    { 4, 2, "Flutter'a giris", "flutter.mp4" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseRank_CourseId",
                schema: "course",
                table: "CourseRank",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseRank_RankId",
                schema: "course",
                table: "CourseRank",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuestionTypeId",
                schema: "exam",
                table: "Question",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_QuestionOptionId",
                schema: "exam",
                table: "QuestionAnswer",
                column: "QuestionOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswer_UserId",
                schema: "exam",
                table: "QuestionAnswer",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOption_QuestionId",
                schema: "exam",
                table: "QuestionOption",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_CourseId",
                schema: "course",
                table: "Review",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_UserId",
                schema: "course",
                table: "Review",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_CourseId",
                schema: "course",
                table: "Step",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_User_GenderId",
                schema: "user",
                table: "User",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RankId",
                schema: "user",
                table: "User",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStep_StepId",
                schema: "user",
                table: "UserStep",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStep_UserId",
                schema: "user",
                table: "UserStep",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blog",
                schema: "blog");

            migrationBuilder.DropTable(
                name: "CourseRank",
                schema: "course");

            migrationBuilder.DropTable(
                name: "QuestionAnswer",
                schema: "exam");

            migrationBuilder.DropTable(
                name: "Review",
                schema: "course");

            migrationBuilder.DropTable(
                name: "UserStep",
                schema: "user");

            migrationBuilder.DropTable(
                name: "QuestionOption",
                schema: "exam");

            migrationBuilder.DropTable(
                name: "Step",
                schema: "course");

            migrationBuilder.DropTable(
                name: "User",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Question",
                schema: "exam");

            migrationBuilder.DropTable(
                name: "Course",
                schema: "course");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "user");

            migrationBuilder.DropTable(
                name: "Rank",
                schema: "user");

            migrationBuilder.DropTable(
                name: "QuestionType",
                schema: "exam");
        }
    }
}
