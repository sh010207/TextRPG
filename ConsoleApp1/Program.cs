namespace ConsoleApp1
{

    internal class GameManager
    {
        string playername;
        PlayerStatus playerStatus;
        Shop shop = new Shop();
        Job job = new Job();

        Inventory inventory = new Inventory();

        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.NameScene();
        }

        void Jobselect() //  직업 선택
        {
            Console.Clear();
            Console.WriteLine("직업을 선택해주세요.\n\n1.전사\n2.도적\n\n");
            Console.Write(">> ");
            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    Console.Clear();
                    job.worrior();
                    Console.WriteLine("전사를 선택하셨습니다.");
                    Console.WriteLine($" 바꾸시겠습니까? \n1.예 \n2. 아니오(선택시 진행)");
                    Console.Write(">> ");
                    string num = Console.ReadLine();
                    switch (num)
                    {
                        case "1":
                            Jobselect();
                            break;
                        case "2":
                            StartScene();
                            break;
                    }
                    break;
                case "2":
                    Console.Clear();
                    job.Thief();
                    Console.WriteLine("도적을 선택하셨습니다.");
                    Console.WriteLine($" 바꾸시겠습니까? \n1.예 \n2. 아니오(선택시 진행)");
                    Console.Write(">> ");
                    string num1 = Console.ReadLine();
                    switch (num1)
                    {
                        case "1":
                            Jobselect();
                            break;
                        case "2":
                            StartScene();
                            break;
                    }

                    break;
                default:
                    Console.WriteLine("잘못된입력입니다. 다시입력해주세요.\n>>");
                    select = Console.ReadLine();
                    break;
            }
        }

        void NameScene()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요.");
            Console.Write(">>");
            playername = Console.ReadLine();  // 플레이어가 쓸 이름 저장.
            Console.WriteLine($"입력하신 이름은{playername}입니다.");
            Console.WriteLine($"1.저장\n2.취소");
            Console.Write(">> ");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    playerStatus = new PlayerStatus(1, 1500);
                    Jobselect();
                    break;
                case "2":
                    NameScene();
                    break;
                default:
                    Console.WriteLine("다시 입력해주세요.");
                    input = Console.ReadLine();
                    break;
            }
        }

        void StartScene()
        {
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int selectNum = int.Parse(Console.ReadLine());
            switch (selectNum)
            {
                case 1:
                    Status();
                    break;
                case 2:
                    Inventory();
                    break;
                case 3:
                    ShopScene();
                    break;
                default:
                    Console.WriteLine("잘못된입력입니다. 다시 입력해주세요.");
                    selectNum = int.Parse(Console.ReadLine());
                    break;
            }
        }

        void Status()
        {
            Console.Clear();
            inventory.EquipitemStat(playerStatus);
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
             
            Console.WriteLine($"Lv. {playerStatus.Level}");
            Console.WriteLine($"{playername}({job.PlayerClass})");
            Console.WriteLine($"공격력: {job.Power} +({playerStatus.InvenItemStat_AT})");
            Console.WriteLine($"방어력: {job.Def} +({playerStatus.InvenItemStat_DF})");
            Console.WriteLine($"Hp: {job.Hp}");
            Console.WriteLine($"Gold: {playerStatus.Gold}");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.WriteLine  (">>");
            int num = int.Parse(Console.ReadLine());

            while (true)
            {
                if (num == 0)
                {
                    StartScene();
                }
            }
        }

        void Inventory()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 보유중인 아이템을 관리 할 수 있습니다.");
            Console.WriteLine("[아이템 목록]");
            inventory.Show();
            Console.WriteLine("\n1.장착관리 \n0.나가기");
            Console.Write(">>");
            int num = int.Parse(Console.ReadLine());
            while (true)
            {
                switch (num)
                {
                    case 0:
                        StartScene(); // 마을로
                        break;
                    case 1:
                        EquipManager();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                        num = int.Parse(Console.ReadLine());
                        break;

                }
            }
        }

        void EquipManager()
        {
            Console.Clear();
            Console.WriteLine("인벤토리\n보유중인 아이템을 관리 할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            inventory.SelectItem();
            Console.WriteLine("\n0.나가기\n(아이템목록의 번호를 누르면 장착) \n\n 원하시는 행동을 입력해주세요.\n");
            Console.Write(">>");
            while (true)
            {
                int num = int.Parse(Console.ReadLine());
                if (num == 0)
                {
                    StartScene();
                }
                else if (num >= 1 && num <= inventory.invenIndex)
                {
                    inventory.EquipItem(num - 1);
                    inventory.Show();
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력하세요.");
                    num = int.Parse(Console.ReadLine());
                }
            }
        }

        void ShopScene()
        {
            Console.Clear();
            Console.WriteLine("상점 - 필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유골드]");
            Console.WriteLine($"{playerStatus.Gold}G\n\n[아이템 목록]");
            shop.ShowShop();
            Console.WriteLine("\n1. 구매하기 0.나가기\n\n원하시는 행동 입력해주세요.");
            Console.Write(">>");
            int num = int.Parse(Console.ReadLine());
            while (true)
            {
                if (num == 0)
                {
                    StartScene();
                }
                else if (num == 1) 
                {
                    Purchase();
                }

                Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                num = int.Parse (Console.ReadLine()); 
            }
        }

        void Purchase()
        {
            Console.Clear();
            Console.WriteLine("상점 \n\n\n[보유골드]");
            Console.WriteLine($"{playerStatus.Gold}G\n\n[아이템 목록]");
            shop.PurchaseShop();

            Console.WriteLine("\n0.나가기\n\n원하시는 행동을 입력해주세요.\n");
            Console.Write(">> ");
            int num = int.Parse(Console.ReadLine());
            while (true)
            {
                if (num == 0)
                {
                    StartScene();
                }
                else if(num >= 1 && num <= shop.shopItemsCount)// 아이템 숫자 표기
                {
                    shop.PurchaseItem(num, playerStatus, inventory);
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 입력해주세요.");
                    num = int.Parse(Console.ReadLine());    
                }
                Console.WriteLine("\n\n\n1. 구매 계속\n0.나가기");
                num = int.Parse(Console.ReadLine());
                if(num == 1)
                {
                    Purchase();
                }
            }
        }
    }
}
