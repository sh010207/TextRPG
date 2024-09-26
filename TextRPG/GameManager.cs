﻿namespace TextRPG
{
    internal class GameManager
    {
        static Player player;

        //이름 생성 화면
        static void CreateName()
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이름을 지어주세요");
            player.name = Console.ReadLine();
        }

        //게임 시작 화면
        static void GameStartUI()
        {
            Console.Clear();
            Console.WriteLine($"{player.name}님은 이제 전투를 시작할 수 있습니다.");
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 전투\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(1,4);

            switch(num)
            {
                case 1:
                    CharacterInfoUI();
                    break;
                case 2:
                    InventoryUI();
                    break;
                case 3:
                    ShopUI();
                    break;
                case 4:
                    Dungeon();
                    break;

            }
        }

        //1-3, 0 선택하기
        static int SelectBehavior(int min, int max)
        {
            while(true)
            {
                string input = Console.ReadLine();
                int num;
                //TryParse로 문자열 -> 정수로 변환 / 성공시 true, 실패시 false
                if (int.TryParse(input, out num))
                {
                    if(num >= min && num <= max)
                    {
                        return num;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red; //색 바꾸기
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor(); //색 초기화
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; //색 바꾸기
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor(); //색 초기화
                }
            }
            
        }

        //상태보기
        static void CharacterInfoUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            Console.WriteLine($"Lv. {player.level:D2}");
            Console.WriteLine($"Job {player.job}");
            Console.WriteLine($"공격력 {player.ad}");
            Console.WriteLine($"방어력 {player.df}");
            Console.WriteLine($"체 력 {player.hp}");
            Console.WriteLine($"Gold {player.gold}");

            Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(0,0);

            if(num == 0)
                GameStartUI();
        }

        //인벤토리
        static void InventoryUI()
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");
            Console.WriteLine("1. 장착 관리\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int num = SelectBehavior(0, 1);

            switch(num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    EquipManagementUI();
                    break;
            }
        }

        //상점
        static void ShopUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G");
            Console.WriteLine("\n[아이템 목록]");
            //아이템목록


            Console.WriteLine("1. 아이템 구매\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(0,1);

            switch(num)
            {
                case 0:
                    PurchaseItem();
                    break;
                case 1:
                    GameStartUI();
                    break;
            }

        }

        //인벤토리 - 장착 관리
        static void EquipManagementUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");
            //인벤토리 불러오기
            Console.WriteLine("0. 나가기\n원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(0, 3); //여기 나중에 (0,list명.count)로 변경

            switch(num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    //무기장착
                    break;
                case 2:
                    //무기장착2
                    break;
                case 3:
                    //무기장착3
                    break;
            }

        }

        static void PurchaseItem()
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold}\n");
            Console.WriteLine("[아이템 목록]");
            //상점아이템목록
            Console.WriteLine("0. 나가기\n\n원하시는 행동을 입력해주세요.");

            int num = SelectBehavior(0, 0);
            if (num == 0)
                GameStartUI();

        }

        static void Dungeon()
        {
            Console.Clear();
        }


        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            player = new Player("", 1, "전사", 10, 5, 100, 10000); //초기세팅
            CreateName();
            GameStartUI();

        }
    }
}
