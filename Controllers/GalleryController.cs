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

        public string ExtractTag(string link)
        {
            string result = "";
            int pos = link.IndexOf("/tags/");
            for (int i=pos+6; i<link.Length-1; i++)//getting tag
            {
                result += link[i];
            }
            foreach (char c in result)//veryfing if tag is proper
            {
                if (!Char.IsLetter(c))
                {
                    return "error";
                }
            }
            return result;
        }

        public ActionResult Show(string galleryname)
        {
            //Console.WriteLine(galleryname);
            //InstGetter inst = new InstGetter();
            //List < string > result = inst.GetLinks(galleryname);


            List<string> links = new List<string>();
            using (var db = new EntryContext())
            {
                var query = from b in db.Entries where b.Name == galleryname select b;

                //ViewBag.Message = query;
                foreach(var a in query)
                {
                    links.Add(a.Link);
                }
            }
            List<string> linksChecked = new List<string>();
            foreach (string link in links)
            {
                if (link.Contains("instagram.com/explore/tags/"))
                    linksChecked.Add(link);
            }

            List<Links> result = new List<Links>();
            InstGetter inst = new InstGetter();
            foreach (string a in linksChecked)
            {
                string tag = ExtractTag(a);
                Links tmp = new Links();
                tmp.Images = inst.GetLinks(tag);
                tmp.Gallery = a;
                result.Add(tmp);
            }

            ViewBag.Message = result;
            ViewBag.Gallery = galleryname;


            //ViewBag.Message = HttpUtility.HtmlEncode(galleryname);

            return View();
            
        }
    }
}