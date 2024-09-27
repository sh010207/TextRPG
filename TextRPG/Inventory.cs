using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG;

namespace TextRPG
{
    internal class Inventory
    {
        // 아이템 리스트 받아오기

        public void Add()  // 인벤에 아이템 추가
        {
            // 아이템 인벤에 추가해주는 코드
        }
        public void ShowItem(bool isEquip) // 보유한 아이템을 보여준다
        {
            //for (int i = 0; i < ItemCount; i++)  // 아이템 클래스에서 가져온다
            //{
            //    item items = ItemCount;  // 아이템 클래스에서 가져온다

            //    string index = isEquip ? $"{i + 1}" : "";   // 아이템 번호 매기기
            //    string ShowEquipped = IsEquipped(items) ? "[E]" : "";  // 장착이면 [E] 아니면 빈칸
            //    Console.WriteLine($"-{index}{ShowEquipped} | {iteminfo}"); // item info는 아이템 설명
            //}
        }

        public void Equipped() //  장착하고 있는지 없는지
        {
            int Type = 0;
            if (IsEquipped == null) // 장착하고 있다면
            {
                //그 아이템을  ReMove
                if( Type == 0) // 아이템이 무기면
                {
                    // 무기 공격력만큼 빼준다
                }
            }
            else  // 방어구면
            {
                // 방어구 방어력만큼 빼준다.
            }
        }

        public bool IsEquipped() // 장착이 되어있는지
        {
            return false;
        }

    }
}


