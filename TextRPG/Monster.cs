using System;
using System.Collections.Generic;
using System.Linq;
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
        public bool IsDead =>  Hp <= 0;

        public Monster(int lv,string name, int hp, int atk) 
        {

            Lv = lv;
            Name = name;
            Hp = hp;
            Atk = atk;
        }
    }
}
