using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineArticleCreator.Models;
using CGT.AspNetMvc;
using System.Security.Cryptography;
using System.Text;

namespace OnlineArticleCreator.Data
{
    public static class DatabaseSeeder
    {
        static readonly SHA512 passHasher = new SHA512Managed();
        static readonly Encoding passEncoding = Encoding.Unicode;

        static Account[] userAccounts = new Account[]
        {
            new Account
            {
                FirstName = "Gabriel",
                LastName = "Cabrera",
                UserName = "gbCabrera",
                Email = "gbCabrera@live.com",
                Password = "password".Hashed(passHasher, passEncoding),
                JoinDate = DateTime.Parse("1/1/2000")
                
            },

            new Account
            {
                FirstName = "John",
                LastName = "Doe",
                UserName = "jDoe",
                Email = "jDoe@gmail.com",
                Password = "passwyrd".Hashed(passHasher, passEncoding),
                JoinDate = DateTime.Parse("2/4/2000")
            },

            new Account
            {
                FirstName = "Jane",
                LastName = "Siegbert",
                UserName = "jSiegbert",
                Email = "jSieg@gmail.com",
                Password = "passverd".Hashed(passHasher, passEncoding),
                JoinDate = DateTime.Parse("3/8/2017")
            }

        };

        public static void SeedDatabaseWith(ApplicationDbContext context)
        {
            context.AddRange(userAccounts);
            context.SaveChanges();

            SetupProfilesAndGalleries(context);
        }

        static void SetupProfilesAndGalleries(ApplicationDbContext context)
        {
            foreach (var account in userAccounts)
            {
                var newProfile = new Profile();
                newProfile.AccountID = account.ID;
                
                var newGallery = new Gallery();
                newGallery.AccountID = account.ID;

                context.Add(newProfile);
                context.Add(newGallery);
                context.SaveChanges();

            }
        }

        
    }
}
