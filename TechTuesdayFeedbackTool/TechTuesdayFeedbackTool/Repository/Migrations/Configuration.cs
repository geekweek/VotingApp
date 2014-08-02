namespace Repository.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TechTuesdayFeedbackTool.Domain;

    internal sealed class Configuration : DbMigrationsConfiguration<TechTuesdayFeedbackTool.Repository.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TechTuesdayFeedbackTool.Repository.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.UserRolesMasterContext.AddOrUpdate(new UserRolesMaster[] {
                new UserRolesMaster { ID = 1, RoleName = "Admin"},
                new UserRolesMaster { ID = 2, RoleName = "User"},
            });
        }
    }
}
