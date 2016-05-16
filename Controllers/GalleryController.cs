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

        public string ExtractInstTag(string link)
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

        public string ExtractFbTag(string link)
        {
            string result = "";
            int pos = link.IndexOf("?set=a.");
            for (int i = pos + 7; i < pos + 23; i++)//getting tag
            {
                result += link[i];
            }
            foreach (char c in result)//veryfing if tag is proper
            {
                if (!Char.IsDigit(c))
                {
                    return null;
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
                else if (link.Contains("www.facebook.com/media/set"))
                {
                    linksChecked.Add(link);
                }
            }

            List<Links> result = new List<Links>();
            InstGetter inst = new InstGetter();
            FbGetter fb = new FbGetter();
            foreach (string a in linksChecked)
            {
                string tag;
                Links tmp = new Links();
                if (a.Contains("instagram.com/explore/tags/"))
                {
                    tag = ExtractInstTag(a);
                    tmp.Images = inst.GetLinks(tag);
                }
                else if (a.Contains("www.facebook.com/media/set"))
                {
                    tag = ExtractFbTag(a);
                    tmp.Images = fb.GetLinks(tag);
                }
                tmp.Gallery = a;
                if (tmp.Images!=null)
                    result.Add(tmp);
            }

            ViewBag.Message = result;
            ViewBag.Gallery = galleryname;


            //ViewBag.Message = HttpUtility.HtmlEncode(galleryname);

            return View();
            
        }
    }
}