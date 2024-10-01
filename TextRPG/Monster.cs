using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    //몬스터 클래스
    public class Monster 
    {
        public int Lv { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public bool IsDead { get; set; }

        public Monster(int lv,string name, int hp, int atk) 
        {

            Lv = lv;
            Name = name;
            Hp = hp;
            Atk = atk;
        }

         static List<Monster> Monsters = new List<Monster>();

        public void monsterLineup(List<Monster>monsters)
        {
            List<Monster> Monsters = new List<Monster>();
            monsters = Monsters;
            {
                Monsters.Add(new Monster(2, "슬라임", 10, 5));
                Monsters.Add(new Monster(5, "고블린", 15, 7));
                Monsters.Add(new Monster(7, "오크", 25, 10));
            };
        }

    }
}
