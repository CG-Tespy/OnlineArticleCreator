using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineArticleCreator.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            bool alreadySeeded = context.Accounts.Any();
            if (alreadySeeded)
                return;

            DatabaseSeeder.SeedDatabaseWith(context);
        }
    }
}
