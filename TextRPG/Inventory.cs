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
            Console.WriteLine("[ 아이템 목록 ]");

            player.ShowInventory(false);

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

        }

        public void Add()  // 인벤에 아이템 추가
        {
            // 아이템 인벤에 추가해주는 코드
        }


    }
}


