using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace TextRPG
{
    internal class GameManager
    {
        static Player player;
        static Monster monster;
        private static Item[] itemDb;
        static BattleResult battleResult;
        static Dungeon dungeon;
        static Shop shop; ////Cha 상점 클래스
        static int potionCount = 3; // 기본 포션개수
        public static List<Item> items;
        static Inventory inventory;
        //이름 생성 화면

        static void CreateName()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine(" \t\t\t < 바닷속 어딘가, 용궁에 한 마을에 도착한다. >");
            Console.WriteLine("이름을 지어주세요");
            player.name = Console.ReadLine();

            battleResult = new BattleResult(player,dungeon);
            shop = new Shop(player);
            inventory = new Inventory(player);
            SetData();
        }
        //직업 선택
        static void SetClass() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\t\t\t [ 당신은 무슨 물고기가 되고 싶으신가요? ] \n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("1. 개복치 : HP가 낮고 방어력도 약하지만, 강력한 공격력을 자랑합니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("2. 망둥어 : 공격력과 방어력 모두 균형 잡힌 캐릭터로, 상황에 맞춰 유연하게 대처할 수 있습니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("3. 블롭피쉬 : 방어력과 체력이 뛰어나 적의 공격을 오랫동안 버티는 탱커형 캐릭터입니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("4. 우파루파 : 회복 능력이 뛰어난 서포터형 캐릭터입니다.\n");
            Console.ResetColor();

            int num = SelectBehavior(1, 4);
            switch (num)
            {
                case 1:
                    player.ChangeJob("개복치", 20, 5, 50, 50, 1000);
                    break;
                case 2:
                    player.ChangeJob("망둥어", 15, 12, 80, 80, 1200);
                    break;
                case 3:
                    player.ChangeJob("블롭피쉬", 5, 20, 150, 150, 800);
                    break;
                case 4:
                    player.ChangeJob("우파루파", 10, 10, 120, 120, 1500);
                    potionCount += 3;
                    break;
            }
        }

        //게임 시작 화면
        public static void GameStartUI()  ////cha 접근제한때문에  public. 로 변경
        {
            Console.Clear();
            Console.WriteLine($"{player.name}님은 이제 전투를 시작할 수 있습니다.\n"); ////cha UI깔끔하게 하기위해 엔터
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 전투 시작\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(1, 4);

            switch (num)
            {
                case 1:
                    CharacterInfoUI();
                    break;
                case 2:
                    inventory.DisplayInventoryUI();
                    break;
                case 3:
                    shop.DisplayShopUI();
                    break;
                case 4:
                    DungeonUI();
                    break;



            }
        }

        //1-3, 0 선택하기 
        public static int SelectBehavior(int min, int max)  ////cha 접근제한때문에  public. 로 변경
        {
            while (true)
            {
                string input = Console.ReadLine();
                int num;
                //TryParse로 문자열 -> 정수로 변환 / 성공시 true, 실패시 false
                if (int.TryParse(input, out num))
                {
                    if (num >= min && num <= max)
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

            player.PlayerInfo(); //캐릭터 정보 표시

            Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(0, 0);

            if (num == 0)
                GameStartUI();
        }
        // 아이템
        static void SetData()
        {
            items = new List<Item>(); // 아이템 리스트 생성
            {
                items.Add(new Item("수련자의 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다. ", 1000));
                items.Add(new Item("무쇠갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다. ", 2000));
                items.Add(new Item("스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ", 3500));
                items.Add(new Item("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검 입니다. ", 600));
                items.Add(new Item("청동 도끼", 0, 5, "어디선가 사용됐던거 같은 도끼입니다. ", 1500));
                items.Add(new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다. ", 2500));
            }
        }
        //인벤토리 >> 삭제 >> Inventory클래스로 이동

        //상점 >> 삭제 >> Shop 클래스로 이동

        //인벤토리 - 장착 관리 >> 삭제 >> Inventory클래스로 이동

        //아이템구매 >> 삭제 >> Shop 클래스로 이동
        
        // 포션 
        static void UsePotion(string job)
        {
            int potionheal = 50;
            Console.Clear();
            if (potionCount > 0)
            {
                if (player.hp >= player.maxhp)
                {
                    Console.WriteLine("이미 최대 체력입니다");
                    Console.WriteLine($"남은 포션 갯수 : {potionCount}");
                }
                else
                {
                    if (player.job == "우파루파") potionheal = 100; 
                    player.hp += potionheal;
                    potionCount -= 1;
                    Console.WriteLine($"남은 포션 갯수 : {potionCount}");

                    if (player.hp >= player.maxhp)
                    {
                        player.hp = player.maxhp;
                    }

                    Console.WriteLine($"회복을 완료했습니다. 현재체력 : {player.hp}/{player.maxhp}");
                }

            }
            else
            {
                Console.WriteLine("포션이 없습니다");
            }
            Console.WriteLine("0 . 나가기");
            Console.WriteLine("1 . 포션 하나 더먹기");
            int num = SelectBehavior(0, 1);
            switch (num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    UsePotion(job);
                    break;
            }


        }

        //전투 시작
        static void DungeonUI()
        {

            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            dungeon.RandMonsters(false);
            //몬스터 정보 입력하고
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/100\n");
            Console.WriteLine("1. 공격\n\n원하시는 행동을 입력해주세요.");

            int num = SelectBehavior(0, 1);
            switch (num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    AtkUI();
                    ending();
                    break;
            }
        }

        //공격
        static void AtkUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            dungeon.ShowMonsters(); //랜덤 몬스터 바뀌는 값 수정

            //몬스터 정보 입력하고
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/100\n");
            Console.WriteLine("1. 공격\n\n원하시는 행동을 입력해주세요.");

            dungeon.StartBattle();
            //int attackNum = SelectBehavior(1, dungeon.randomMonsterCount);
            //switch (attackNum)
            //{
            //    case 1:
            //        dungeon.AttackMonsters(attackNum);
            //        break;
            //    case 2:
            //        dungeon.AttackMonsters(attackNum);

            //        break;
            //    case 3:
            //        dungeon.AttackMonsters(attackNum);

            //        break;
            //    case 4:
            //        dungeon.AttackMonsters(attackNum);

            //        break;
            //}
        }



        //게임 종료 결과 화면
        static void ending()
        {
            battleResult.GameEndLogic();
            int num = SelectBehavior(0, 0);

            if (num == 0)
                GameStartUI();
        }
        static void Main(string[] args)
        {
            player = new Player("", 1, "전사", 10, 5, 100, 100, 10000); //초기세팅
            dungeon = new Dungeon(player);
            CreateName();
            SetClass();
            GameStartUI();

        }
    }
}
