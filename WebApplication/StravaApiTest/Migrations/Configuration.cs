namespace StravaApiTest.Migrations
{
    using StravaApiTest.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StravaApiTest.DAL.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "StravaApiTest.DAL.AppDbContext";
        }

        protected override void Seed(StravaApiTest.DAL.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //context.Users.AddOrUpdate(p => p.Id,
            //    new UserEntity() { Id = 7960195, AccessToken = "6ecc269834c1d2db8886e98da9f6df5214fd2136", FirstName = "Seed", LastName = "Data" },
            //    new UserEntity() { Id = 3209422, AccessToken = "0f3b3bc578ebcef104ecf8e7c4eb4fbe1fda406f", FirstName = "Seed", LastName = "Data" }
            //    );

        }
    }
}
