using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class ValueChangedAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "C#",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "+k9RpdH0GbfC2DC3kfKl/SS2JRhMlbqZkvRXKOWzY0I=", "qiWKIJPJ0JAzagrgJ6WSag==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "Piotr",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "6sYiYlWogqjuYyLpBJDnIxFXZcFyJZT5+a0wNDPo9pI=", "t/B16TuEHBonmTJythUc9w==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "C#",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "FGnkxb22w9R8uouTrApOt6T+KJZtsacgQhHIrM8fYRA=", "hQaudFiTLIOBvPhwnNsoYw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "Piotr",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "5OAeYAta+Ve80G8VFUTqoE3CvqCXtN4fOSoGf0jWoBs=", "O5hKC0wOfQIpwrlDtVKtWQ==" });
        }
    }
}
