using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace EcoDrive
{
    // The items that can appear on a level.
    public enum ItemType
    {
        Grass,
        Goal,
        Car,
        CarOnGoal,
        Road,
        Building,
        Trash,
        BlankSpace
    }

    // The different directions the player can face/move.
    public enum MoveDirection
    {
        Right,
        Up,
        Down,
        Left
    }

    /*
     * This class keeps the level information and draws the level
     * on the screen. The items in a level are the Grass, Road,
     * and the Car (player). 
    */ 
    class Level
    {
        private string name = string.Empty;     // Name of the level
        private ItemType[,] levelMap;           // Level layout of items
        private int numberOfGoals = 0;          // The car must be placed on the goal
        private int levelNumber = 0;
        private int width = 0;                  // Level width in items
        private int height = 0;                 // Level height in items

        // The name of the level set that the level belongs to.
        private string levelSetName = string.Empty;

        private int moves = 0;              // Number of moves made
        private int pushes = 0;             // Number of pushes made

        private int carXPos;             // X position of the car
        private int carYPos;             // Y position of the car

        // Default direction the car is facing when starting a level.
        private MoveDirection carDirection = MoveDirection.Up;

        // ITEM_SIZE is the size of an item in the level.
        public const int ITEM_SIZE = 30;

        /*
         * The items are updated every time the player moves the car. 
         * A maximum of 3 items can change per push. The changes are
         * kept track of so that the whole level does not need to be
         * redrawn, and instead it's just the 3 changed items.
        */ 
        private Item item1, item2, item3;

        // For drawing the level on the screen.
        private Bitmap img;
        private Graphics g;

        #region Properties

        public string Name
        {
            get 
            { 
                return name; 
            }
        }

        public int LevelNumber
        {
            get 
            { 
                return levelNumber; 
            }
        }

        public int Width
        {
            get 
            { 
                return width; 
            }
        }

        public int Height
        {
            get 
            { 
                return height; 
            }
        }

        public string LevelSetName
        {
            get 
            { 
                return levelSetName; 
            }
        }

        public int Moves
        {
            get 
            { 
                return moves; 
            }
        }

        public int Pushes
        {
            get 
            { 
                return pushes; 
            }
        }

        #endregion
        

        public Level(string aName, ItemType[,] aLevelMap, int aWidth, int aHeight, int aNumberOfGoals, int aLevelNumber, string aLevelSetName)
        {
            name = aName;
            width = aWidth;
            height = aHeight;
            levelMap = aLevelMap;
            numberOfGoals = aNumberOfGoals;
            levelNumber = aLevelNumber;
            levelSetName = aLevelSetName;
        }

        /*
         * This method draws the level on the screen. A border is drawn
         * to make the level look neater. Then the map is loaded and 
         * the images are inserted.
        */ 
        public Image DrawLevel()
        {
            int levelWidth = (width + 2) * Level.ITEM_SIZE;
            int levelHeight = (height + 2) * Level.ITEM_SIZE;

            img = new Bitmap(levelWidth, levelHeight);
            g = Graphics.FromImage(img);

            Font statusText = new Font("Tahoma", 10, FontStyle.Bold);

            g.Clear(Color.FromArgb(27, 33, 61));

            // Drawing the border around the level.
            for (int i = 0; i < width + 2; i++)
            {
                g.DrawImage(ImgBlankSpace, ITEM_SIZE * i, 0, ITEM_SIZE, ITEM_SIZE);
                g.DrawImage(ImgBlankSpace, ITEM_SIZE * i, (height + 1) * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }
            for (int i = 1; i < height + 1; i++)
            {
                g.DrawImage(ImgBlankSpace, 0, ITEM_SIZE * i, ITEM_SIZE, ITEM_SIZE);
            }
            for (int i = 1; i < height + 1; i++)
            {
                g.DrawImage(ImgBlankSpace, (width + 1) * ITEM_SIZE, ITEM_SIZE * i, ITEM_SIZE, ITEM_SIZE);
            }

            // Drawing the level.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Image image = GetLevelImage(levelMap[i, j], carDirection);

                    g.DrawImage(image, ITEM_SIZE + i * ITEM_SIZE, ITEM_SIZE + j * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

                    // Setting the player's position.
                    if (levelMap[i, j] == ItemType.Car || levelMap[i, j] == ItemType.CarOnGoal)
                    {
                        carXPos = i;
                        carYPos = j;
                    }
                }
            }

            return img;
        }

        /*
         * This method draws the changes made after a move rather than
         * redrawing the entire level. 
        */ 
        public Image DrawChanges()
        {
            Image image1 = GetLevelImage(item1.ItemType, carDirection);
            g.DrawImage(image1, ITEM_SIZE + item1.XPos * ITEM_SIZE, ITEM_SIZE + item1.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

            Image image2 = GetLevelImage(item2.ItemType, carDirection);
            g.DrawImage(image2, ITEM_SIZE + item2.XPos * ITEM_SIZE, ITEM_SIZE + item2.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);

            if (item3 != null)
            {
                Image image3 = GetLevelImage(item3.ItemType, carDirection);
                g.DrawImage(image3, ITEM_SIZE + item3.XPos * ITEM_SIZE, ITEM_SIZE + item3.YPos * ITEM_SIZE, ITEM_SIZE, ITEM_SIZE);
            }

            return img;
        }

        /*
         * This method checks if the level has been completed. This is 
         * done by counting the number of Cars on goals compared to
         * the total number of goals on a level.
        */ 
        public bool IsCompleted()
        {
            int numberOfCarsOnGoal = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (levelMap[i, j] == ItemType.CarOnGoal)
                    {
                        numberOfCarsOnGoal++;
                    }
                }
            }

            return numberOfCarsOnGoal == numberOfGoals ? true : false;
        }

        
        // Checks what direction the player wishes to move.
        public void MoveCar(MoveDirection direction)
        {
            carDirection = direction;

            switch (direction)
            {
                case MoveDirection.Up:
                    MoveUp();
                    break;
                case MoveDirection.Down:
                    MoveDown();
                    break;
                case MoveDirection.Right:
                    MoveRight();
                    break;
                case MoveDirection.Left:
                    MoveLeft();
                    break;
            }
        }

        // Move up
        private void MoveUp()
        {
            if ((levelMap[carXPos, carYPos - 1] == ItemType.Car || levelMap[carXPos, carYPos - 1] == ItemType.CarOnGoal) && (levelMap[carXPos, carYPos - 2] == ItemType.Road || levelMap[carXPos, carYPos - 2] == ItemType.Goal))
            {
                if (levelMap[carXPos, carYPos - 2] == ItemType.Road)
                {
                    levelMap[carXPos, carYPos - 2] = ItemType.Car;
                    item3 = new Item(ItemType.Car, carXPos, carYPos - 2);
                }
                else if (levelMap[carXPos, carYPos - 2] == ItemType.Goal)
                {
                    levelMap[carXPos, carYPos - 2] = ItemType.CarOnGoal;
                    item3 = new Item(ItemType.CarOnGoal, carXPos, carYPos - 2);
                }
                else if (levelMap[carXPos, carYPos - 1] == ItemType.CarOnGoal)
                {
                    levelMap[carXPos, carYPos - 1] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos, carYPos - 1);
                }

                UpdateCarPosition();
                moves++;
                carYPos--;
            }
            else if (levelMap[carXPos, carYPos - 1] == ItemType.Road || levelMap[carXPos, carYPos - 1] == ItemType.Goal)
            {
                if (levelMap[carXPos, carYPos - 1] == ItemType.Road)
                {
                    levelMap[carXPos, carYPos - 1] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos, carYPos - 1);
                }
                else if (levelMap[carXPos, carYPos - 1] == ItemType.Goal)
                {
                    levelMap[carXPos, carYPos - 1] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos, carYPos - 1);
                }

                item3 = null;
                UpdateCarPosition();
                moves++;
                carYPos--;
            }
        }

        // Move down
        private void MoveDown()
        {
            if ((levelMap[carXPos, carYPos + 1] == ItemType.Car || levelMap[carXPos, carYPos + 1] == ItemType.CarOnGoal) && (levelMap[carXPos, carYPos + 2] == ItemType.Road || levelMap[carXPos, carYPos + 2] == ItemType.Goal))
            {
                if (levelMap[carXPos, carYPos + 2] == ItemType.Road)
                {
                    levelMap[carXPos, carYPos + 2] = ItemType.Car;
                    item3 = new Item(ItemType.Car, carXPos, carYPos + 2);
                }
                else if (levelMap[carXPos, carYPos + 2] == ItemType.Goal)
                {
                    levelMap[carXPos, carYPos + 2] = ItemType.CarOnGoal;
                    item3 = new Item(ItemType.CarOnGoal, carXPos, carYPos + 2);
                }

                if (levelMap[carXPos, carYPos + 1] == ItemType.Car)
                {
                    levelMap[carXPos, carYPos + 1] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos, carYPos + 1);
                }
                else if (levelMap[carXPos, carYPos + 1] == ItemType.CarOnGoal)
                {
                    levelMap[carXPos, carYPos + 1] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos, carYPos + 1);
                }

                UpdateCarPosition();
                moves++;
                carYPos++;
            }
            else if (levelMap[carXPos, carYPos + 1] == ItemType.Road || levelMap[carXPos, carYPos + 1] == ItemType.Goal)
            {
                if (levelMap[carXPos, carYPos + 1] == ItemType.Road)
                {
                    levelMap[carXPos, carYPos + 1] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos, carYPos + 1);
                }
                else if (levelMap[carXPos, carYPos + 1] == ItemType.Goal)
                {
                    levelMap[carXPos, carYPos + 1] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos, carYPos + 1);
                }

                item3 = null;
                UpdateCarPosition();
                moves++;
                carYPos++;
            }
        }

        // Move right
        private void MoveRight()
        {
            if ((levelMap[carXPos + 1, carYPos] == ItemType.Car || levelMap[carXPos + 1, carYPos] == ItemType.CarOnGoal) && (levelMap[carXPos + 2, carYPos] == ItemType.Road || levelMap[carXPos + 2, carYPos] == ItemType.Goal))
            {
                if (levelMap[carXPos + 2, carYPos] == ItemType.Road)
                {
                    levelMap[carXPos + 2, carYPos] = ItemType.Car;
                    item3 = new Item(ItemType.Car, carXPos + 2, carYPos);
                }
                else if (levelMap[carXPos + 2, carYPos] == ItemType.Goal)
                {
                    levelMap[carXPos + 2, carYPos] = ItemType.CarOnGoal;
                    item3 = new Item(ItemType.CarOnGoal, carXPos + 2, carYPos);
                }
                if (levelMap[carXPos + 1, carYPos] == ItemType.Car)
                {
                    levelMap[carXPos + 1, carYPos] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos + 1, carYPos);
                }
                else if (levelMap[carXPos + 1, carYPos] == ItemType.CarOnGoal)
                {
                    levelMap[carXPos + 1, carYPos] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos + 1, carYPos);
                }

                UpdateCarPosition();
                moves++;
                carXPos++;
            }
            else if (levelMap[carXPos + 1, carYPos] == ItemType.Road || levelMap[carXPos + 1, carYPos] == ItemType.Goal)
            {
                if (levelMap[carXPos + 1, carYPos] == ItemType.Road)
                {
                    levelMap[carXPos + 1, carYPos] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos + 1, carYPos);
                }
                else if (levelMap[carXPos + 1, carYPos] == ItemType.Goal)
                {
                    levelMap[carXPos + 1, carYPos] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos + 1, carYPos);
                }

                item3 = null;
                UpdateCarPosition();
                moves++;
                carXPos++;
            }
        }

        // Move left
        private void MoveLeft()
        {
            if ((levelMap[carXPos - 1, carYPos] == ItemType.Car || levelMap[carXPos - 1, carYPos] == ItemType.CarOnGoal) && (levelMap[carXPos - 2, carYPos] == ItemType.Road || levelMap[carXPos - 2, carYPos] == ItemType.Goal))
            {
                if (levelMap[carXPos - 2, carYPos] == ItemType.Road)
                {
                    levelMap[carXPos - 2, carYPos] = ItemType.Car;
                    item3 = new Item(ItemType.Car, carXPos - 2, carYPos);
                }
                else if (levelMap[carXPos - 2, carYPos] == ItemType.Goal)
                {
                    levelMap[carXPos - 2, carYPos] = ItemType.CarOnGoal;
                    item3 = new Item(ItemType.CarOnGoal, carXPos - 2, carYPos);
                }
                if (levelMap[carXPos - 1, carYPos] == ItemType.Car)
                {
                    levelMap[carXPos - 1, carYPos] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos - 1, carYPos);
                }
                else if (levelMap[carXPos - 1, carYPos] == ItemType.CarOnGoal)
                {
                    levelMap[carXPos - 1, carYPos] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos - 1, carYPos);
                }

                UpdateCarPosition();
                moves++;
                carXPos--;
            }
            else if (levelMap[carXPos - 1, carYPos] == ItemType.Road || levelMap[carXPos - 1, carYPos] == ItemType.Goal)
            {
                if (levelMap[carXPos - 1, carYPos] == ItemType.Road)
                {
                    levelMap[carXPos - 1, carYPos] = ItemType.Car;
                    item2 = new Item(ItemType.Car, carXPos - 1, carYPos);
                }
                else if (levelMap[carXPos - 1, carYPos] == ItemType.Goal)
                {
                    levelMap[carXPos - 1, carYPos] = ItemType.CarOnGoal;
                    item2 = new Item(ItemType.CarOnGoal, carXPos - 1, carYPos);
                }

                item3 = null;
                UpdateCarPosition();
                moves++;
                carXPos--;
            }
        }

        // Updates the player's position.
        private void UpdateCarPosition()
        {
            if (levelMap[carXPos, carYPos] == ItemType.Car)
            {
                levelMap[carXPos, carYPos] = ItemType.Road;
                item1 = new Item(ItemType.Road, carXPos, carYPos);
            }
            else if (levelMap[carXPos, carYPos] == ItemType.CarOnGoal)
            {
                levelMap[carXPos, carYPos] = ItemType.Goal;
                item1 = new Item(ItemType.Goal, carXPos, carYPos);
            }
        }


        /*
         * This method shows the images to be displayed on the screen.
         * It takes the pictures from the file and displays the relevant
         * one depending on the direction the car is facing etc.
        */ 
        public Image GetLevelImage(ItemType itemType, MoveDirection direction)
        {
            Image image;

            if (itemType == ItemType.Grass)
            {
                image = ImgGrass;
            }
            else if (itemType == ItemType.Road)
            {
                image = ImgRoad;
            }
            else if (itemType == ItemType.Goal)
            {
                image = ImgGoal;
            }
            else if (itemType == ItemType.Trash)
            {
                image = ImgTrash;
            }
            else if (itemType == ItemType.Building)
            {
                image = ImgBuilding;
            }
            else if (itemType == ItemType.Car)
            {
                if (direction == MoveDirection.Up)
                {
                    image = ImgCarUp;
                }
                else if (direction == MoveDirection.Down)
                {
                    image = ImgCarDown;
                }
                else if (direction == MoveDirection.Right)
                {
                    image = ImgCarRight;
                }
                else
                {
                    image = ImgCarLeft;
                }
            }
            else if (itemType == ItemType.CarOnGoal)
            {
                if (direction == MoveDirection.Up)
                {
                    image = ImgCarUpGoal;
                }
                else if (direction == MoveDirection.Down)
                {
                    image = ImgCarDownGoal;
                }
                else if (direction == MoveDirection.Right)
                {
                    image = ImgCarRightGoal;
                }
                else
                {
                    image = ImgCarLeftGoal;
                }
            }
            else
            {
                image = ImgBlankSpace;
            }

            return image;
        }

        
        // These are the pictures being taken from the file.
        public Image ImgGrass
        {
            get
            {
                return Image.FromFile("graphics/grass.png");
            }
        }

        public Image ImgRoad
        {
            get
            {
                return Image.FromFile("graphics/road1.png");
            }
        }

        public Image ImgGoal
        {
            get
            {
                return Image.FromFile("graphics/goal.png");
            }
        }

        public Image ImgCarUp
        {
            get
            {
                return Image.FromFile("graphics/carup.png");
            }
        }

        public Image ImgCarDown
        {
            get
            {
                return Image.FromFile("graphics/cardown.png");
            }
        }

        public Image ImgCarRight
        {
            get
            {
                return Image.FromFile("graphics/carright.png");
            }
        }

        public Image ImgCarLeft
        {
            get
            {
                return Image.FromFile("graphics/carleft.png");
            }
        }

        public Image ImgCarUpGoal
        {
            get
            {
                return Image.FromFile("graphics/carupgoal.png");
            }
        }

        public Image ImgCarDownGoal
        {
            get
            {
                return Image.FromFile("graphics/cardowngoal.png");
            }
        }

        public Image ImgCarRightGoal
        {
            get
            {
                return Image.FromFile("graphics/carRightGoal.png");
            }
        }

        public Image ImgCarLeftGoal
        {
            get
            {
                return Image.FromFile("graphics/carLeftGoal.png");
            }
        }

        public Image ImgTrash
        {
            get
            {
                return Image.FromFile("graphics/bin.png");
            }
        }

        public Image ImgBuilding
        {
            get
            {
                return Image.FromFile("graphics/building.png");
            }
        }

        public Image ImgBlankSpace
        {
            get
            {
                return Image.FromFile("graphics/blankSpace.bmp");
            }
        }
    }
}
