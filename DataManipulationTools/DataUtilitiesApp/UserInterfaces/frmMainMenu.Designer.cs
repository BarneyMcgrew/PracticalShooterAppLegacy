namespace DataUtilitiesApp.UserInterfaces
{
    partial class frmMainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnModifyRulebook = new System.Windows.Forms.Button();
            this.btnModifyLearnArticles = new System.Windows.Forms.Button();
            this.btnModifyDrillPractices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnModifyRulebook
            // 
            this.btnModifyRulebook.Location = new System.Drawing.Point(12, 70);
            this.btnModifyRulebook.Name = "btnModifyRulebook";
            this.btnModifyRulebook.Size = new System.Drawing.Size(232, 52);
            this.btnModifyRulebook.TabIndex = 0;
            this.btnModifyRulebook.Text = "Modify Rulebooks";
            this.btnModifyRulebook.UseVisualStyleBackColor = true;
            this.btnModifyRulebook.Click += new System.EventHandler(this.btnModifyRulebook_Click);
            // 
            // btnModifyLearnArticles
            // 
            this.btnModifyLearnArticles.Location = new System.Drawing.Point(12, 12);
            this.btnModifyLearnArticles.Name = "btnModifyLearnArticles";
            this.btnModifyLearnArticles.Size = new System.Drawing.Size(232, 52);
            this.btnModifyLearnArticles.TabIndex = 1;
            this.btnModifyLearnArticles.Text = "Modify Learn Articles";
            this.btnModifyLearnArticles.UseVisualStyleBackColor = true;
            // 
            // btnModifyDrillPractices
            // 
            this.btnModifyDrillPractices.Location = new System.Drawing.Point(12, 128);
            this.btnModifyDrillPractices.Name = "btnModifyDrillPractices";
            this.btnModifyDrillPractices.Size = new System.Drawing.Size(232, 52);
            this.btnModifyDrillPractices.TabIndex = 2;
            this.btnModifyDrillPractices.Text = "Modify Drill Practices";
            this.btnModifyDrillPractices.UseVisualStyleBackColor = true;
            // 
            // frmMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 199);
            this.Controls.Add(this.btnModifyDrillPractices);
            this.Controls.Add(this.btnModifyLearnArticles);
            this.Controls.Add(this.btnModifyRulebook);
            this.Name = "frmMainMenu";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnModifyRulebook;
        private System.Windows.Forms.Button btnModifyLearnArticles;
        private System.Windows.Forms.Button btnModifyDrillPractices;
    }
}

