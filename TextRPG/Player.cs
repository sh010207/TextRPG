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

        public int maxhp { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public int extraAd {  get; private set; }
        public int extraDf { get; private set; }

        public List<Item> equipItems = new List<Item>();
        public List<Item> Inventory = new List<Item>();

        private static Item item;

        public int InventoryCount // 아이템 갯수 받아오기
        {
            get
            {
                return Inventory.Count;
            }
        }

        public List<Item> returnInventory //인벤토리 개수 받아오기
        {
            get
            {
                return Inventory;
            }

        }

        public Player(string name, int level, string job, int ad, int df, int maxhp, int hp, int gold)
        {
            this.name = name;
            this.level = level;
            this.job = job;
            this.ad = ad;
            this.df = df;
            this.maxhp = maxhp;
            this.hp = hp;
            this.gold = gold;
        }

        public void PlayerInfo()
        {
            Console.WriteLine($"Lv. {level:D2}");
            Console.WriteLine($"{name} ( {job} )"); ////Cha 하드코딩되어있어서 이름으로 변경
            Console.WriteLine(extraAd == 0 ? $"공격력 {ad}" : $"공격력 {extraAd + ad} ( + {extraAd} )");
            Console.WriteLine(extraDf == 0 ? $"방어력 {df}" : $"방어력 {extraDf + df} ( + {extraDf} )");
            Console.WriteLine($"체 력 {hp}/{maxhp}");
            Console.WriteLine($"Gold {gold}");
        }

        public void ChangeJob(string job, int ad, int df, int maxhp, int hp, int gold) //직업선택을 위한
        {
            this.job = job; 
            this.ad = ad;
            this.df = df;
            this.maxhp = maxhp;
            this.hp = hp;
            this.gold = gold;
        }

        //아이템 장착 [E]
        public void EquipItem(Item item)
        {
            if (IsEquipped(item))         // 아이템 장착이 되어있다면
            {
                equipItems.Remove(item);    // 장비아이템에서 아이템을 빼준다.
                if (item.itemType == 0)     // 빼준아이템 타입이 0이면
                {
                    extraAd -= item.itemValue;    //  표시된 무기 공격력에서 무기공격력만큼 빼준다.
                }
                if (item.itemType == 1)    //  타입이 1이면
                {
                    extraDf -= item.itemValue;  // 표시된 방어구 방어력에서 방어구방어력만큼 빼준다.
                }
            }
            else                         // 장착이 안되어있다면
            {
                equipItems.Add(item);
                if (item.itemType == 0)
                {
                    extraAd += item.itemValue;
                }
                if (item.itemType == 1)
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
        public void DecreaseGold(Item item) //cha - 금액 수정
        {
            gold -= item.itemPrice; // 금액 차감
            Inventory.Add(item); // 인벤토리리스트에 배열 추가
        }

        public bool HasItem(Item item) // cha - 인벤토리 갯수 불러오기
        {
            return Inventory.Contains(item);
        }

        //사망 시 체력 초기화
        public void ResetHp()
        {
            hp = 100;
        }

    }
}
