namespace EcoDrive
{
    class Item
    {
        private ItemType itemType;  // Item types are the grass, road, etc..
        private int xPos;           // The X position of an item on the level.
        private int yPos;           // The Y position of an item on the level.


        public Item(ItemType aItemType, int aXPos, int aYPos)
        {
            itemType = aItemType;
            xPos = aXPos;
            yPos = aYPos;
        }


        public ItemType ItemType
        {
            get 
            { 
                return itemType; 
            }
        }

        public int XPos
        {
            get 
            { 
                return xPos; 
            }
        }

        public int YPos
        {
            get 
            { 
                return yPos; 
            }
        }
    }
}
