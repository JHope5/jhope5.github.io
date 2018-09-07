using System.Collections;
using System.Xml;
using System.IO;
using System.Reflection;

namespace EcoDrive
{
    class LevelSet
    {
        // An array of the levels in an XML level set.
        private ArrayList levels = new ArrayList();

        private string title = string.Empty;
        private string description = string.Empty;
        private string author = string.Empty;
        private string fileName = string.Empty;

        private int currentLevel = 0;
        private int numberOfLevelsInSet = 0;
        private int lastCompletedLevel = 0;

        public string Title
        {
            get 
            { 
                return title; 
            }
        }

        public string Description
        {
            get 
            { 
                return description; 
            }
        }

        public string Author
        {
            get 
            { 
                return author; 
            }
        }

        public string FileName
        {
            get 
            { 
                return fileName; 
            }
        }

        public int NumberOfLevelsInSet
        {
            get 
            { 
                return numberOfLevelsInSet; 
            }
        }

        public int CurrentLevel
        {
            get 
            { 
                return currentLevel; 
            }
            set 
            { 
                currentLevel = value; 
            }
        }

        public int LastCompletedLevel
        {
            set 
            { 
                lastCompletedLevel = value; 
            }
        }

        public LevelSet(string aTitle, string aDescription, string aAuthor, int aNumberOfLevels, string aFileName)
        {
            title = aTitle;
            description = aDescription;
            author = aAuthor;
            numberOfLevelsInSet = aNumberOfLevels;
            fileName = aFileName;
        }

        public LevelSet() 
        { 
        }


        // Indexer for the LevelSet object.
        public Level this[int index]
        {
            get
            {
                return (Level)levels[index];
            }
        }


        // Setting the general information of the level set.
        public void SetLevelSet(string setName)
        {
            // Loading the XML file.
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(setName);

            fileName = setName;
            title = Xdoc.SelectSingleNode("//Title").InnerText;
            description = Xdoc.SelectSingleNode("//Description").InnerText;

            XmlNode levelCollection = Xdoc.SelectSingleNode("//LevelCollection");
            author = levelCollection.Attributes["Copyright"].Value;
            XmlNodeList levels = Xdoc.SelectNodes("//Level");
            numberOfLevelsInSet = levels.Count;
        }


        // This method is called when a level set has been selected.
        public void SetLevelsInLevelSet(string setName)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(setName);

            // Get all Level elements from the level set
            XmlNodeList levelInfoList = Xdoc.SelectNodes("//Level");

            int levelNumber = 1;
            foreach (XmlNode levelInfo in levelInfoList)
            {
                LoadLevel(levelInfo, levelNumber);
                levelNumber++;
            }
        }


        // This method reads the level information from a level node.
        private void LoadLevel(XmlNode levelInfo, int levelNumber)
        {
            // Read the attributes from the level element            
            XmlAttributeCollection xac = levelInfo.Attributes;
            string levelName = xac["Id"].Value;
            int levelWidth = int.Parse(xac["Width"].Value);
            int levelHeight = int.Parse(xac["Height"].Value);
            int numberOfGoals = 0;

            // Read the layout of the level
            XmlNodeList levelLayout = levelInfo.SelectNodes("L");

            // Declare the level map
            ItemType[,] levelMap = new ItemType[levelWidth, levelHeight];

            // Read the level line by line
            for (int i = 0; i < levelHeight; i++)
            {
                string line = levelLayout[i].InnerText;
                bool grassEncountered = false;

                // Read the line character by character
                for (int j = 0; j < levelWidth; j++)
                {
                    // If the end of the line is shorter than the width of the
                    // level, then the rest of the line is filled with spaces.
                    if (j >= line.Length)
                        levelMap[j, i] = ItemType.BlankSpace;
                    else
                    {
                        switch (line[j].ToString())
                        {
                            case " ":
                                if (grassEncountered)
                                {
                                    levelMap[j, i] = ItemType.Road;
                                }
                                    
                                else
                                { 
                                    levelMap[j, i] = ItemType.BlankSpace;
                                }
                                break;
                            case "#":
                                levelMap[j, i] = ItemType.Grass;
                                grassEncountered = true;
                                break;
                            case "G":
                                levelMap[j, i] = ItemType.Goal;
                                numberOfGoals++;
                                break;
                            case "C":
                                levelMap[j, i] = ItemType.Car;
                                break;
                            case "*":
                                levelMap[j, i] = ItemType.CarOnGoal;
                                numberOfGoals++;
                                break;
                            case "B":
                                levelMap[j, i] = ItemType.Building;
                                break;
                            case "T":
                                levelMap[j, i] = ItemType.Trash;
                                break;
                            case "=":
                                levelMap[j, i] = ItemType.BlankSpace;
                                break;
                        }
                    }
                }
            }

            // Adding a new level to the collection of levels in the level set.
            levels.Add(new Level(levelName, levelMap, levelWidth, levelHeight, numberOfGoals, levelNumber, title));
        }


        // This method obtains a list of the level sets available.
        public static ArrayList GetAllLevelSetInfos()
        {
            ArrayList levelSets = new ArrayList();

            // Read current path and remove the 'file:/' from the string
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

            // Read all files from the levels directory
            string[] fileEntries = Directory.GetFiles(path + "/levels");

            // Read the level info from the files with an .xml extension
            foreach (string fileName in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(fileName);

                if (fileInfo.Extension.Equals(".xml"))
                {
                    XmlDocument Xdoc = new XmlDocument();
                    Xdoc.Load(fileName);

                    string title = Xdoc.SelectSingleNode("//Title").InnerText;
                    string description = Xdoc.SelectSingleNode("//Description").InnerText;
                    XmlNode levelInfo = Xdoc.SelectSingleNode("//LevelCollection");
                    string author = levelInfo.Attributes[0].Value;
                    XmlNodeList levels = Xdoc.SelectNodes("//Level");

                    levelSets.Add(new LevelSet(title, description, author, levels.Count, fileName));
                }
            }

            return levelSets;
        }
    }
}
