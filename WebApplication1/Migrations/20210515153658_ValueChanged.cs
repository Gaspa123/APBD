using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class ValueChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "C#",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "Java", "eeeeeeeeeeeeeeefffffffffffffffffffggggggggggggggggghhhhhhhhhhhhh" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Login",
                keyValue: "Piotr",
                columns: new[] { "Password", "Salt" },
                values: new object[] { "Paweł", "ididiidididididididiididididid" });
        }
    }
}
