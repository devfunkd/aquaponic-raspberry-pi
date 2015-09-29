using System;
using AquaCultureMonitor.Core.Domain;
using AquaCultureMonitor.Core.Repositories;

namespace AquaCultureMonitor.Core.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DatabaseContext context)
        {

        }
    }
}
