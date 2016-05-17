using InFb.DAL;
using InFb.Models;
using InFb.Source;
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
            List<Link> links = new List<Link>();
            using (var db = new DataContext())
            {
                Gallery gal = db.Galleries.First(c => c.Name == galleryname);
                foreach (var a in gal.Links)
                {
                    links.Add(a);
                }
            }
            List<string> linksChecked = new List<string>();
            foreach (var link in links)
            {
                if (link.Adress.Contains("instagram.com/explore/tags/"))
                    linksChecked.Add(link.Adress);
                else if (link.Adress.Contains("www.facebook.com/media/set"))
                {
                    linksChecked.Add(link.Adress);
                }
            }

            List<GalleryLinks> result = new List<GalleryLinks>();
            InstGetter inst = new InstGetter();
            FbGetter fb = new FbGetter();
            foreach (string a in linksChecked)
            {
                string tag;
                GalleryLinks tmp = new GalleryLinks();
                if (a.Contains("instagram.com/explore/tags/"))
                {
                    tmp.Images = inst.GetLinks(a);
                }
                else if (a.Contains("www.facebook.com/media/set"))
                {
                    tmp.Images = fb.GetLinks(a);
                }
                tmp.Gallery = a;
                if (tmp.Images!=null)
                    result.Add(tmp);
            }

            ViewBag.Message = result;
            ViewBag.Gallery = galleryname;

            return View();
            
        }
    }
}