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
    }
}
