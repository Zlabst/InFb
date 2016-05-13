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
            new Entry{EntryID=1,Name="aaa",Link="bbb"},
            new Entry{EntryID=2,Name="aaa1",Link="bbb1"}
            };

            entries.ForEach(s => context.Entries.Add(s));
            context.SaveChanges();
        }
    }
}