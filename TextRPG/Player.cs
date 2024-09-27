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

        //private List<Item> equipItems = new List<Item>();

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

        public void InventoryInfo()
        {

        }

        //아이템 장착 [E]
        public void EquipItem()
        {

        }

        public bool IsEquipped()
        {
            return false;
        }

        //아이템 구매 시 골드 차감
        public void DecreaseGold()
        {
            //gold -= 아이템가격
        }

        public void HasItem()
        {

        }

    }
}
