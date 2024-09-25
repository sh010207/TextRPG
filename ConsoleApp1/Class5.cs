using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    
    internal class Job
    {
        public string PlayerClass { get; set; }
        public int Power { get; set; }
        public int Def { get; set; }
        public int Hp { get; set; }

        public void worrior()
        {
            PlayerClass = "전사";
            Power = 10;
            Def = 5;
            Hp = 100;
        }
        public void Thief()
        {
            PlayerClass = "도적";
            Power = 15;
            Def = 3;
            Hp = 75;
        }
    }
}
