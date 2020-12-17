using IPSCCompanionLibrary.ObjectClasses.Utilities;
using IPSCCompanionLibrary.StaticResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IPSCCompanionLibrary.Services
{
    public class WebsitesReader
    {
        public WebsitesReader()
        { }

        public WebsiteCollection ReadWebsitesResource()
        {
            var file = UtilityHelper.ReadStreamFromFile("Websites");
            StreamReader reader = new StreamReader(file);
            string text = reader.ReadToEnd();
            var websiteCollection = JsonConvert.DeserializeObject<WebsiteCollection>(text);

            return websiteCollection;
        }
    }
}
