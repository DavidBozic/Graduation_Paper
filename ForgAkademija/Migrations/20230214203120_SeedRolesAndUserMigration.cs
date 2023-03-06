using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgAkademija.Migrations
{
    public partial class SeedRolesAndUserMigration : Migration
    {
        private string adminRoleId = Guid.NewGuid().ToString();
        private string moderatorRoleId = Guid.NewGuid().ToString();
        private string userRoleId = Guid.NewGuid().ToString();

        private string adminId = Guid.NewGuid().ToString();
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSql(migrationBuilder);
            SeedAdministrator(migrationBuilder);
            SeedAdministratorRole(migrationBuilder);
        }

        private void SeedRolesSql(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{adminRoleId}','Administrator','ADMINISTRATOR',null);");

            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{moderatorRoleId}','Moderator','MODERATOR',null);");

            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{userRoleId}','User','USER',null);");
        }

        private void SeedAdministrator(MigrationBuilder migrationBuilder)
        {
            //Username:administrator|Password:Test-1234 (predefined admin account in the app)
            migrationBuilder.Sql(@$"INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], 
            [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], 
            [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) 
            VALUES 
            (N'{adminId}', N'administrator', N'ADMINISTRATOR', N'administrator@test.com', N'ADMINISTRATOR@TEST.COM', 0, 
            N'AQAAAAEAACcQAAAAELu4gaFVVMHrK60epF2SFGaTAqiCw9JnKJ5xmU9ayCJQ8Hry5CVjWXwo2WPXO+pW8w==', 
            N'DNYISS2YC5VOEAI5GW46B2YJNACFLTAW', N'6f444820-31ba-4024-ae88-8473d4b5a45c', NULL, 0, 0, NULL, 1, 0)");
        }

        private void SeedAdministratorRole(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@$"
            INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
            VALUES
           ('{adminId}', '{adminRoleId}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
