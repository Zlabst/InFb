using InFb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InFb.DAL
{
    public class DbInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<EntryContext>
    {
        protected override void Seed(EntryContext context)
        {
            var entries = new List<Models.Entry>
            {
            new Entry{EntryID=1,Name="test",Link="https://www.instagram.com/explore/tags/koenigsegg/"},
            new Entry{EntryID=2,Name="test",Link="https://www.instagram.com/explore/tags/games/"},
            /*new Entry{EntryID=3,Name="test",Link="https://www.instagram.com/explore/tags/store/"},
            new Entry{EntryID=4,Name="aaa",Link="bbb"},
            new Entry{EntryID=5,Name="aaa",Link="bbb"},
            new Entry{EntryID=6,Name="aaa",Link="bbb"},
            new Entry{EntryID=7,Name="aaa1",Link="bbb1"}*/
            };

            entries.ForEach(s => context.Entries.Add(s));
            context.SaveChanges();
        }
    }
}