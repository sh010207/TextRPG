using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    internal class Inventory
    {
        private Player player;

        public Inventory(Player player)
        {
            this.player = player;
        }


        public void DisplayInventoryUI() // 내 가방에 뭐있나..
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t\t\t IIIII  N   N  V     V  EEEEE  N   N  TTTTT  OOOOO  RRRRR  Y   Y\r\n \t\t\t   I    NN  N   V   V   E      NN  N    T   O     O R    R  Y Y \r\n\t\t\t   I    N N N    V V    EEEE   N N N    T   O     O RRRRR    Y  \r\n\t\t\t   I    N  NN     V     E      N  NN    T   O     O R   R    Y  \r\n\t\t\t IIIII  N   N     V     EEEEE  N   N    T    OOOOO  R    R   Y\r\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\t\t\t\t      ><(((°>   가방입니다.   <°)))><\n\n");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t     보유중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t       [ 아이템 목록 ]\n");
            Console.ResetColor();

            ShowInventory(false);

            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t\t1 : 장착 관리");
            Console.WriteLine("\t\t\t\t\t\t0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t\t원하시는 행동을 입력해주세요.\n");

            int result = GameManager.SelectBehavior(0, 1);

            switch (result)
            {
                case 0:
                    GameManager.GameStartUI();
                    break;

                case 1:
                    DisplayEquipUI();
                    break;
            }

        }

        public void DisplayEquipUI() //장착 할래말래?
        {
            Console.Clear();
            List<Item> items = new List<Item>();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\t\t\t IIIII  N   N  V     V  EEEEE  N   N  TTTTT  OOOOO  RRRRR  Y   Y\r\n \t\t\t   I    NN  N   V   V   E      NN  N    T   O     O R    R  Y Y \r\n\t\t\t   I    N N N    V V    EEEE   N N N    T   O     O RRRRR    Y  \r\n\t\t\t   I    N  NN     V     E      N  NN    T   O     O R   R    Y  \r\n\t\t\t IIIII  N   N     V     EEEEE  N   N    T    OOOOO  R    R   Y\r\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\t\t\t\t      ><(((°>   장착하세요!   <°)))><\n\n");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t    보유 중인 아이템을 관리할 수 있습니다.\n\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t       [ 아이템 목록 ]\n");
            Console.ResetColor();
            ShowInventory(true); // 아이템 목록 표시 ( 인덱스 o 누르면 장착);

            Console.WriteLine("\n\t\t\t\t\t\t0 : 나가기\n\n\t\t\t\t\t원하시는 행동을 입력해주세요.\n");
            int result = GameManager.SelectBehavior(0, player.InventoryCount);

            switch (result)
            {
                case 0:
                    GameManager.GameStartUI();
                    break;
                default:
                    int itemIndex = result - 1;
                    Item targetItem = player.returnInventory[itemIndex];
                    player.EquipItem(targetItem);
                    DisplayEquipUI();
                    break;
            }

        }

        public void ShowInventory(bool ItemIndex) // 인벤 보여주기
        {
            for (int i = 0; i < player.InventoryCount; i++)
            {
                Item items = player.Inventory[i];

                string ShowItemIndex = ItemIndex ? $"{i + 1}" : "";
                string ShowEquipItems = player.IsEquipped(items) ? $"[E]" : ""; // class Player에서 Player가 장착되어있는지 확인 / 되어있다면 [E]출력 아니면 공백
                Console.WriteLine($"\t\t\t {ShowItemIndex} {ShowEquipItems} {items.ItemInfoText()}"); // -번호 [E] | 아이템 설명
                if(ItemIndex == true)
                    GameManager.quest.QuestProgress();
            }
            if (player.InventoryCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n\t\t\t\t\t[ 보유한 아이템이 없습니다.]      \n\n");
                Console.ResetColor();
            }
        }
    }
}


