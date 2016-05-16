using InFb.DAL;
using InFb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InFb.Controllers
{
    public class AddingController : Controller
    {
        // GET: Adding
        public ActionResult Index(string galleryname, string link)
        {

            using (var db = new DataContext())
            {
                var gallery = from ga in db.Galleries where ga.Name == galleryname select ga;
                var gall = gallery.FirstOrDefault<Gallery>();

                var max = db.Links.OrderByDescending(u => u.LinkID).FirstOrDefault();

                Link en = new Link { GalleryID = gall.GalleryID, Adress = link, LinkID = max.LinkID + 1 };
                db.Links.Add(en);
                db.SaveChanges();

            }



            return View();
        }
    }
}