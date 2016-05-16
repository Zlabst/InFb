using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InFb.Models
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Link> Links { get; set; }
    }
}