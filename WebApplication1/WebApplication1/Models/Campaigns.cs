using System;
using System.Web;

namespace WebApplication1.Models
{
    public class Campaigns
    {
        public Object id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string postedBy { get; set; }
        public string status { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        public Object fileId { get; set; }
    }
}