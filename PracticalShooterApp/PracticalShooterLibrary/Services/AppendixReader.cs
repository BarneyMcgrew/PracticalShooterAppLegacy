using PracticalShooterLibrary.ObjectClasses.Rules;
using PracticalShooterLibrary.StaticResources;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PracticalShooterLibrary.Services
{
    public class AppendixReader
    {
        public AppendixReader()
        { }

        public AppendixLibrary ReadAppendixResource()
        {
            var file = UtilityHelper.ReadStreamFromFile("Appendices");
            StreamReader reader = new StreamReader(file);
            string text = reader.ReadToEnd();
            var appendixLibrary = JsonConvert.DeserializeObject<AppendixLibrary>(text);

            return appendixLibrary;
        }
    }
}
