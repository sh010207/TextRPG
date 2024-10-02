using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
namespace TextRPG
{
    [Serializable]
    internal class GameManager
    {
        static Player player;
        static Monster monster;
        private static Item[] itemDb;
        static BattleResult battleResult;
        static Dungeon dungeon;
        static Shop shop;
        public static List<Item> items;
        static Inventory inventory;
        static Quest quest = new Quest();
        static GameSaveFunction saveFunction;
        static EndingCredit endingCredit;
        static string saveFilePath = "game_save.json"; /////Json파일 생성

        static void GameLoad() // 저장한 게임 가져오기 기능
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\t\t\t ><(((('>   바닷속 어딘가, 용궁에 한 마을에 도착한다.   <'))))><\n\n\n\n\n");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t\t1. 여기 와본적이 있는거 같다.\n\n\n");
            Console.Write("\t\t\t\t\t     2. 처음 와본거 같다.\n\n\n ");

            int num = SelectBehavior(1, 2);
            switch (num)
            {
                case 1:
                    Player loadedGameData = GameSaveFunction.LoadGame(saveFilePath);

                    if (loadedGameData != null)
                    {
                        player = loadedGameData;
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\t\t\t\t      이름 : {loadedGameData.name}, 레벨 : {loadedGameData.level}, 직업 : {loadedGameData.job} ");
                        Console.ResetColor();
                        Console.WriteLine("\n\t\t\t\t\t       아무키나 눌러주세유");
                        Console.ReadKey(); // 한글자 입력*

                        battleResult = new BattleResult(player,dungeon);
                        shop = new Shop(player);
                        inventory = new Inventory(player);
                        saveFunction = new GameSaveFunction(player);
                        SetData();
                        SetData();
                        GameStartUI();
                    }

                    break;
                case 2:
                    CreateName();
                    break;

            }
        }

        //이름 생성 화면
        static void CreateName()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\n\t\t><(((('>    그렇군요.. 처음 오신만큼 이 마을의 가호가 깃들길 바라겠습니다.   <'))))><\n\n\n\n\n");
            Console.ResetColor();
            Console.Write("\t\t\t\t\t용사님의 이름은 무엇인가요?   \n\n");
            Console.Write("\t\t\t\t\t\t    ");
            player.name = Console.ReadLine();

            battleResult = new BattleResult(player, dungeon);
            shop = new Shop(player);
            inventory = new Inventory(player);
            saveFunction = new GameSaveFunction(player);
            SetData();
            SetClass();
            GameStartUI();
        }
        //직업 선택
        static void SetClass()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\t\t\t  ><(((°>   당신은 무슨 물고기가 되고 싶으신가요?   <°)))><\n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t  1. 개복치 : HP가 낮고 방어력도 약하지만, 강력한 공격력을 자랑합니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t  2. 망둥어 : 공격력과 방어력 모두 균형 잡힌 캐릭터로, 상황에 맞춰 유연하게 대처할 수 있습니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\t\t  3. 블롭피쉬 : 방어력과 체력이 뛰어나 적의 공격을 오랫동안 버티는 탱커형 캐릭터입니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t  4. 우파루파 : 회복 능력이 뛰어난 서포터형 캐릭터입니다.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\t\t\t\t\t     용사님의 선택은?? \n");
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
                    player.ChangeJob("우파루파", 10, 10, 120, 120, 15000);
                    dungeon.potionCount += 3;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red; //색 바꾸기
                    Console.WriteLine("\t\t\t\t\t\t잘못된 입력입니다.\n");
                    Console.ResetColor(); //색 초기화
                    break;
            }
        }
        //게임 시작 화면
        public static void GameStartUI()  ////cha 접근제한때문에  public. 로 변경
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n\t\t     ><(((('>   {player.name} ( {player.job} ) 님, 용궁마을에 오신 것을 환영합니다.   <'))))><\n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t\t\t  ■■■■■■■■■■■■\t\t ><(('>\n\t\t\t\t\t  ■\t\t        ■");
            Console.Write(" \t\t\t\t\t  ■    ");
            Console.ResetColor();
            Console.Write("1. 상태 보기    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\n\t\t\t         <')))><  ■    ");
            Console.ResetColor();
            Console.Write("2. 가     방    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\t\t\t><((((('>\n\t<°)))><\t\t\t  ■    ");
            Console.ResetColor();
            Console.Write("3. 상     점    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\n\t\t\t><(('>\t\t  ■    ");
            Console.ResetColor();
            Console.Write("4. 던전 입장    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\t<')))><\n\t\t\t\t\t  ■    ");
            Console.ResetColor();
            Console.Write("5.  퀘스트      ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\n\t\t                   \t  ■    ");
            Console.ResetColor();
            Console.Write("6. 게임 저장    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\n\t\t            ><((('>\t  ■    ");
            Console.ResetColor();
            Console.Write("7. 게임 종료    ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("■\t     ><(((('> \n\t\t\t\t\t  ■\t\t        ■\n \t\t\t\t\t  ■■■■■■■■■■■■\n");
            Console.ResetColor();
            if (endingCredit.CheckEndLogic())
            {
                Console.Write("8. 용궁 입장    ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("■\n\t\t            ><((('>\t  ■    ");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\t\t\t\t         원하시는 행동을 입력해주세요.  \n\n");
            int num = endingCredit.CheckEndLogic() == true ? SelectBehavior(1, 8) : SelectBehavior(1, 7);
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
                case 6:
                    GameSaveFunction.SaveGame(player, saveFilePath);
                    GameStartUI();
                    quest.QuestUI();
                    break;
                case 7:
                    Console.ForegroundColor= ConsoleColor.Cyan;
                    Console.WriteLine("\t\t\t     ><((º> 용궁에서의 기억은 좋으셨나요? 또 만나요 <º))><");
                    Console.ResetColor();
                    break;
                case 8:
                    endingCredit.EndingDisplay();
                    break;
            }
        }
        //1-3, 0 선택하기
        public static int SelectBehavior(int min, int max)  ////cha 접근제한때문에  public. 로 변경
        {
            while (true)
            {
                Console.Write("\t\t\t\t\t\t      ");
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
                        Console.WriteLine("\t\t\t\t\t\t잘못된 입력입니다.\n");
                        Console.ResetColor(); //색 초기화
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; //색 바꾸기
                    Console.WriteLine("\t\t\t\t\t\t잘못된 입력입니다.\n");
                    Console.ResetColor(); //색 초기화
                }
            }
        }
        //상태보기
        static void CharacterInfoUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\t\t\t\t\t   SSSSS  TTTTT  AAAAA  TTTTT  \r\n \t\t\t\t\t  S         T    A   A    T    \r\n\t\t\t\t\t   SSS      T    AAAAA    T    \r\n\t\t\t\t\t       S    T    A   A    T    \r\n\t\t\t\t\t  SSSSS     T    A   A    T    \r\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\t\t\t\t\t\t[ 상태 보기 ]\n");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t\t 용사님의 정보가 표시됩니다.\n");

            player.PlayerInfo(); //캐릭터 정보 표시

            Console.Write("\n\t\t\t\t\t\t  0. 나가기\n\n\t\t\t\t\t 원하시는 행동을 입력해주세요.\n\n");
            int num = SelectBehavior(0, 0);
            if (num == 0)
                GameStartUI();
        }
        // 아이템
        static void SetData()
        {
            items = new List<Item>(); // 아이템 리스트 생성
            {
                items.Add(new Item(" 고등어 비늘 ", 1, 05, "       반짝반짝하니까..천적을 조심하세요       ", 1000));
                items.Add(new Item("물방울 보호막", 1, 09, "        물방울이어도 나름 튼튼합니다.          ", 2000));
                items.Add(new Item(" 고래의 은혜 ", 1, 15, "고래의 은혜를 받아 만들어진 전설의 갑옷입니다. ", 3500));
                items.Add(new Item("    복어탄   ", 0, 02, "    복어가 뾰족하기도 하지만 독도 있답니다.    ", 600));
                items.Add(new Item("  오징어 활  ", 0, 05, "    오징어 발이 10개라 화살 수도 10개라구요!   ", 1500));
                items.Add(new Item(" 상어의 의지 ", 0, 07, "      상어의 의지가 담긴 전설의 창입니다.      ", 2500));
            }
        }
       //던전UI
        static void DungeonUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            dungeon.RandMonsters(false);
            //몬스터 정보 입력하고
            Console.WriteLine("[내정보]");
            Console.WriteLine(player.name);
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/{player.maxhp}\n");
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
            Console.WriteLine($"{player.name}");
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/{player.maxhp}\n");
            Console.WriteLine("1. 공격");
            Console.WriteLine($"{dungeon.spawnedMonsters.Count + 1} 체력 포션 \n\n원하시는 행동을 입력해주세요.");
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
            player = new Player("", 1, "일반물고기", 10, 5, 100, 100, 0, 10000); //초기세팅
            dungeon = new Dungeon(player);
            endingCredit = new EndingCredit(player);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n\n\t\t\t                  <°))><    _----|   ><>   _ _ _ _ _\r\n\t\t\t            >((('>            ----|_----|   ]-I-I-I-[\r\n\t\t\t          _ _ _ _ _ _ _----|      | ----|   \\ `  ' /\r\n\t\t\t          ]-I-I-I-I-[  ----|      |     |    |. ` | <')))>< \r\n\t\t\t           \\ `   '_/       |     / \\    |    | /^\\|\r\n\t\t\t            []  `__|  ><>  ^    / ^ \\   ^    | |*||\r\n\t\t\t            |__   ,|      / \\  / ^ ^`\\ / \\   | ===|\r\n\t\t\t ><(('> ___| ___ ,|__   / ^  /=_=_=_=\\ ^ \\  |, `_|\r\n\t\t\t         I_I__I_I__I_I  (====(_________)_^___|____|____\r\n\t\t\t         \\-\\--|-|--/-/  |     I  [ ]__I I_I__|____I_I_|\r\n\t\t\t          |[] `    '|_  |_   _|`__  ._[  _-\\--|-|--/-/\r\n\t\t\t         / \\  [] ` .| |-| |-| |_| |_| |_| | []   [] |\r\n\t\t\t        <===>      .|-=-=-=-=-=-=-=-=-=-=-|        / \\   ><('>\r\n\t\t\t        ] []|` ` [] | .   _________   .   |-      <===>  \r\n\t\t\t        <===>  `  ' ||||  |       |  |||  |  []   <===>\r\n\t\t\t         \\_/     -- ||||  |       |  |||  | .  '   \\_/\r\n\t\t\t        ./|' . . . .|||||/|_______|\\|||| /|. . . . .|\\_\r\n\t\t\t    --------------------------------------------------------");
            Console.WriteLine("\n\n\t\t\t         끝없는 바다 아래, 그 곳에서 새로운 모험이 시작된다. \n\n\t\t\t\t\t     아무 키나 눌러 시작하세요!");
            Console.ReadKey();
            Console.ResetColor();
            GameLoad();
        }
    }
}