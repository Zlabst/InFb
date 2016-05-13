using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InFb.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(string galleryname)
        {
            //Console.WriteLine(galleryname);
            ViewBag.Message = HttpUtility.HtmlEncode("Witaj " + galleryname);

            return View();
            
        }
    }
}