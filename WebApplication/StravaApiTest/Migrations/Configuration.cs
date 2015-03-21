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

            context.Users.AddOrUpdate(p => p.Id,
                new UserEntity() { Id = 7960195, AccessToken = "6ecc269834c1d2db8886e98da9f6df5214fd2136", FirstName = "Steve", LastName = "Land" },
                new UserEntity() { Id = 3209422, AccessToken = "0f3b3bc578ebcef104ecf8e7c4eb4fbe1fda406f", FirstName = "Duncan", LastName = "Land" },
                new UserEntity() { Id = 3646290, AccessToken = "4d245cfaba1abf8ea9cba02746f7495cf2e61002", FirstName = "Daniel", LastName = "Cunnama" },
                new UserEntity() { Id = 6601020, AccessToken = "9a90802d8392573b941579eca0ed52e7ea314b17", FirstName = "Devon", LastName = "Coetzee" },
                new UserEntity() { Id = 552173, AccessToken = "4051442f80f86866fb089224c77204c38e09a6fd", FirstName = "Ross", LastName = "Winckworth" },
                new UserEntity() { Id = 7657479, AccessToken = "1bb229e1f31bbce301b5aa9ec7a4b670bc76ead1", FirstName = "Ashley", LastName = "Driver" }
                );

        }
    }
}
