using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    internal class Inventory
    {
        private Item item;
        private Player player;
        private List<Item> inventory = new List<Item>();
        public int InventoryCount // 아이템 갯수 받아오기
        {
            get
            {
                return inventory.Count;
            }
        }

        public void Add()  // 인벤에 아이템 추가
        {
            // 아이템 인벤에 추가해주는 코드
        }

        public void ShowInventory(bool ItemIndex) // 인벤 보여주기
        {
            for (int i = 0; i < InventoryCount; i++)
            {
                Item items = inventory[i];

                string ShowItemIndex = ItemIndex ? $"{i + 1}" : "";
                string ShowEquipItems = player.IsEquipped(items) ? $"[E]" : ""; // class Player에서 Player가 장착되어있는지 확인 / 되어있다면 [E]출력 아니면 공백
                Console.WriteLine($"-{ShowItemIndex} {ShowEquipItems} |  {items.ItemInfoText}"); // -번호 [E] | 아이템 설명
            }
        }
    }
}


