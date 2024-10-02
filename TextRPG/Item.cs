using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    [Serializable]
    public class Item
    {
        public string itemName { get; set; }
        public int itemType { get; set; }
        public int itemValue { get; set; }
        public string itemDesc { get; set; }
        public int itemPrice { get; set;}
        public string DisplayTypeText
        {
            get
            {
                return itemType == 0 ? "공격력" : "방어력";
            }
        }

        public Item(string itemname, int itemtype, int itemvalue, string itemdesc, int itemprice)
        {
            itemName = itemname;
            itemType = itemtype;
            itemValue = itemvalue;
            itemDesc = itemdesc;
            itemPrice = itemprice;
        }
       public string ItemInfoText()
        {
            return ($"{itemName} | {DisplayTypeText}+{itemValue:D2} | {itemDesc} ");
        }
    }
}
