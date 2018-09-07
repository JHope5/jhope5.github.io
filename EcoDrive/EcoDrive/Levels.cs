using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace EcoDrive
{
    /*
     * This form allows the user to select a level set 
     * from the list provided.
    */
    public partial class Levels : Form
    {
        private Container components = null;
        private ListBox lstLevelSets;
        private Label lblAuthorH;
        private Label lblAuthor;
        private Button btnSelect;
        private Label lblDescriptionH;
        private Label lblDescription;
        private Label lblNumberOfLevels;
        private Label lblNumberOfLevelsH;

        private ArrayList levelSets = new ArrayList();
        private string fileNameLevelSet = string.Empty;
        private string nameLevelSet = string.Empty;

        public Levels()
        {
            InitializeComponent();

            // Loads the information of all level sets
            levelSets = LevelSet.GetAllLevelSetInfos();
            
            // Adds the title of each level set to the listbox
            foreach (LevelSet levelSet in levelSets)
                lstLevelSets.Items.Add(levelSet.Title);
            
            lstLevelSets.SelectedIndex = 0;
		}

        // When the user selects different options from the listbox
        private void lstLevelSets_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int index = lstLevelSets.SelectedIndex;
            LevelSet levelSet = (LevelSet)levelSets[index];

            lblAuthor.Text = levelSet.Author;
            lblDescription.Text = levelSet.Description;
            lblNumberOfLevels.Text = levelSet.NumberOfLevelsInSet.ToString();
        }

        // Sets the name and the file name of the level set.
        private void btnSelect_Click(object sender, System.EventArgs e)
        {
            nameLevelSet = lstLevelSets.SelectedItem.ToString();

            foreach (LevelSet levelSet in levelSets)
            {
                if (levelSet.Title == nameLevelSet)
                {
                    fileNameLevelSet = levelSet.FileName;
                    break;
                }
            }

            this.Close();
        }


        public string FileNameLevelSet
        {
            get 
            { 
                return fileNameLevelSet; 
            }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstLevelSets = new ListBox();
            this.lblAuthorH = new Label();
            this.lblDescriptionH = new Label();
            this.lblAuthor = new Label();
            this.lblDescription = new Label();
            this.btnSelect = new Button();
            this.lblNumberOfLevels = new Label();
            this.lblNumberOfLevelsH = new Label();
            this.SuspendLayout();
            // 
            // lstLevelSets
            // 
            this.lstLevelSets.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lstLevelSets.Location = new Point(24, 24);
            this.lstLevelSets.Name = "lstLevelSets";
            this.lstLevelSets.Size = new Size(176, 199);
            this.lstLevelSets.TabIndex = 0;
            this.lstLevelSets.SelectedIndexChanged += new EventHandler(this.lstLevelSets_SelectedIndexChanged);
            // 
            // lblAuthorH
            // 
            this.lblAuthorH.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblAuthorH.Location = new Point(224, 32);
            this.lblAuthorH.Name = "lblAuthorH";
            this.lblAuthorH.Size = new Size(80, 16);
            this.lblAuthorH.TabIndex = 1;
            this.lblAuthorH.Text = "Author:";
            this.lblAuthorH.ForeColor = Color.White;
            // 
            // lblDescriptionH
            // 
            this.lblDescriptionH.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblDescriptionH.Location = new Point(224, 112);
            this.lblDescriptionH.Name = "lblDescriptionH";
            this.lblDescriptionH.Size = new Size(104, 16);
            this.lblDescriptionH.TabIndex = 3;
            this.lblDescriptionH.Text = "Description:";
            this.lblDescriptionH.ForeColor = Color.White;
            // 
            // lblAuthor
            // 
            this.lblAuthor.Location = new Point(304, 32);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new Size(168, 16);
            this.lblAuthor.TabIndex = 6;
            this.lblAuthor.ForeColor = Color.White;
            // 
            // lblDescription
            // 
            this.lblDescription.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.lblDescription.Location = new Point(224, 136);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(248, 88);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.ForeColor = Color.White;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.btnSelect.Location = new Point(392, 240);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.TabIndex = 12;
            this.btnSelect.Text = "Start game";
            this.btnSelect.Click += new EventHandler(this.btnSelect_Click);
            this.btnSelect.ForeColor = Color.White;
            // 
            // lblNumberOfLevels
            // 
            this.lblNumberOfLevels.Location = new Point(304, 80);
            this.lblNumberOfLevels.Name = "lblNumberOfLevels";
            this.lblNumberOfLevels.Size = new Size(168, 16);
            this.lblNumberOfLevels.TabIndex = 14;
            this.lblNumberOfLevels.ForeColor = Color.White;
            // 
            // lblNumberOfLevelsH
            // 
            this.lblNumberOfLevelsH.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblNumberOfLevelsH.Location = new Point(224, 80);
            this.lblNumberOfLevelsH.Name = "lblNumberOfLevelsH";
            this.lblNumberOfLevelsH.Size = new Size(80, 16);
            this.lblNumberOfLevelsH.TabIndex = 13;
            this.lblNumberOfLevelsH.Text = "Level count:";
            this.lblNumberOfLevelsH.ForeColor = Color.White;
            // 
            // FormLevels
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(498, 277);
            this.ControlBox = false;
            this.Controls.Add(this.lblNumberOfLevels);
            this.Controls.Add(this.lblNumberOfLevelsH);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblDescriptionH);
            this.Controls.Add(this.lblAuthorH);
            this.Controls.Add(this.lstLevelSets);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "Levels";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Levels";
            this.ResumeLayout(false);
            this.BackColor = Color.FromArgb(84, 48, 12);

        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion

    }
}
