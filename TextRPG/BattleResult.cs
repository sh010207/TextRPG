using System.Globalization;
namespace TextRPG
{
    public class BattleResult
    {
        Player player;
        Monster monster;
        Dungeon dungeon;
        GameManager gameManager;
        public BattleResult(Player player, Dungeon dungeon)
        {
            this.player = player;
            this.dungeon = dungeon;
        }
        //게임종료의 조건 / 승패 판별
        public void GameEndLogic()
        {
            if (player.hp <= 0)
            {
                LoseUI();
            }
            else
            {
                WinUI();
            }
        }
        public void GetResult() //보상
        {
            float totalexp = 0;
            int totalgold = 0;
            for (int i = 0; i < dungeon.spawnedMonsters.Count; i++)
            {
                Monster monster = dungeon.spawnedMonsters[i];
                totalexp += monster.Exp;
                totalgold += monster.Gold;
            }
            player.gold += totalgold;
            player.exp += totalexp;
            Console.WriteLine($"{totalexp} 경험치 획득!  현재 경험치 : {player.exp}/{player.MaxExp.ToString("F1")}");
            Console.WriteLine($"{totalgold} 골드 획득!  현재 골드 : {player.gold}");
            Random random = new Random();
            int randomnum = random.Next(1, 10);
            if (randomnum >= 3)
            {
                dungeon.potionCount++;
            }
        }
        //승리 시 결과화면
        public void WinUI()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!! - Result\n");
            GetResult();
            player.LevelUp();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Victory\n");
            Console.ResetColor();
            //아마도 랜덤 소환 몬스터 리스트 크기만큼 잡았다고 할 예정
            Console.WriteLine($"던전에서 몬스터 {dungeon.randomMonsterCount}마리를 잡았습니다.\n"); //추후 수정
            Console.WriteLine($"Lv.{player.level} {player.job}");
            Console.WriteLine($"HP 100 -> {player.hp}\n\n0.다음\n");
            player.ResetHp();
        }
        //패배 시 결과화면
        public void LoseUI()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!! - Result\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You Lose\n");
            Console.ResetColor();
            Console.WriteLine($"Lv.{player.level} {player.job}");
            Console.WriteLine($"HP {player.maxhp} -> {player.hp}\n\n0.다음\n");
            player.ResetHp();
        }
    }
}