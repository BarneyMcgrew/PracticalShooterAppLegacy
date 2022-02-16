using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooterApp.Models
{
    public class RSSFeedModel
    {        
        public class RSSFeedItem
        {
            public String Title { get; set; }
            public String Description { get; set; }
            public String PostLink { get; set; }
            public String LinkReference { get; set; }
            public List<String> Categories { get; set; }
            public DateTime PublishDate { get; set; }
        }

        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
        public List<RSSFeedItem> Items { get; set; }
    }
}
