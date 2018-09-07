using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace EcoDrive
{
    public partial class Players : Form
    {
        private GroupBox grpNewPlayer;
        private GroupBox grpSelectPlayer;
        private Label lblPlayers;
        private ListBox lstPlayers;
        private CheckBox chkContinuePrev;
        private Button btnStartExisting;
        private Label lblPlayerName;
        private TextBox txtPlayerName;
        private Button btnStartNew;
        private Container components = null;

        private string playerName = string.Empty;

        // Indicate if the player wants to continue an existing game or start a new one.
        private bool continueExistingGame = true;

        // Indicate if the user has picked an existing player or wishes to create a new one.
        private bool newPlayer = false;
        
        public Players()
        {
            InitializeComponent();

            ArrayList players = PlayerData.GetPlayers();

            // List the players in listbox if there are any.
            if (players.Count == 0)
                btnStartExisting.Enabled = false;
            else
                lstPlayers.DataSource = players;
        }


        // Starts an existing game and checks if the previous game is to be continued or not.
        private void btnStartExisting_Click(object sender, System.EventArgs e)
        {
            playerName = lstPlayers.SelectedItem.ToString();
            continueExistingGame = chkContinuePrev.Checked ? true : false;

            this.Close();
        }

        // Starts a new game for the new player.
        private void btnStartNew_Click(object sender, System.EventArgs e)
        {
            playerName = txtPlayerName.Text;
            continueExistingGame = false;

            if (txtPlayerName.Text == "")
            {
                MessageBox.Show("Please enter a player name.", "Whoops!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                newPlayer = true;
                this.Close();
            }
        }



        public string PlayerName
        {
            get 
            { 
                return playerName; 
            }
        }

        public bool ContinueExistingGame
        {
            get 
            { 
                return continueExistingGame; 
            }
        }

        public bool NewPlayer
        {
            get 
            { 
                return newPlayer; 
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpNewPlayer = new GroupBox();
            this.txtPlayerName = new TextBox();
            this.lblPlayerName = new Label();
            this.btnStartNew = new Button();
            this.grpSelectPlayer = new GroupBox();
            this.btnStartExisting = new Button();
            this.chkContinuePrev = new CheckBox();
            this.lstPlayers = new ListBox();
            this.lblPlayers = new Label();
            this.grpNewPlayer.SuspendLayout();
            this.grpSelectPlayer.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNewPlayer
            // 
            this.grpNewPlayer.Controls.Add(this.txtPlayerName);
            this.grpNewPlayer.Controls.Add(this.lblPlayerName);
            this.grpNewPlayer.Controls.Add(this.btnStartNew);
            this.grpNewPlayer.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.grpNewPlayer.Location = new Point(213, 49);
            this.grpNewPlayer.Name = "grpNewPlayer";
            this.grpNewPlayer.Size = new Size(156, 184);
            this.grpNewPlayer.TabIndex = 0;
            this.grpNewPlayer.TabStop = false;
            this.grpNewPlayer.Text = "Create a new player";
            this.grpNewPlayer.ForeColor = Color.White;
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new Point(7, 56);
            this.txtPlayerName.MaxLength = 30;
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new Size(119, 20);
            this.txtPlayerName.TabIndex = 1;
            this.txtPlayerName.Text = "";
            this.txtPlayerName.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Location = new Point(7, 56);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new Size(68, 13);
            this.lblPlayerName.TabIndex = 0;
            this.lblPlayerName.Text = "Player name:";
            this.lblPlayerName.ForeColor = Color.White;
            this.lblPlayerName.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // btnStartNew
            // 
            this.btnStartNew.Location = new Point(7, 143);
            this.btnStartNew.Size = new Size(120, 23);
            this.btnStartNew.Name = "btnStartNew";
            this.btnStartNew.TabIndex = 3;
            this.btnStartNew.Text = "Start Game";
            this.btnStartNew.Click += new EventHandler(this.btnStartNew_Click);
            this.btnStartNew.ForeColor = Color.White;
            this.btnStartNew.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // grpSelectPlayer
            // 
            this.grpSelectPlayer.Controls.Add(this.btnStartExisting);
            this.grpSelectPlayer.Controls.Add(this.chkContinuePrev);
            this.grpSelectPlayer.Controls.Add(this.lstPlayers);
            this.grpSelectPlayer.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.grpSelectPlayer.Location = new Point(24, 49);
            this.grpSelectPlayer.Name = "grpSelectPlayer";
            this.grpSelectPlayer.Size = new Size(168, 184);
            this.grpSelectPlayer.TabIndex = 1;
            this.grpSelectPlayer.TabStop = false;
            this.grpSelectPlayer.Text = "Select existing player";
            this.grpSelectPlayer.ForeColor = Color.White;
            // 
            // btnStartExisting
            // 
            this.btnStartExisting.Location = new Point(6, 143);
            this.btnStartExisting.Size = new Size(120, 23);
            this.btnStartExisting.Name = "btnStartExisting";
            this.btnStartExisting.TabIndex = 2;
            this.btnStartExisting.Text = "Start game";
            this.btnStartExisting.Click += new EventHandler(this.btnStartExisting_Click);
            this.btnStartExisting.ForeColor = Color.White;
            this.btnStartExisting.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // chkContinuePrev
            // 
            this.chkContinuePrev.Checked = true;
            this.chkContinuePrev.CheckState = CheckState.Checked;
            this.chkContinuePrev.Location = new Point(6, 120);
            this.chkContinuePrev.Name = "chkContinuePrev";
            this.chkContinuePrev.Size = new Size(157, 17);
            this.chkContinuePrev.TabIndex = 1;
            this.chkContinuePrev.Text = "Continue previous game";
            this.chkContinuePrev.ForeColor = Color.White;
            this.chkContinuePrev.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // lstPlayers
            // 
            this.lstPlayers.Location = new Point(6, 19);
            this.lstPlayers.Name = "lstPlayers";
            this.lstPlayers.Size = new Size(120, 95);
            this.lstPlayers.TabIndex = 0;
            this.lstPlayers.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            // 
            // lblPlayers
            // 
            this.lblPlayers.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblPlayers.Location = new Point(24, 24);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new Size(384, 16);
            this.lblPlayers.TabIndex = 2;
            this.lblPlayers.Text = "Welcome to Eco Drive! Find the shortest route to reach the goal.";
            this.lblPlayers.ForeColor = Color.White;
            // 
            // FormPlayers
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.ClientSize = new Size(424, 283);
            this.Controls.Add(this.lblPlayers);
            this.Controls.Add(this.grpNewPlayer);
            this.Controls.Add(this.grpSelectPlayer);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.Name = "FormPlayers";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Eco Drive";
            this.grpNewPlayer.ResumeLayout(false);
            this.grpSelectPlayer.ResumeLayout(false);
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
