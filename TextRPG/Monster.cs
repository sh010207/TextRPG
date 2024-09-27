using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public interface IMonster
    {
        int Lv { get;}
        string Name { get; }
        int Hp { get; set; }
        int Atk { get; }
        bool IsDead { get; }
    }

    //몬스터 클래스
    public class Monster : IMonster
    {
        public int Lv { get;}
        public string Name { get; }
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

    public class Slime : Monster
    {
        public Slime(string name) : base(2,name, 10, 5) { }
    }
    public class Goblin : Monster
    {
        public Goblin(string name) : base(5,name, 15, 7) { }
    }
    public class Oak : Monster
    {
        public Oak(string name) : base(7,name, 25, 10) { }
    }
}
