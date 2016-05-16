using InFb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InFb.DAL
{
    public class DbInit : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var galleries = new List<Gallery>
            {
                new Gallery { GalleryID = 1, Name = "test" },
                new Gallery { GalleryID = 2, Name = "test2" }
            };
            galleries.ForEach(s => context.Galleries.Add(s));
            context.SaveChanges();

            var links = new List<Link>
            {
            new Link{LinkID=1,GalleryID=1,Adress="https://www.instagram.com/explore/tags/koenigsegg/"},
            new Link{LinkID=2,GalleryID=2,Adress="https://www.instagram.com/explore/tags/test/"},
            new Link{LinkID=3,GalleryID=1,Adress="https://www.instagram.com/explore/tags/mountains/"}
            };
            links.ForEach(s => context.Links.Add(s));
            context.SaveChanges();
        }
    }
}