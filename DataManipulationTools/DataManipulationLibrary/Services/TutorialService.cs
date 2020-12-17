using PracticalShooterLibrary.ObjectClasses.Learn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataManipulationLibrary.Services
{
    public class TutorialService
    {
        private readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory.Replace("IPSCCompanionApp\\DataManipulationTools\\DataUtilitiesApp\\bin\\Debug\\", "Resources\\Articles\\Learn\\");

        public TutorialService()
        { }

        public void ConvertArticlesToXML()
        {
            var articleList = GetTutorialsFromDirectory();
            var tutorialList = new List<Tutorial>();

            foreach (var article in articleList)
            {
                var tutorial = new Tutorial();
                // populate tutorial
            }
        }

        private List<FileInfo> GetTutorialsFromDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo(_filePath);
            return directory.GetFiles("*.txt").ToList();
        }
    }
}
