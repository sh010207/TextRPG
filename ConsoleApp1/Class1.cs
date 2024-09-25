using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class PlayerStatus
    {
        Inventory Inventory;

        public int InvenItemStat_AT {  get; set; }
        public int InvenItemStat_DF { get; set; }

        public int TotalStat {  get; set; }

        public int Level {  get; set; }
        
        private int _level;
        public int Gold { get; set; }

        public PlayerStatus(int level, int gold)
        {
            Level = level;
            Gold = gold;
            InvenItemStat_AT = 0;
            InvenItemStat_DF = 0;

            TotalStat = 0;
        }
    }
    internal class PlayerJob : Job
    {
        public PlayerJob(string playerclass, int power, int def, int hp)
        {
            PlayerClass = playerclass;
            Power = power;
            Def = def;
            Hp = hp;
        }

    }
}
