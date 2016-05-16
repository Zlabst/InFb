using InFb.DAL;
using InFb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InFb.Controllers
{
    public class AddGalleryController : Controller
    {
        // GET: AddGallery
        public ActionResult Index(string galleryname)
        {
            using (var db = new DataContext())
            {
                var gallery = from ga in db.Galleries where ga.Name == galleryname select ga;
                if (gallery == null)
                {
                    return View();
                }

                var max = db.Galleries.OrderByDescending(u => u.GalleryID).FirstOrDefault();
                int maxx = max.GalleryID;
                Gallery g = new Gallery { GalleryID = maxx + 1, Name = galleryname };
                db.Galleries.Add(g);
                db.SaveChanges();

            }
            return View();
        }
    }
}