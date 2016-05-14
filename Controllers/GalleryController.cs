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
            if (galleryname == "1")
            {
                ViewBag.Message = "<a href=" + "http://upload.wikimedia.org/wikipedia/commons/6/6b/Big_Sur_June_2008.jpg" + ">";
            }
            else
            {
                ViewBag.Message = "http://upload.wikimedia.org/wikipedia/commons/6/6b/Big_Sur_June_2008.jpg";
            }

            //ViewBag.Message = HttpUtility.HtmlEncode(galleryname);

            return View();
            
        }
    }
}