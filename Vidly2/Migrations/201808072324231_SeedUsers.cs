namespace Vidly2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"

            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'371be363-93c2-45cf-acd3-b803e0f98303', N'guest@vidly.com', 0, N'AJdlpI0WhkTIe4aNcSX0aNGLArh9k3EOdje8YJDcFrz8bxet7DBj9mUxWqAWSegx7g==', N'ad223dcf-832d-4f51-8fb7-06c09d474108', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'6a3d32a8-9144-4f19-b408-4cdc6634922b', N'admin@vidly.com', 0, N'AHQ9UVxpPcMDUA5EqCpljonZrzxoCjNEhq9RyHgmUdCVKvOd/kTk5cbgEPZg25k7cg==', N'bd575466-ee42-43cd-a36a-3443e5466fd2', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'655000cf-8dfe-44bc-a6ac-5ef162775ba8', N'CanManageMovies')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6a3d32a8-9144-4f19-b408-4cdc6634922b', N'655000cf-8dfe-44bc-a6ac-5ef162775ba8')
            ");
        }
        
        public override void Down()
        {
        }
    }
}
