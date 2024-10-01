namespace TextRPG
{
    public class Dungeon
    {
        Monster monster;
        Player player;
        Random random = new Random();
        public int randomMonsterCount;
        int selectMonIndex = 10;


        List<Monster> spawnedMonsters = new List<Monster>(); //랜덤 소환된 몬스터(전투중인?)
        
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
                monsters.Add(new Monster(2, "슬라임", 10, 5));
                monsters.Add(new Monster(5, "고블린", 15, 7));
                monsters.Add(new Monster(7, "오크", 25, 10));
            }


            for (int i = 0; i < randomMonsterCount; i++)
            {
                // 랜덤하게 선택된 몬스터의 속성을 기반으로 새로운 몬스터 인스턴스 생성
                Monster selectedMonster = monsters[random.Next(monsters.Count)];
                Monster newMonster = new Monster(selectedMonster.Lv, selectedMonster.Name,
                    selectedMonster.Hp, selectedMonster.Atk);

                spawnedMonsters.Add(newMonster);

                string MonsterIndex = ShowIndex ? $"{i + 1}" : ""; // 몬스터의 인덱스를 표시
                Console.WriteLine($"{MonsterIndex}  Lv.{newMonster.Lv} {newMonster.Name}\n HP: {newMonster.Hp}\n\n");// 생성된 몬스터 출력
            }
        }

        //2. 같은 종류의 몬스터가 다같이 피가 닳는 현상 => 객체화?하나씩 나누기 피통,레벨등


        public void ShowMonsters() //isDead?
        {

            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                if (selectMonIndex > 0 && selectMonIndex < spawnedMonsters.Count)
                {
                    string isDeadTxt = spawnedMonsters[selectMonIndex - 1].Hp == 0 ? "Dead" : $" HP: {spawnedMonsters[i].Hp}";
                    Console.WriteLine($"{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n HP: {isDeadTxt}\n\n");

            }
                else
            {
                Console.WriteLine($"{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n HP: {spawnedMonsters[i].Hp}\n\n");

            }

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
                Console.WriteLine($"Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name}");
                spawnedMonsters[attackMonsterNum - 1].Hp -= attackRange;
                Console.Write($"\nHP {spawnedMonsters[attackMonsterNum - 1].Hp + attackRange} ");
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
                    ShowMonsters();
                    break;
            }



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
