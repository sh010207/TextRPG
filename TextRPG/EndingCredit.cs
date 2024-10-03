namespace TextRPG
{
    [Serializable]
    public class EndingCredit
    {
        //1. 상어의 의지, 고래의 은혜가 인벤토리에 있으며, 퀘스트 아이템 (진주)1개가 있으면 히든 스테이지8번 생성. 8번을 누르면 엔딩크래딧 보여주고 엔딩.
        Player player;
        bool isWhale = false;
        bool isShark = false;
        bool isPearl = true;

        public EndingCredit(Player player)
        {
            this.player = player;
        }


        public bool CheckEndLogic()
        {
            foreach( var item in player.Inventory)
            {
                if(item.itemName.Contains(" 고래의 은혜 "))
                    isWhale = true;
                if(item.itemName.Contains(" 상어의 의지 "))
                    isShark = true;
                if (item.itemName.Contains("진주"))
                    isPearl = true;
            }

            if(isWhale && isShark && isPearl)
            {
                return true;
            }
            return false;
        }

        public void EndingDisplay()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t           ___====-_  _-====___");
            Console.WriteLine("\t\t        _--^^^#####//      \\\\#####^^^--_");
            Console.WriteLine("\t\t     _-^##########// (    ) \\\\##########^-_");
            Console.WriteLine("\t\t    -############//  |\\^^/|  \\\\############-");
            Console.WriteLine("\t\t  _/############//   (@::@)   \\\\############\\_");
            Console.WriteLine("\t\t /#############((     \\\\//     ))#############\\");
            Console.WriteLine("\t\t-###############\\\\    (oo)    //###############-");
            Console.WriteLine("\t\t -##############\\\\  / '' \\\\  //##############-");
            Console.WriteLine("\t\t   -#############\\\\/  (__)  \\\\//#############-");
            Console.WriteLine("\t\t    _\\#############(   ''   )#############/_");
            Console.WriteLine("\t\t     -###############\\     //###############-");
            Console.WriteLine("\t\t       -#############\\   //#############-");
            Console.WriteLine("\t\t          -###########\\ //###########-");
            Console.WriteLine("\t\t             -#########\\//#########-");
            Console.WriteLine("\t\t                ^^^^^^^  ^^^^^^^");
            Console.ResetColor();

            Console.WriteLine("\n\t\t\t\t    용왕의 위엄이 느껴집니다!");
            Console.WriteLine("당신은 용왕의 축복을 받아..");
            Console.WriteLine("안녕히 계세요 여러분~ 전 이 세상의 모든 굴레와 속박을 벗어던지고 제 행복을 찾아 떠납니다~");

        }
    }
}