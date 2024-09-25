using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Inventory
    {
        Job job = new Job();
        private List<Item> myItems = new List<Item>();
        public int invenIndex = 0;
        PlayerStatus status;

        public void Add(Item item)
        {
            myItems.Add(item); // 아이템 인벤에 추가
        }



        public void Show()
        {
            invenIndex = myItems.Count;
            if (myItems.Count < 1)
            {
                Console.WriteLine("보유중인 아이템이 없습니다.");
            }
            else
            {
                   for (int i = 0; i < myItems.Count; i++)
                   {
                       string equippedStatus = myItems[i].Isitem ? "[E]" : "";
                       Console.WriteLine($" {equippedStatus}{myItems[i].Name} | 공격력+{myItems[i].Power} | 방어력+{myItems[i].Def} | {myItems[i].Desc}");
                   }
            }
        }

        public void SelectItem()
        {
            for (int i = 0; i < myItems.Count; i++)
            {
                Console.WriteLine($"- {i + 1} {myItems[i].Name} | 공격력+{myItems[i].Power} | 방어력+{myItems[i].Def} | {myItems[i].Desc}");
            }
        }

        //장착하기 (혼자 못했음)
        public void EquipItem(int itemIndex)
        {
            foreach( var item in myItems) // 아이템 리스트 돌면서
            {
                if(item.Isitem) // 장착이 되어있으면 해제
                {
                    item.Isitem = false;
                    Console.WriteLine("아이템이 해제되었습니다.");
                }
                else
                {
                    myItems[itemIndex].Isitem = true; // 장착 안되어 있으면 장착\
                    Console.WriteLine("아이템이 장착되었습니다.");
                }
            }
        }

        public void EquipitemStat(PlayerStatus status)
        {
            foreach (var item in myItems)
            {
                if (item.Isitem == true)
                {
                    status.InvenItemStat_AT += item.Power;
                    status.InvenItemStat_DF += item.Def;
                }
                if (item.Isitem == false)
                {
                    item.Power -= status.InvenItemStat_AT;
                    item.Def -= status.InvenItemStat_DF;
                }
            }
        }
    }  
}
