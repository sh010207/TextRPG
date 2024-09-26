namespace TextRPG
{
    public class Dungeon
    {
        public int randomMonsterCount; //등장할 랜덤 몬스터 수
        Monster monster;

        //1-4마리의 몬스터 랜덤 등장
        public void SpawnRandomMonster()
        {
            //몬스터 리스트 저장 및 생성
            List<Func<string, Monster>> monsters = new List<Func<string, Monster>>()
            {
                name => new Slime(name),   // 슬라임 생성자
                name => new Goblin(name),  // 고블린 생성자
                name => new Oak(name)      // 오크 생성자
            };

            //랜덤으로 등장 할 몬스터 수
            Random random = new Random();
            randomMonsterCount = random.Next(1, 5); //1-4마리 랜덤
            string[] monsterName = { "슬라임", "고블린", "오크" };

            for (int i = 0; i < randomMonsterCount; i++)
            {
                int randomMonsterType = random.Next(monsters.Count); // 랜덤으로 등장할 몬스터 타입 선택


                // 몬스터 생성 (랜덤 타입과 이름으로)
                monster = monsters[randomMonsterType](monsterName[randomMonsterType]);

                // 몬스터 정보 출력
                Console.WriteLine($"Lv. 1 {monster.Name} HP: {monster.Hp}\n\n");
                Console.WriteLine("구현중 일단 0번 누르기");
            }

        }
    }
}
