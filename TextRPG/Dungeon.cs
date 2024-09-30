namespace TextRPG
{
    public class Dungeon
    {
        Monster monster;
        Player player;
        Random random = new Random();
        public int randomMonsterCount;
        List<Monster> spawnedMonsters = new List<Monster>(); //랜덤 소환된 몬스터(전투중인?)

        public Dungeon(Player player)
        {
            this.player = player;
        }


        // 몬스터 랜덤 생성
        public void RandMonsters(bool ShowIndex)
        {

            randomMonsterCount = random.Next(1, 5); // 몬스터 등장 수

            List<Monster> monsters = new List<Monster>(); // 몬스터 리스트 생성
            {
                monsters.Add(new Monster(2, "슬라임", 10, 5));
                monsters.Add(new Monster(5, "고블린", 15, 7));
                monsters.Add(new Monster(7, "오크", 25, 10));
            }


            for (int i = 0; i < randomMonsterCount; i++)
            {
                monster = monsters[random.Next(monsters.Count)];
                spawnedMonsters.Add(monster);
                string MonsterIndex = ShowIndex ? $"{i + 1}" : ""; // 몬스터의 인덱스를 표시
                Console.WriteLine($"{MonsterIndex}  Lv.{monster.Lv} {monster.Name}\n HP: {monster.Hp}\n\n");// 생성된 몬스터 출력
            }
        }




        public void ShowMonsters()
        {
            for (int i = 0; i < spawnedMonsters.Count; i++)
            {
                string isDeadTxt = spawnedMonsters[i].IsDead ? "Dead" : $" HP: {spawnedMonsters[i].Hp}";
                Console.WriteLine($"{i + 1}  Lv.{spawnedMonsters[i].Lv} {spawnedMonsters[i].Name}\n HP: {spawnedMonsters[i].Hp}\n\n");


            }
        }

        //공격 후 몬스터 체력감소
        public void AttackMonsters(int attackMonsterNum)
        {
            if (spawnedMonsters[attackMonsterNum].IsDead == false)
            {
                int attackRange = random.Next(player.ad * 9 / 10, player.ad * 11 / 10); //10%오차
                
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Battle!!\n");
                Console.ResetColor();

                Console.WriteLine($"{player.name}의 공격!");
                Console.WriteLine($"Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name} 을(를) 맞췄습니다.  [데미지 : {attackRange}]");
                Console.WriteLine($"Lv.{spawnedMonsters[attackMonsterNum - 1].Lv} {spawnedMonsters[attackMonsterNum - 1].Name}");
                Console.WriteLine($"HP {spawnedMonsters[attackMonsterNum].Hp} -> Dead");

                spawnedMonsters[attackMonsterNum].Hp -= attackRange;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ResetColor();
            }

        }


    }
}
