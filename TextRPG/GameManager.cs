using System.Diagnostics;
using System.Numerics;

namespace TextRPG
{
    // 아이템을 끼면 공격력 + 
    // 아이템을 끼면 맨앞에 E
    // 아이템을 사 << 샵?
    // 아이템을 착용
    internal class GameManager
    {
        static Player player;
        static Monster monster;
        private static Item[] itemDb;
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
            Console.WriteLine("1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 전투 시작\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(1, 4);

            switch (num)
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
                    DungeonUI();
                    break;

            }
        }

        //1-3, 0 선택하기
        static int SelectBehavior(int min, int max)
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
            List<Item> items = new List<Item>(); // 아이템 리스트 생성
            {
                items.Add(new Item("수련자의 갑옷", 1, 5, "수련에 도움을 주는 갑옷입니다. ", 1000));
                items.Add(new Item("무쇠갑옷", 1, 9, "무쇠로 만들어져 튼튼한 갑옷입니다. ", 2000));
                items.Add(new Item("스파르타의 갑옷", 1, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다. ", 3500));
                items.Add(new Item("낡은 검", 0, 2, "쉽게 볼 수 있는 낡은 검 입니다. ", 600));
                items.Add(new Item("청동 도끼", 0, 5, "어디선가 사용됐던거 같은 도끼입니다. ", 1500));
                items.Add(new Item("스파르타의 창", 0, 7, "스파르타의 전사들이 사용했다는 전설의 창입니다. ", 2500));
            }
        }
        //인벤토리
        static void InventoryUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]\n");

            player.ShowInventory(false);  // 아이템 목록 표시 ( 인덱스 x)

            Console.WriteLine("1. 장착 관리\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int num = SelectBehavior(0, 1);

            switch (num)
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
            int num = SelectBehavior(0, 1);

            switch (num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    PurchaseItem();
                    break;
            }

        }

        //인벤토리 - 장착 관리
        static void EquipManagementUI()
        {
            Console.Clear();
            List<Item> items = new List<Item>();    
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");
            Console.WriteLine("[아이템 목록]");

            player.ShowInventory(true); // 아이템 목록 표시 ( 인덱스 o 누르면 장착);
            
            Console.WriteLine("0. 나가기\n원하시는 행동을 입력해주세요.");
            int num = SelectBehavior(0, player.InventoryCount); //여기 나중에 (0,list명.count)로 변경

            switch (num)
            {
                case 0:
                    GameStartUI();
                    break;
                default:
                    int itemIndex = num - 1;
                    Item item = items[itemIndex];
                    player.EquipItem(item);
                    break;
            }
        }

        //아이템구매
        static void PurchaseItem()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
        static void RandMonsters(bool ShowIndex) // 몬스터 랜덤 생성
        {
            List<Monster> monsters = new List<Monster>(); // 몬스터 리스트 생성
            {
                monsters.Add(new Monster(2, "슬라임", 10, 5));
                monsters.Add(new Monster(5, "고블린", 15, 7));
                monsters.Add(new Monster(7, "오크", 25, 10));
            }


            Random randCount = new Random();
            int randomMonsterCount = randCount.Next(1, 4); // 몬스터 등장 수
            for (int i = 0; i < randomMonsterCount; i++)
            {
                monster = monsters[i];
                string MonsterIndex = ShowIndex ? $"{i + 1}" : ""; // 몬스터의 인덱스를 표시
                Console.WriteLine($"{MonsterIndex} Lv.{monster.Lv} {monster.Name}\n HP: {monster.Hp}\n\n");// 생성된 몬스터 출력
            }
        }

        //전투 시작
        static void DungeonUI()
        {

            Dungeon dungeon = new Dungeon();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            RandMonsters(false);
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
                    break;
            }
        }
        static void AtkUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            RandMonsters(true);// 몬스터 랜덤 생성

            //몬스터 정보 입력하고
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/100\n");
            Console.WriteLine("1. 공격\n\n원하시는 행동을 입력해주세요.");

            int num = SelectBehavior(0, 4);
            switch (num)
            {
                case 0:
                    GameStartUI();
                    break;
                case 1:
                    // 공격 기능
                    break;
                case 2:
                    // 공격 기능
                    break;
                case 3:
                    // 공격 기능
                    break;
                case 4:
                    // 공격 기능
                    break;
            }
        }
        static void Main(string[] args)
        {
            player = new Player("", 1, "전사", 10, 5, 100, 10000); //초기세팅
            CreateName();
            GameStartUI();

        }
    }
}
