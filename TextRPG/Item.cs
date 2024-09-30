using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Item
    {
        public string itemName { get; }
        public int itemType { get; }
        public int itemValue { get; }
        public string itemDesc { get; }
        public int itemPrice { get; }
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
            return ($"{itemName} | {DisplayTypeText} +{itemValue} | {itemDesc} ");
        }
    }
}
