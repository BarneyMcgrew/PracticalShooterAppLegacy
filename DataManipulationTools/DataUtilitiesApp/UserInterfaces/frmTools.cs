using DataManipulationLibrary.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataUtilitiesApp.UserInterfaces
{
    public partial class frmTools : Form
    {
        private readonly RulebookService _rulebookService = new RulebookService();
        private readonly TutorialService _learnArticlesService = new TutorialService();
        public frmTools()
        {
            InitializeComponent();
        }

        private void btnConvertRulebook_Click(object sender, EventArgs e)
        {
            // rulebookservice initiates doing the conversion process
            _rulebookService.SaveXMLFile();
        }

        private void btnConvertLearnArticles_Click(object sender, EventArgs e)
        {
            _learnArticlesService.ConvertArticlesToXML();
        }
    }
}
