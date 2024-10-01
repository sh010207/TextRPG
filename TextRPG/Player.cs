using System.Numerics;

namespace TextRPG
{
     public class Player
    {
       
        public string name { get; set; }
        public int level {  get; set; }
        public string job { get; set; }
        public int ad {  get; set; }
        public int df { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public int extraAd {  get; set; }
        public int extraDf { get; set; }

        private List<Item> equipItems = new List<Item>();
        private List<Item> Inventory = new List<Item>();


        public int InventoryCount // 아이템 갯수 받아오기
        {
            get
            {
                return Inventory.Count;
            }
        }

        public Player(string name, int level, string job, int ad, int df, int hp, int gold)
        {
            this.name = name;
            this.level = level;
            this.job = job;
            this.ad = ad;
            this.df = df;
            this.hp = hp;
            this.gold = gold;
        }

        public void PlayerInfo()
        {
            Console.WriteLine($"Lv. {level:D2}");
            Console.WriteLine($"Job ( {job} )");
            Console.WriteLine(extraAd == 0 ? $"공격력 {ad}" : $"공격력 {ad} + ( {extraAd} )");
            Console.WriteLine(extraDf == 0 ? $"방어력 {df}" : $"방어력{df} + ( {extraDf} )");
            Console.WriteLine($"체 력 {hp}");
            Console.WriteLine($"Gold {gold}");
        }

        public void ShowInventory(bool ItemIndex) // 인벤 보여주기
        {
            for (int i = 0; i < InventoryCount; i++)
            {
                Item items = Inventory[i];

                string ShowItemIndex = ItemIndex ? $"{i + 1}" : "";
                string ShowEquipItems = IsEquipped(items) ? $"[E]" : ""; // class Player에서 Player가 장착되어있는지 확인 / 되어있다면 [E]출력 아니면 공백
                Console.WriteLine($"-{ShowItemIndex} {ShowEquipItems} |  {items.ItemInfoText}"); // -번호 [E] | 아이템 설명
            }
            if(InventoryCount == 0)
            {
                Console.WriteLine("   [보유한 아이템이 없습니다.]      \n\n");
            }    
        }

        //public void InventoryInfo()
        //{


        //}

        //아이템 장착 [E]
        public void EquipItem(Item item)
        {
            if(IsEquipped(item))         // 아이템 장착이 되어있다면
            {
                equipItems.Remove(item);    // 장비아이템에서 아이템을 빼준다.
                if (item.itemType == 0)     // 빼준아이템 타입이 0이면
                {
                    extraAd -= item.itemValue;    //  표시된 무기 공격력에서 무기공격력만큼 빼준다.
                }
                if(item.itemType == 1)    //  타입이 1이면
                {
                    extraDf -= item.itemValue;  // 표시된 방어구 방어력에서 방어구방어력만큼 빼준다.
                }
            }
            else                         // 장착이 안되어있다면
            {
                equipItems.Add(item);
                if(item.itemType == 0)
                {
                    extraAd += item.itemValue;
                }
                if(item.itemType == 1)
                {
                    extraDf += item.itemValue;
                }
            }
        }

        public bool IsEquipped(Item item)
        {
            return equipItems.Contains(item);
        }

        //아이템 구매 시 골드 차감
        public void DecreaseGold()
        {
            //gold -= 아이템가격
        }

        public void HasItem()
        {

        }

        //사망 시 체력 초기화
        public void ResetHp()
        {
            hp = 100;
        }

    }
}
