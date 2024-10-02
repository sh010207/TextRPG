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
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[ 아이템 목록 ]\n");

            ShowInventory(false);

            Console.WriteLine();
            Console.WriteLine("1 : 장착 관리");
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

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
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            ShowInventory(true); // 아이템 목록 표시 ( 인덱스 o 누르면 장착);

            Console.WriteLine("\n0. 나가기\n원하시는 행동을 입력해주세요.");
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
                    GameManager.quest.QuestProgress();
                    DisplayEquipUI();
                    break;
            }

            //public void Add()  // 인벤에 아이템 추가
            //{
            //    // 아이템 인벤에 추가해주는 코드
            //}
        }

        public void ShowInventory(bool ItemIndex) // 인벤 보여주기
        {
            for (int i = 0; i < player.InventoryCount; i++)
            {
                Item items = player.Inventory[i];

                string ShowItemIndex = ItemIndex ? $"{i + 1}" : "";
                string ShowEquipItems = player.IsEquipped(items) ? $"[E]" : ""; // class Player에서 Player가 장착되어있는지 확인 / 되어있다면 [E]출력 아니면 공백
                Console.WriteLine($"- {ShowItemIndex} {ShowEquipItems} {items.ItemInfoText()}"); // -번호 [E] | 아이템 설명
            }
            if (player.InventoryCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n   [ 보유한 아이템이 없습니다.]      \n\n");
                Console.ResetColor();
            }
        }
    }
}


