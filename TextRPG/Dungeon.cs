namespace TextRPG
{
    [Serializable]
    public class Dungeon
    {
        Monster monster;
        Player player;
        Random random = new Random();
        public int randomMonsterCount;
        public int potionCount = 3; // 기본 포션개수
        public List<Monster> spawnedMonsters = new List<Monster>(); //랜덤 소환된 몬스터(전투중인?)
        public Dungeon(Player player)
        {
            this.player = player;
        }
        // 몬스터 랜덤 생성
        public void RandMonsters(bool ShowIndex)
        {
            spawnedMonsters.Clear();
            randomMonsterCount = random.Next(1, 5); // 몬스터 등장 수
            List<Monster> monsters = new List<Monster>(); // 몬스터 리스트 생성
            {
                monsters.Add(new Monster(2, "빨판상어", 10, 5, 500, 50));
                monsters.Add(new Monster(5, "귀상어", 15, 7, 700, 70));
                monsters.Add(new Monster(7, "백상어", 25, 10, 1000, 100));
            }
            for (int i = 0; i < randomMonsterCount; i++)
            {
                // 랜덤하게 선택된 몬스터의 속성을 기반으로 새로운 몬스터 인스턴스 생성
                Monster selectedMonster = monsters[random.Next(monsters.Count)];
                Monster newMonster = new Monster(selectedMonster.Lv, selectedMonster.Name,
                    selectedMonster.Hp, selectedMonster.Atk, selectedMonster.Gold, selectedMonster.Exp);
                spawnedMonsters.Add(newMonster);
                string MonsterIndex = ShowIndex ? $"{i + 1}" : ""; // 몬스터의 인덱스를 표시
                Console.WriteLine($"\n\n\t\t\t\t\t\t{MonsterIndex}  Lv.{newMonster.Lv} {newMonster.Name}\n \t\t\t\t\t\t   HP: {newMonster.Hp}\n\n");// 생성된 몬스터 출력
            }
        }
        public void StartBattle()
        {
            while (player.hp > 0 && spawnedMonsters.Any(monster => monster.Hp > 0))
            {
                int num = GameManager.SelectBehavior(1, randomMonsterCount + 1);
                if (num == spawnedMonsters.Count + 1)
                {
                    UsePotion(player.job);
                    Console.Clear();
                    ShowMonsters();
                    PlayerStat(true);
                    int numAttack = GameManager.SelectBehavior(1, spawnedMonsters.Count);
                    AttackMonsters(numAttack);
                }
                else { AttackMonsters(num); }
                if (spawnedMonsters.All(monster => monster.Hp == 0))
                {
                    break;
                }
                EnemyPhase();
            }
        }
        //생성된 몬스터 보여주기(출력)
        public void ShowMonsters() //isDead?
        {
            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                //if (selectMonIndex > 0 && selectMonIndex < spawnedMonsters.Count)
                //{
                string isDeadTxt = spawnedMonsters[i].Hp == 0 ? "Dead" : $"{spawnedMonsters[i].Hp}";
                Console.WriteLine($"\n\n\t\t\t\t\t\t{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n \t\t\t\t\t\t   HP: {isDeadTxt}\n\n");
                //}
                //else
                //{
                //    Console.WriteLine($"{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n HP: {spawnedMonsters[i].Hp}\n\n");
                //}
            }
        }
        //공격 후 몬스터 체력감소
        public void AttackMonsters(int attackMonsterNum)
        {
            int attackRange = random.Next(player.ad * 9 / 10, player.ad * 11 / 10); //10%오차
            if (!(spawnedMonsters[attackMonsterNum - 1].Hp == 0)) //입력은 1-4이고, 배열은 0부터여서 -1 참조오류 해결
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n\n       \t\t             O===[==============================================>\r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t            ||                     \t\t\t\t||\r\n       \t\t            ||     BBBB   AAAAA  TTTTT  TTTTT  L      EEEEE     ||\r\n       \t\t            ||     B   B  A   A    T      T    L      E         ||\r\n       \t\t            ||     BBBB   AAAAA    T      T    L      EEEE      ||\r\n       \t\t            ||     B   B  A   A    T      T    L      E         ||\r\n       \t\t            ||     BBBB   A   A    T      T    LLLLL  EEEEE     ||\r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t             O===[==============================================>\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\n\n\n\t\t\t\t\t       나, {player.name}의 공격!\n\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\t\t\t\t  Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name} 을(를) 맞췄습니다.  [ 데미지 : {attackRange} ]");
                Console.WriteLine($"\n\t\t\t\t\t\t    Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name}");
                spawnedMonsters[attackMonsterNum - 1].Hp -= attackRange;
                Console.Write($"\t\t\t\t\t\t   HP {spawnedMonsters[attackMonsterNum - 1].Hp + attackRange} ");
                Console.WriteLine($"-> {spawnedMonsters[attackMonsterNum - 1].Hp}\n");
                Console.ResetColor();
                //몬스터 체력이 0보다 작으면 0으로
                if (spawnedMonsters[attackMonsterNum - 1].Hp <= 0)
                {
                    spawnedMonsters[attackMonsterNum - 1].Hp = 0;
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\n\t\t\t\t\t     이런, 허공을 때렸습니다.\n\t\t\t\t\t    이미 그 친구는 죽었습니다..\n \t\t\t\t\t       맞기만 해야하네요..\n\n");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t\t\t\t\t0");
            Console.ResetColor();
            Console.WriteLine("을 눌러 다음 전투를 확인하세요.\n");
            int num = GameManager.SelectBehavior(0, 0);
            switch (num)
            {
                case 0:
                    Console.Clear();
                    //ShowMonsters();
                    break;
            }
        }
        //적 -> 공격 차례 로직
        public void EnemyPhase()
        {
            if (player.hp <= 0)
            {
                player.hp = 0;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\t\t\t\t            L      OOOOO  SSSSS  EEEEE\r\n\t\t\t\t            L     O     O S      E    \r\n\t\t\t\t            L     O     O  SSS   EEEE \r\n\t\t\t\t            L     O     O     S  E    \r\n\t\t\t\t            LLLLL  OOOOO  SSSSS  EEEEE\r\n\n\n");
                Console.ResetColor();
                // 죽었을때 메인
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n\n\t\t\t\t\t       {player.name}님이 사망하였습니다.\n\n");
                Console.WriteLine($"\n\n\t\t\t\t\t\t     Lv.{player.level} {player.job}");
                Console.WriteLine($"\n\t\t\t\t\t\t     HP {player.maxhp} -> {player.hp}\n\n");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("\t\t\t\t\t   0");
                Console.ResetColor();
                Console.WriteLine("을 누르면 메인화면으로 돌아갑니다.\n");
                player.ResetHp();
            }
            else
            {
                //몬스터들이 차례대로 공격
                for (int i = 0; i < spawnedMonsters.Count; i++)
                {
                    Monster currentMonster = spawnedMonsters[i];
                    if (currentMonster.Hp > 0)
                    {
                        //플레이어 체력 감소 로직
                        player.hp -= currentMonster.Atk;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n\n       \t\t             O===[==============================================>\r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t            ||                     \t\t\t\t||\r\n       \t\t            ||     BBBB   AAAAA  TTTTT  TTTTT  L      EEEEE     ||\r\n       \t\t            ||     B   B  A   A    T      T    L      E         ||\r\n       \t\t            ||     BBBB   AAAAA    T      T    L      EEEE      ||\r\n       \t\t            ||     B   B  A   A    T      T    L      E         ||\r\n       \t\t            ||     BBBB   A   A    T      T    LLLLL  EEEEE     ||\r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t            ||                     \t\t\t\t|| \r\n       \t\t             O===[==============================================>\n\n");
                        Console.ResetColor();
                        Console.ForegroundColor= ConsoleColor.Magenta;
                        Console.WriteLine("\t\t\t\t\t    우쒸..이제 우리들 차례다!!!!");
                        Console.WriteLine($"\t\t\t\t\t         Lv.{currentMonster.Lv} {currentMonster.Name}의 공격!\n\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine($"\t\t\t\t\t {player.name}을(를) 맞췄습니다.  [데미지 : {currentMonster.Atk}]\n");
                        Console.WriteLine($"\n\t\t\t\t\t\t      Lv.{ player.level} {player.name}");
                        Console.WriteLine($"\t\t\t\t\t\t   HP {player.hp + currentMonster.Atk} -> {player.hp}\n");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write("\t\t\t\t\t   0");
                        Console.ResetColor();
                        Console.WriteLine("을 눌러 다음 전투를 확인하세요.\n");
                        //Console.WriteLine("대상을 선택해주세요.");
                        int num0 = GameManager.SelectBehavior(0, 0);
                        if (num0 == 0)
                        {
                            Console.Clear();
                        }
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            //ShowMonsters();
            //PlayerStat(true);
            //int num = SelectBehavior(spawnedMonsters.Count + 1, spawnedMonsters.Count + 1);
            //if (num == spawnedMonsters.Count + 1)
            //{
            //    UsePotion(player.job);
            //}
            Console.Clear();
            ShowMonsters();
            PlayerStat(true);
        }
        public void UsePotion(string job)
        {
            int potionheal = 50;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\t\t\t\t     H   H  EEEEE  AAAAA  L      TTTTT  H   H  Y   Y\r\n\t\t\t\t     H   H  E      A   A  L        T    H   H   Y Y \r\n\t\t\t\t     HHHHH  EEEE   AAAAA  L        T    HHHHH    Y  \r\n\t\t\t\t     H   H  E      A   A  L        T    H   H    Y  \r\n\t\t\t\t     H   H  EEEEE  A   A  LLLLL    T    H   H    Y  \r\n");
            Console.ResetColor();
            if (potionCount > 0)
            {
                if (player.hp >= player.maxhp)
                {

                    Console.Write("\n\n\t\t\t\t\t\t이미 ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.Write("최대 체력 ");
                    Console.ResetColor();
                    Console.WriteLine("입니다. \n");
                    Console.Write("\t\t\t\t\t\t 남은 ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("포션 ");
                    Console.ResetColor();
                    Console.Write("갯수 : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{potionCount}");
                    Console.ResetColor();
                    
                }
                else
                {
                    if (player.job == "우파루파") potionheal = 100; //우파루파 특성
                    player.hp += potionheal;
                    potionCount -= 1;
                    Console.Write("\n\t\t\t\t\t\t 남은 ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("포션 ");
                    Console.ResetColor();
                    Console.Write("갯수 : ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{potionCount}");
                    Console.ResetColor();
                    if (player.hp >= player.maxhp) player.hp = player.maxhp;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n\n\t\t\t\t\t    === 회복을 완료했습니다. ===");
                    Console.ResetColor();
                    Console.Write("\n\t\t\t\t\t\t 현재체력 : ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write($"{player.hp}");
                    Console.ResetColor();
                    Console.WriteLine($" / {player.maxhp}");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("\n\n\n\t\t\t\t\t      === 포션이 없습니다 === \n");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\t\t\t\t\t            0 . 나가기");
            Console.WriteLine("\n\t\t\t\t\t\t1 . 포션 하나 더먹기\n");
            int num = GameManager.SelectBehavior(0, 1);
            switch (num)
            {
                case 0:
                    return;
                case 1:
                    UsePotion(job);
                    break;
            }
        }
        //플레이어 정보 출력
        public void PlayerStat(bool isFight) //////////////////////
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\t\t\t\t\t  [ 내정보 ]\n");
            Console.WriteLine($"\t\t\t\t\t\t     {player.name}");
            Console.WriteLine($"\t\t\t\t\t\tLv.{player.level:D2} / {player.job}");
            Console.WriteLine($"\t\t\t\t\t\t   HP {player.hp}/{player.maxhp}\n\n");
            Console.ResetColor();
            //string press1 = isFight == true ? $"\t\t\t공격할 몬스터를 입력해주세요.\n{spawnedMonsters.Count + 1}. 체력포션" : "1. 공격\n\n원하시는 행동을 입력해주세요.";
            if (isFight == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\t\t\t\t\t     {spawnedMonsters.Count + 1}개의 체력포션");
                Console.ResetColor();
                Console.WriteLine("이 있습니다.\n");
                Console.WriteLine("\t\t\t\t        >((('>  먹는방법을 알려드리죠  <')))< \n");
                Console.Write("\t\t\t\t           몬스터 수 + ");
                Console.ForegroundColor = (ConsoleColor)ConsoleColor.Magenta;
                Console.Write("1");
                Console.ResetColor ();
                Console.Write("번을 눌러주세요.\n\n");
                Console.ForegroundColor = (ConsoleColor)ConsoleColor.Cyan;
                Console.WriteLine("\t\t\t\t      공격 하시고 싶은 대상을 번호로 눌러주세요.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)ConsoleColor.Cyan;
                Console.WriteLine("\t\t\t\t\t\t    1. 공격\n");
                Console.ResetColor();
                Console.WriteLine("\n\n\n\t\t\t\t           원하시는 행동을 입력해주세요.");
            }
            //Console.WriteLine($"{press1}");
            
        }

    }
}