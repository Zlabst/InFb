using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InFb.Models
{
    public class Link
    {
        public int LinkID { get; set; }
        public int GalleryID { get; set; }
        public string Adress { get; set; }
        public virtual Gallery Gallery { get; set; }
    }
}