using System;
using System.Collections.Generic;
using System.Text;

namespace PracticalShooter.ViewModels
{

    public class HomeViewModel : BaseViewModel
    {
        private string _infoText;
        private string _updateText;
        private string _testText;

        public HomeViewModel() 
        {
            InfoText =
                "This app is still in early stages of development and will likely change as feedback is received and new features are developed. " +
                "Most core features will be made available for free, however additional features will be accessible via in-app purchases." +
                Environment.NewLine + Environment.NewLine +
                "As you can see, this page is being used as a beta update page, however is intended to be used as a hub for new features, tutorials " +
                "and featured advertising for the practical shooting user. We are also looking for feedback to what the homepage could also be used for.";
                
            UpdateText = 
                "The app currently contains the rules for IPSC and contains some of the core elements of the core section. We plan to improve the theming and " +
                "the settings section will include options catering to the user's requirements. This update is focused primarily on the basic functions of the rules section.";

            TestText =
                "To help with the smooth release of the app, we would appreciate testing of many of the features that are currently available in the app. To " +
                "assist with the testing, please use the following features and provide any feedback regarding function, UI and performance:" +
                Environment.NewLine + Environment.NewLine +
                "⁍ The rules text themselves need to be verified as complete. The rules are word for word, however their could be missing text." + Environment.NewLine + Environment.NewLine +
                "⁍ The rules search function between chapters and sections." + Environment.NewLine + Environment.NewLine +
                "⁍ Changing of the rulebook discipline within chapters and appendices. With and without search text.";
        }

        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }

        public string UpdateText
        {
            get => _updateText;
            set => SetProperty(ref _updateText, value);
        }

        public string TestText
        {
            get => _testText;
            set => SetProperty(ref _testText, value);
        }
    }
}
