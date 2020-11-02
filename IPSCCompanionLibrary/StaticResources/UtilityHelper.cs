using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace IPSCCompanionLibrary.StaticResources
{
    public static class UtilityHelper
    {
        public static string[] ReadFromFile(string fileName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(UtilityHelper)).Assembly;
            var resources = assembly.GetManifestResourceNames();
            List<string> text = new List<string>();

            foreach (var resource in resources)
            {
                if (resource.Contains(fileName))
                {
                    Stream stream = assembly.GetManifestResourceStream(resource);

                    using (var reader = new StreamReader(stream))
                    {
                        var line = string.Empty;

                        do
                        {
                            line = reader.ReadLine();
                            text.Add(line);

                        } while (line != null);

                        text.RemoveAt(text.Count - 1);
                    }

                    break;
                }
            }

            return text.ToArray();
        }

        public static Stream ReadStreamFromFile(string fileName)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(UtilityHelper)).Assembly;
            var resources = assembly.GetManifestResourceNames();
            List<string> text = new List<string>();

            foreach (var resource in resources)
            {
                if (resource.Contains(fileName))
                {
                    Stream stream = assembly.GetManifestResourceStream(resource);

                    return stream;

                    break;
                }
            }

            return null;
        }
    }
}
