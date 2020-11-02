using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace IPSCCompanionLibrary.StaticResources
{
    public class GlobalSettings
    {
        public static GlobalSettings Current = new GlobalSettings();
        public static string SettingsFolderPath;
        private static readonly string SettingsFileName = "settings.xml";

        private static string SettingsFilePath
        { 
            get { return Path.Combine(SettingsFolderPath, SettingsFileName); } 
        }

        public GlobalSettings() { }

        public GlobalSettings(bool createDefaults)
        {
            if (createDefaults)
            {
                EnableGlossaryTooltips = true;
                EnableRuleReferences = true;
                ShowGoBackButton = true;
                InclusiveSearch = true;
                ShowDisciplineSelectorOnMainScreen = true;
                SelectedDiscipline = "Handgun";
                AppVersion = 0;
                AppLaunchCount = 0;
                EnableDisciplineColorCoding = false;
            }
        }

        private void CreateDefaults()
        {
            Current = new GlobalSettings(true);

            UpdateSettings();
        }

        public void LoadSettings(string folderPath)
        {
            SettingsFolderPath = folderPath;

            Current = new GlobalSettings();

            if (!File.Exists(SettingsFilePath))
            {
                CreateDefaults();
            }
            else
            {
                var file = File.OpenRead(SettingsFilePath);

                var serializer = new XmlSerializer(GetType());

                var settings = (GlobalSettings)serializer.Deserialize(file);

                file.Dispose();

                Current = settings;
            }
        }

        public void UpdateSettings()
        {
            var settings = Current;
            var xmlFile = new MemoryStream();

            var serializer = new XmlSerializer(settings.GetType());

            serializer.Serialize(xmlFile, settings);

            File.WriteAllText(SettingsFilePath, Encoding.UTF8.GetString(xmlFile.ToArray()));
        }

        public bool EnableGlossaryTooltips;
        public bool EnableRuleReferences;
        public bool ShowGoBackButton;
        public bool InclusiveSearch;
        public bool ShowDisciplineSelectorOnMainScreen;
        public string SelectedDiscipline;
        public double AppVersion;
        public int AppLaunchCount;
        public bool EnableDisciplineColorCoding;


    }
}
