using System;

namespace RulebookConversionLibrary.Enums.Attributes
{
    public class FilenameAttribute : Attribute
    {
        public string FileName { get; }

        public FilenameAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}