namespace DataUtilitiesApp.UserInterfaces
{
    partial class frmTools
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
            this.btnConvertRulebook = new System.Windows.Forms.Button();
            this.btnConvertLearnArticles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConvertRulebook
            // 
            this.btnConvertRulebook.Location = new System.Drawing.Point(12, 12);
            this.btnConvertRulebook.Name = "btnConvertRulebook";
            this.btnConvertRulebook.Size = new System.Drawing.Size(127, 23);
            this.btnConvertRulebook.TabIndex = 0;
            this.btnConvertRulebook.Text = "Convert Rulebook";
            this.btnConvertRulebook.UseVisualStyleBackColor = true;
            this.btnConvertRulebook.Click += new System.EventHandler(this.btnConvertRulebook_Click);
            // 
            // btnConvertLearnArticles
            // 
            this.btnConvertLearnArticles.Location = new System.Drawing.Point(12, 41);
            this.btnConvertLearnArticles.Name = "btnConvertLearnArticles";
            this.btnConvertLearnArticles.Size = new System.Drawing.Size(127, 23);
            this.btnConvertLearnArticles.TabIndex = 1;
            this.btnConvertLearnArticles.Text = "Convert Learn Articles";
            this.btnConvertLearnArticles.UseVisualStyleBackColor = true;
            this.btnConvertLearnArticles.Click += new System.EventHandler(this.btnConvertLearnArticles_Click);
            // 
            // frmTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnConvertLearnArticles);
            this.Controls.Add(this.btnConvertRulebook);
            this.Name = "frmTools";
            this.Text = "frmTools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConvertRulebook;
        private System.Windows.Forms.Button btnConvertLearnArticles;
    }
}