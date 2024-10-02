using System.Numerics;
namespace TextRPG
{
    [Serializable]
    public class Player
    {
        public string name { get; set; }
        public int level { get; set; }
        public string job { get; set; }
        public int ad { get; set; }
        public int df { get; set; }
        public int maxhp { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public float exp { get; set; }
        public float MaxExp = 20;
        public int extraAd { get; set; }
        public int extraDf { get; set; }
        public List<Item> equipItems { get; set; } = new List<Item>();
        public List<Item> Inventory { get; set; } = new List<Item>();
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
        public Player(string name, int level, string job, int ad, int df, int maxhp, int hp, float exp, int gold)
        {
            this.name = name;
            this.level = level;
            this.job = job;
            this.ad = ad;
            this.df = df;
            this.maxhp = maxhp;
            this.hp = hp;
            this.exp = exp;
            this.gold = gold;
        }
        public void PlayerInfo()
        {
            Console.WriteLine($"\t\t\t\t\t\t   Lv. {level:D2}");
            Console.WriteLine($"\t\t\t\t\t        {name} ( {job} )");
            Console.WriteLine(extraAd == 0 ? $"\t\t\t\t\t\t  공격력 {ad}" : $"\t\t\t\t\t\t  공격력 {extraAd + ad} ( + {extraAd} )");
            Console.WriteLine(extraDf == 0 ? $"\t\t\t\t\t\t  방어력 {df}" : $"\t\t\t\t\t\t  방어력 {extraDf + df} ( + {extraDf} )");
            Console.WriteLine($"\t\t\t\t\t\t체 력 {hp}/{maxhp}");
            Console.WriteLine($"\t\t\t\t\t\t  Gold {gold}");
            Console.WriteLine($"\t\t\t\t\t\t경험치 {exp}");
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
        void ClassLevel(string job)
        {
            int Ad = 0;
            int Df = 0;
            int Maxhp = 0;
            float remainExp = exp - MaxExp;
            switch (job) // 직업마다 스탯 오르는게 다름
            {
                case "개복치": //공격
                    level += 1;
                    Ad = 5;
                    Df = 0;
                    Maxhp = 3;
                    ad += Ad;
                    df += Df;
                    maxhp += Maxhp;
                    hp = maxhp;
                    break;
                case "망둥어": //균형
                    Ad = 3;
                    Df = 2;
                    Maxhp = 4;
                    level += 1;
                    ad += Ad;
                    df += Df;
                    maxhp += Maxhp;
                    hp = maxhp;
                    break;
                case "블롭피쉬": //탱커
                    Ad = 1;
                    Df = 3;
                    Maxhp = 7;
                    level += 1;
                    ad += Ad;
                    df += Df;
                    maxhp += Maxhp;
                    hp = maxhp;
                    break;
                case "우파루파": // 체력탱커
                    Ad = 1;
                    Df = 2;
                    Maxhp = 10;
                    level += 1;
                    ad += Ad;
                    df += Df;
                    maxhp += Maxhp;
                    hp = maxhp;
                    break;
            }
            exp = 0;
            exp += remainExp;
            MaxExp *= 1.1f;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n\t\t\t\t\t        !레벨업! ");
            Console.ResetColor();
            Console.Write(" 현재 레벨 : ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"{level}");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("\t\t\t\t     스탯 증갸량");
            Console.ResetColor();
            Console.Write(" : 공격력 : ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Ad}");
            Console.ResetColor();
            Console.Write(" : 방어력 : ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"{Df}");
            Console.ResetColor();
            Console.Write(" : 체력 : ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"{Maxhp}");
            Console.ResetColor();
            Console.Write($"\t\t\t\t      현재 스탯 : 공격력 : {ad} 방어력 : {df} 체력 : {maxhp}\n");
            Console.ReadKey();
        }
        public void LevelUp()
        {
            while (exp >= MaxExp) ClassLevel(job);
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
            for (int i = 0; i < equipItems.Count; i++)
            {
                if (equipItems[i].itemName == item.itemName)
                {
                    return true;
                }
            }
            return false;
        }
        //아이템 구매 시 골드 차감
        public void DecreaseGold(Item item) //cha - 금액 수정
        {
            gold -= item.itemPrice; // 금액 차감
            Inventory.Add(item); // 인벤토리리스트에 배열 추가
        }
        public bool HasItem(Item item) // cha - 인벤토리 갯수 불러오기
        {
            for (int i = 0; i < Inventory.Count; i++) 
            { 
                if (Inventory[i].itemName == item.itemName)
                {
                    return true;
                }
            }
            return false;
        }
        //사망 시 체력 초기화
        public void ResetHp()
        {
            hp = maxhp;
        }
        public void PlayerQuestListUI()
        {
        }
    }
}