using InFb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InFb.DAL
{
    public class EntryContext : DbContext
    {
        public EntryContext() : base("EntryContext")
        {
        }

        public DbSet<Entry> Entries { get; set; }

    }
}