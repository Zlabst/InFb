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
            pos += 7;
            while (link[pos] != '.')
            {
                result += link[pos++];
            }
            /*for (int i = pos + 7; i < pos + 23; i++)//getting tag
            {
                result += link[i];
            }*/
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