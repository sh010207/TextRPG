using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Shop
    {
        private List<Item> shopItems;
        public int shopItemsCount = 0;

        public Shop()
        {
            shopItems = new List<Item>
            {
                new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 1000),
                new Item("무쇠갑옷", "무쇠로만들어져 단단한 갑옷입니다.", 0, 9, 1500),
                new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 3000),
                new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 600),
                new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 1500),
                new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 3000),
            };
        }

        public void ShowShop()
        {
            for (int i = 0; i < shopItemsCount; i++)
            {
                string Buy = shopItems[i].Owneditems ? "[구매완료]" : $"{shopItems[i].Gold}G";
                Console.WriteLine($"| {shopItems[i].Name} | 공격력 +{shopItems[i].Power} |" + $"방어력+{shopItems[i].Def} | {shopItems[i].Desc} | {Buy}");
            }
        }

        public void PurchaseShop()
        {
            shopItemsCount = shopItems.Count;
            for(int i = 0; i< shopItemsCount;i++)
            {
                string isBought = shopItems[i].Owneditems ? "[구매완료]" : $"{shopItems[i].Gold}G";
                Console.WriteLine($"-{i + 1} {shopItems[i].Name} | 공격력+{shopItems[i].Power} |" +
                    $"방어력+{shopItems[i].Def} | {shopItems[i].Desc} | {isBought}");
            }
        }

        public void PurchaseItem(int itemIndex , PlayerStatus status, Inventory inventory )
        {
            Item selectItem = shopItems[(int)itemIndex - 1];
            if(status.Gold >= selectItem.Gold)
            {
                inventory.Add(selectItem);
                selectItem.Owneditems = true;
                status.Gold -= selectItem.Gold;
                Console.WriteLine($"골드가 {status.Gold}G 남았습니다.");
            }
            else
            {
                Console.WriteLine("골드가 부족합니다."); 
            }
        }
    }
}
