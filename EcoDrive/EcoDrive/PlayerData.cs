using System.Collections;
using System.IO;
using System.Reflection;
using System.Xml;

namespace EcoDrive
{
    /*
     * This class deals with the XML file used to store the user's
     * data and update the information for whenever the user
     * completes a level.
    */
    class PlayerData
    {
        private string name = string.Empty;             // Player name
        private string fileName = string.Empty;         // Savegame URI
        private int lastCompletedLevel = 0;
        private string lastPlayedSet = string.Empty;

        public string Name
        {
            get
            { 
                return name; 
            }
        }

        public int LastCompletedLevel
        {
            get 
            { 
                return lastCompletedLevel; 
            }
        }

        public string LastPlayedSet
        {
            get 
            { 
                return lastPlayedSet; 
            }
        }

        // Setting the player's name, and the path to the XML file.
        public PlayerData(string aName)
        {
            name = aName;

            // Getting the current filepath
            string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);
            fileName = filePath + "/SavedGames/" + aName + ".xml";
        }

        // Creating a new XML file for a new player.
        public void CreatePlayer(LevelSet levelSet)
        {
            XmlDocument Xdoc = new XmlDocument();

            // Create new file (playerName.xml) in SavedGames directory.
            XmlTextWriter writer = new XmlTextWriter(fileName, null);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteProcessingInstruction("xml",
                "version='1.0' encoding='ISO-8859-1'");
            writer.WriteStartElement("savegame");
            writer.Close();

            Xdoc.Load(fileName);

            // Adding all elements to the XML file.
            XmlNode root = Xdoc.DocumentElement;
            XmlElement playerName = Xdoc.CreateElement("playerName");
            playerName.InnerText = name;
            XmlElement lastPlayedNameSet = Xdoc.CreateElement("lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.FileName;
            XmlElement lastCompletedLevel = Xdoc.CreateElement("lastCompletedLevel");
            lastCompletedLevel.InnerText = "0";
            XmlElement levelSets = Xdoc.CreateElement("levelSets");

            XmlElement nodeLevelSet = Xdoc.CreateElement("levelSet");
            XmlAttribute xa = Xdoc.CreateAttribute("title");
            xa.Value = levelSet.Title;
            nodeLevelSet.Attributes.Append(xa);
            XmlElement lastCompletedLevelInSet = Xdoc.CreateElement("lastCompletedLevelInSet");
            lastCompletedLevelInSet.InnerText = "0";

            nodeLevelSet.AppendChild(lastCompletedLevelInSet);
            levelSets.AppendChild(nodeLevelSet);
            root.AppendChild(playerName);
            root.AppendChild(lastPlayedNameSet);
            root.AppendChild(lastCompletedLevel);
            root.AppendChild(levelSets);

            Xdoc.Save(fileName);

        }

        // Creating a new XML file for a new player.
        public void LoadPlayer(LevelSet levelSet)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(fileName);

            XmlNode lastPlayedNameSet = Xdoc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedNameSet.InnerText = levelSet.FileName;
                        
            Xdoc.Save(fileName);
            lastCompletedLevel = 0;
        }

        // This method loads the last game played by the user.
        public void LoadLastGame()
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(fileName);

            XmlNode lastPlayedNameSet = Xdoc.SelectSingleNode("//lastPlayedNameSet");
            lastPlayedSet = lastPlayedNameSet.InnerText;
            XmlNode lastCompletedLvl = Xdoc.SelectSingleNode("//lastCompletedLevel");
            lastCompletedLevel = int.Parse(lastCompletedLvl.InnerText);

        }


        // This method saves the level set.
        public void SaveLevelSet(LevelSet levelSet)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(fileName);

            XmlNode lastFileName = Xdoc.SelectSingleNode("//lastPlayedNameSet");
            lastFileName.InnerText = levelSet.FileName;
            XmlNode lastCompletedLvl = Xdoc.SelectSingleNode("//lastCompletedLevel");
            lastCompletedLvl.InnerText = "0";

            XmlNode setName = Xdoc.SelectSingleNode("/SavedGames/levelSets/" + "levelSet[@title = \"" + levelSet.Title + "\"]");

            // Playing the set for the first time.
            if (setName == null) 
            {
                XmlNode levelSets = Xdoc.GetElementsByTagName("levelSets")[0];

                XmlElement newLevelSet = Xdoc.CreateElement("levelSet");
                XmlAttribute xa = Xdoc.CreateAttribute("title");
                xa.Value = levelSet.Title;
                newLevelSet.Attributes.Append(xa);
                XmlElement lastCompletedLevelInSet = Xdoc.CreateElement("lastCompletedLevelInSet");
                lastCompletedLevelInSet.InnerText = "0";

                newLevelSet.AppendChild(lastCompletedLevelInSet);
                levelSets.AppendChild(newLevelSet);
            }

            Xdoc.Save(fileName);
        }

        // This method saves the level that the user is on so they can continue later.
        public void SaveLevel(Level level)
        {
            XmlDocument Xdoc = new XmlDocument();
            Xdoc.Load(fileName);

            XmlNode lastCompletedLvl = Xdoc.SelectSingleNode("//lastCompletedLevel");
            lastCompletedLvl.InnerText = level.LevelNumber.ToString();

            XmlNode setName = Xdoc.SelectSingleNode("/SavedGames/levelSets/" + "levelSet[@title = \"" + level.LevelSetName + "\"]");
            try
            {
                XmlNode nodeLevel = setName.SelectSingleNode("level[@levelNumber = " + level.LevelNumber + "]");

                if (nodeLevel == null)
                {
                    XmlElement nodeNewLevel = Xdoc.CreateElement("level");
                    XmlAttribute xa = Xdoc.CreateAttribute("levelNumber");
                    xa.Value = level.LevelNumber.ToString();
                    nodeNewLevel.Attributes.Append(xa);
                    XmlElement moves = Xdoc.CreateElement("moves");
                    moves.InnerText = level.Moves.ToString();

                    nodeNewLevel.AppendChild(moves);
                    setName.AppendChild(nodeNewLevel);
                }
                else
                {
                    XmlElement moves = nodeLevel["moves"];
                    int numberOfMoves = int.Parse(moves.InnerText);
                }
            }
            catch
            {
            }

            Xdoc.Save(fileName);
        }


        // This obtains a list of the players who have played the game.
        public static ArrayList GetPlayers()
        {
            ArrayList playerNames = new ArrayList();

            // Read current path and remove the 'file:/' from the string
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase).Substring(6);

            // Read all files from the savegame directory
            string[] fileEntries = Directory.GetFiles(path + "/SavedGames");

            // Read the playerName tag from the files with an .xml extension
            foreach (string fileName in fileEntries)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                if (fileInfo.Extension.Equals(".xml"))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fileName);

                    XmlNode playerName = doc.SelectSingleNode("//playerName");
                    if (playerName != null)
                        playerNames.Add(playerName.InnerText);
                }
            }

            return playerNames;
        }
    }


}
