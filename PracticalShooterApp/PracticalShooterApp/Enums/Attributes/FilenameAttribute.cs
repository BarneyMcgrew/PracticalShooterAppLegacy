using System;

namespace PracticalShooterApp.Enums.Attributes
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