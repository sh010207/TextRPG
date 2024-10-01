namespace TextRPG
{
    public class Dungeon
    {
        Monster monster;
        Player player;
        Random random = new Random();
        public int randomMonsterCount;
        int selectMonIndex = 10;


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
                monsters.Add(new Monster(2, "슬라임", 10, 5,5));
                monsters.Add(new Monster(5, "고블린", 15, 7, 7));
                monsters.Add(new Monster(7, "오크", 25, 10, 10));
            }


            for (int i = 0; i < randomMonsterCount; i++)
            {
                // 랜덤하게 선택된 몬스터의 속성을 기반으로 새로운 몬스터 인스턴스 생성
                Monster selectedMonster = monsters[random.Next(monsters.Count)];
                Monster newMonster = new Monster(selectedMonster.Lv, selectedMonster.Name,
                    selectedMonster.Hp, selectedMonster.Atk, selectedMonster.Exp);

                spawnedMonsters.Add(newMonster);

                string MonsterIndex = ShowIndex ? $"{i + 1}" : ""; // 몬스터의 인덱스를 표시
                Console.WriteLine($"{MonsterIndex}  Lv.{newMonster.Lv} {newMonster.Name}\n HP: {newMonster.Hp}\n\n");// 생성된 몬스터 출력
            }
        }

        public void StartBattle()
        {
            while(player.hp >= 0 && spawnedMonsters.Any(monster => monster.Hp > 0))
            {
                
                int num = SelectBehavior(1, randomMonsterCount);
                AttackMonsters(num);

                if(spawnedMonsters.All(monster => monster.Hp == 0))
                {
                    break;
                }

                EnemyPhase();

                if(player.hp <= 0)
                {
                    break;
                }
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
                    Console.WriteLine($"{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n HP: {isDeadTxt}\n\n");

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
            selectMonIndex = attackMonsterNum;
            int attackRange = random.Next(player.ad * 9 / 10, player.ad * 11 / 10); //10%오차

            if (!(spawnedMonsters[attackMonsterNum - 1].Hp == 0)) //입력은 1-4이고, 배열은 0부터여서 -1 참조오류 해결
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                Console.WriteLine($"{player.name}의 공격!");
                Console.WriteLine($"Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name} 을(를) 맞췄습니다.  [데미지 : {attackRange}]");
                Console.WriteLine($"\nLv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name}");
                spawnedMonsters[attackMonsterNum - 1].Hp -= attackRange;
                Console.Write($"HP {spawnedMonsters[attackMonsterNum - 1].Hp + attackRange} ");
                Console.WriteLine($"-> {spawnedMonsters[attackMonsterNum - 1].Hp}\n");



                //몬스터 체력이 0보다 작으면 0으로
                if (spawnedMonsters[attackMonsterNum - 1].Hp <= 0)
                {
                    spawnedMonsters[attackMonsterNum - 1].Hp = 0;

                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();

            }

            Console.WriteLine("0. 다음");
            int num = SelectBehavior(0, 0);
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
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();

            if (player.hp <= 0)
            {
                player.hp = 0;
                Console.WriteLine($"{player.name}님이 사망하였습니다");
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

                        Console.WriteLine($"Lv.{currentMonster.Lv} {currentMonster.Name} 의 공격!");
                        Console.WriteLine($"{player.name} 을(를) 맞췄습니다.  [데미지 : {currentMonster.Atk}]\n");
                        Console.WriteLine($"Lv.{player.level} {player.name}");
                        Console.WriteLine($"HP {player.hp + currentMonster.Atk} -> {player.hp}\n");
                        Console.WriteLine("0. 다음\n");
                        Console.WriteLine("대상을 선택해주세요.");
                        int num = SelectBehavior(0, 0);
                        if (num == 0)
                        {
                            Console.Clear();
                        }


                    }
                }
                
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();
            ShowMonsters();
            PlayerStat(true);
        }
        
        //플레이어 정보 출력
        public void PlayerStat(bool isFight)
        {
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level:D2}\nJob {player.job}");
            Console.WriteLine($"HP {player.hp}/100\n");
            string press1 = isFight == true ? "공격할 몬스터를 입력해주세요." : "1. 공격\n\n원하시는 행동을 입력해주세요.";
            Console.WriteLine($"{press1}");
        }

        public int SelectBehavior(int min, int max)
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
    }
}
