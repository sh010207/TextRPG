using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Item
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Power { get; set; }
        public int Def {  get; set; }
        public int Gold { get; set; }
        public bool Isitem { get; set; }
        public bool Owneditems {  get; set; }
        public Item(string name, string desc, int power, int def, int gold) 
        {
            Name = name;
            Desc = desc;
            Power = power;
            Def = def;
            Gold = gold;
            Isitem = false;
            Owneditems = false;
        }

    }
}
