using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;

namespace EcoDrive
{
    public partial class Board : Form
    {
        private Container components = null;

        // Initializing the required objects.
        private PlayerData playerData;
        private LevelSet levelSet;
        private Level level;

        // Objects for drawing graphics on screen.
        private PictureBox screen;
        private Image img;

        // Form controls to display information on the screen.
        private Label lblMvs;
        private Label lblMoves;
        private GroupBox grpMoves;
        private Label lblPlayerName;
        private Label lblLevelNumber;

        public Board()
        {
            InitializeComponent();
            screen = new PictureBox();
            Controls.AddRange(new Control[] { screen });

            InitializeGame();
        }

        // Setting the data for levelSet, playerData etc.
        private void InitializeGame()
        {
            levelSet = new LevelSet();

            // Display the form to select a player.
            Players players = new Players();
            players.ShowDialog();
            playerData = new PlayerData(players.PlayerName);

            // This is the code for continuing a game that already exists.
            if (players.ContinueExistingGame)
            {
                playerData.LoadLastGame();
                levelSet.SetLevelSet(playerData.LastPlayedSet);

                // Setting the level to be played next.
                levelSet.CurrentLevel = playerData.LastCompletedLevel + 1;
                if (levelSet.CurrentLevel > levelSet.NumberOfLevelsInSet)
                {
                    levelSet.CurrentLevel = levelSet.NumberOfLevelsInSet;
                }

                levelSet.LastCompletedLevel = playerData.LastCompletedLevel;
            }
            else
            {
                // Choosing a level set to play.
                Levels Levels = new Levels();
                Levels.ShowDialog();
                levelSet.SetLevelSet(Levels.FileNameLevelSet);
                levelSet.CurrentLevel = 1;

                // Creating a new player
                if (players.NewPlayer)
                {
                    playerData.CreatePlayer(levelSet);
                }
                // Loading an existing player
                else
                {
                    playerData.LoadPlayer(levelSet);
                    playerData.SaveLevelSet(levelSet);
                }
            }

            lblPlayerName.Text = playerData.Name;

            // Loading the levels in the LevelSet object and setting the current level.
            levelSet.SetLevelsInLevelSet(levelSet.FileName);
            level = (Level)levelSet[levelSet.CurrentLevel - 1];

            // Drawing the level on the screen.
            DrawLevel();
        }

        /*
         * Setting the size of the screen before drawing the level
         * including the labels etc. to be displayed.
        */ 
        private void DrawLevel()
        {
            int levelWidth = (level.Width + 2) * Level.ITEM_SIZE;
            int levelHeight = (level.Height + 2) * Level.ITEM_SIZE;

            this.ClientSize = new Size(levelWidth + 150, levelHeight);
            screen.Size = new System.Drawing.Size(levelWidth, levelHeight);

            img = level.DrawLevel();
            screen.Image = img;

            lblPlayerName.Location = new Point(levelWidth, 25);
            lblLevelNumber.Location = new Point(levelWidth, 65);

            grpMoves.Location = new Point(levelWidth + 15, 90);
            lblMvs.Location = new Point(15, 20);
            lblMoves.Location = new Point(70, 20);

            lblMoves.Text = "0";
            lblLevelNumber.Text = "Level: " + level.LevelNumber;
        }

        // Drawing changes made when the player makes a move.
        private void DrawChanges()
        {
            img = level.DrawChanges();
            screen.Image = img;

            // Updating the labels showing the number of moves.
            lblMoves.Text = level.Moves.ToString();
        }


        /*
         * This section of the code is for when the user presses down a
         * key on the keyboard.
        */ 
        private void AKeyDown(object sender, KeyEventArgs e)
        {
            string result = e.KeyData.ToString();

            switch (result)
            {
                case "Up":
                    MovePlayer(MoveDirection.Up);
                    break;
                case "Down":
                    MovePlayer(MoveDirection.Down);
                    break;
                case "Right":
                    MovePlayer(MoveDirection.Right);
                    break;
                case "Left":
                    MovePlayer(MoveDirection.Left);
                    break;
            }
        }

        // Moving the player depending on the direction pressed
        private void MovePlayer(MoveDirection direction)
        {
            if (direction == MoveDirection.Up)
            {
                level.MoveCar(MoveDirection.Up);
            }
            else if (direction == MoveDirection.Down)
            {
                level.MoveCar(MoveDirection.Down);
            }
            else if (direction == MoveDirection.Right)
            {
                level.MoveCar(MoveDirection.Right);
            }
            else if (direction == MoveDirection.Left)
            {
                level.MoveCar(MoveDirection.Left);
            }

            // Drawing the changes made to the level.
            DrawChanges();

            /*
             * Checking if the level is completed and displaying
             * a message. The number of moves made on the level are
             * saved in the XML file.
            */ 
            if (level.IsCompleted())
            {
                levelSet.LastCompletedLevel = levelSet.CurrentLevel;
                playerData.SaveLevel(level);

                int moves = Convert.ToInt32(lblMoves.Text);
                double co2 = 0.22 * moves;

                if (levelSet.CurrentLevel < levelSet.NumberOfLevelsInSet)
                {
                    MessageBox.Show("You used " + moves + " moves, emitting " + co2 + "kg of Co2. \n\nNow try the next level!","Well done!");
                    levelSet.CurrentLevel++;
                    level = (Level)levelSet[levelSet.CurrentLevel - 1];
                    DrawLevel();
                }
                else
                {
                    MessageBox.Show("You used " + moves + " moves, emitting " + co2 + "kg of Co2. \n\nCongratulations, you completed the final level!","Well done!");
                    this.Close();
                }
            }
        }

        [STAThread]
        static void Main()
        {
            Application.Run(new Board());
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ResourceManager resources = new ResourceManager(typeof(Board));
            this.lblMvs = new Label();
            this.lblMoves = new Label();
            this.grpMoves = new GroupBox();
            this.lblPlayerName = new Label();
            this.lblLevelNumber = new Label();
            this.grpMoves.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMvs
            // 
            this.lblMvs.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblMvs.ForeColor = Color.White;
            this.lblMvs.Location = new Point(16, 24);
            this.lblMvs.Name = "lblMvs";
            this.lblMvs.Size = new Size(52, 16);
            this.lblMvs.TabIndex = 0;
            this.lblMvs.Text = "Moves:";
            // 
            // lblMoves
            // 
            this.lblMoves.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblMoves.ForeColor = Color.Orange;
            this.lblMoves.Location = new Point(72, 24);
            this.lblMoves.Name = "lblMoves";
            this.lblMoves.Size = new Size(44, 16);
            this.lblMoves.TabIndex = 2;
            // 
            // grpMoves
            // 
            this.grpMoves.Controls.Add(this.lblMvs);
            this.grpMoves.Controls.Add(this.lblMoves);
            this.grpMoves.Location = new Point(40, 56);
            this.grpMoves.Name = "grpMoves";
            this.grpMoves.Size = new Size(120, 64);
            this.grpMoves.TabIndex = 4;
            this.grpMoves.TabStop = false;
            // 
            // lblPlayerName
            // 
            this.lblPlayerName.Font = new Font("Tahoma", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblPlayerName.ForeColor = Color.White;
            this.lblPlayerName.Location = new Point(80, 16);
            this.lblPlayerName.Name = "lblPlayerName";
            this.lblPlayerName.Size = new Size(150, 24);
            this.lblPlayerName.TabIndex = 4;
            this.lblPlayerName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblLevelNumber
            // 
            this.lblLevelNumber.Font = new Font("Tahoma", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.lblLevelNumber.ForeColor = Color.White;
            this.lblLevelNumber.Location = new Point(168, 48);
            this.lblLevelNumber.Name = "lblLevelNumber";
            this.lblLevelNumber.Size = new Size(150, 16);
            this.lblLevelNumber.TabIndex = 4;
            this.lblLevelNumber.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Board
            // 
            this.AutoScaleBaseSize = new Size(5, 13);
            this.BackColor = Color.FromArgb(84, 48, 12);
            this.ClientSize = new Size(446, 200);
            this.Controls.Add(this.grpMoves);
            this.Controls.Add(this.lblPlayerName);
            this.Controls.Add(this.lblLevelNumber);
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Board";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Eco Drive";
            this.KeyDown += new KeyEventHandler(this.AKeyDown);
            this.grpMoves.ResumeLayout(false);
            this.ResumeLayout(false);

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


