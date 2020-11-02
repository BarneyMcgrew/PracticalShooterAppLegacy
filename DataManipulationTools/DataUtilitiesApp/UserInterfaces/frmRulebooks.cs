using DataManipulationLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace DataUtilitiesApp.UserInterfaces
{
    public partial class frmRulebooks : Form
    {
        private RulebookService _rulebookService;

        public frmRulebooks()
        {
            InitializeComponent();

            _rulebookService = new RulebookService();

            cmbbxDisciplines.ValueMember = "Key";
            cmbbxDisciplines.DisplayMember = "Value";
            cmbbxDisciplines.DataSource = _rulebookService.GetDisciplines().Select(o => new KeyValuePair<Guid, string>(o.Key, o.Value)).ToList();                  
        }

        private void btnUpdateRulebookFile_Click(object sender, System.EventArgs e)
        {
            _rulebookService.SaveXMLFile();
        }

        private void cmbbxDisciplines_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var selectedIndex = (KeyValuePair<Guid, string>)cmbbxDisciplines.SelectedItem;

            lstbxSections.ValueMember = "Key";
            lstbxSections.DisplayMember = "Value";
            lstbxSections.DataSource = _rulebookService.GetSections(selectedIndex.Key).Select(o => new KeyValuePair<Guid, string>(o.Key, o.Value)).ToList();

            lstbxGlossary.ValueMember = "Key";
            lstbxGlossary.DisplayMember = "Value";
            lstbxGlossary.DataSource = _rulebookService.GetGlossaries(selectedIndex.Key).Select(o => new KeyValuePair<Guid, string>(o.Key, o.Value)).ToList();
        }
    }
}
