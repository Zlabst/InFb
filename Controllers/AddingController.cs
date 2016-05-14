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
            
            using (var db = new EntryContext())
            {
                var max = db.Entries.OrderByDescending(u => u.EntryID).FirstOrDefault();

                Entry en = new Entry { Name = galleryname, Link = link, EntryID=max.EntryID + 1 };
                db.Entries.Add(en);
                db.SaveChanges();
                
            }



            return View();
        }
    }
}