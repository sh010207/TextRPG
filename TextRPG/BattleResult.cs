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
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n\t\t\t\t      {totalexp}");
            Console.ResetColor();
            Console.WriteLine($"경험치 획득!  현재 경험치 : {player.exp}/{player.MaxExp.ToString("F1")}");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"\n\t\t\t\t      {totalgold}");
            Console.ResetColor();
            Console.WriteLine($"골드 획득!  현재 골드 : {player.gold}\n\n");
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
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\t\t\t\t     V     V  III  CCCCC  TTTTT  OOOOO  RRRRR  Y   Y\r\n\t\t\t\t      V   V    I   C        T    O   O  R   R   Y Y \r\n\t\t\t\t       V V     I   C        T    O   O  RRRRR    Y  \r\n\t\t\t\t        V      I   C        T    O   O  R   R    Y  \r\n\t\t\t\t        V     III  CCCCC    T    OOOOO  R   R    Y  \r\n\n");
            Console.ResetColor();
            Console.WriteLine("\t\t\t\t\t\t       전 투 결 과\n");
            GetResult();
            player.LevelUp();
            Console.ResetColor();;
            //아마도 랜덤 소환 몬스터 리스트 크기만큼 잡았다고 할 예정
            Console.Write("\n\n\t\t\t\t          던전에서 몬스터 "); //추후 수정
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{dungeon.randomMonsterCount}"); //추후 수정
            Console.ResetColor();
            Console.Write("마리를 잡았습니다.\n\n"); //추후 수정
            Console.WriteLine($"\t\t\t\t\t\t     Lv.{player.level} {player.job}");
            Console.WriteLine($"\t\t\t\t\t\t     HP 100 -> {player.hp}\n\n"); //
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t\t\t\t\t   0");
            Console.ResetColor();
            Console.WriteLine("을 누르면 메인화면으로 돌아갑니다.\n");
            
            player.ResetHp();
        }
        //패배 시 결과화면
        public void LoseUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\t\t\t\t            L      OOOOO  SSSSS  EEEEE\r\n\t\t\t\t            L     O     O S      E    \r\n\t\t\t\t            L     O     O  SSS   EEEE \r\n\t\t\t\t            L     O     O     S  E    \r\n\t\t\t\t            LLLLL  OOOOO  SSSSS  EEEEE\r\n\n\n");
            Console.ResetColor();
            //죽었을때 3초뒤 메인화면으로 가게하기
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n\n\t\t\t\t\t       {player.name}님이 사망하였습니다.\n\n");
            Console.WriteLine($"\n\n\t\t\t\t\t\t     Lv.{player.level} {player.job}");
            Console.WriteLine($"\n\t\t\t\t\t\t    HP {player.maxhp} -> {player.hp}\n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("\t\t\t\t\t   0");
            Console.ResetColor();
            Console.WriteLine("을 누르면 메인화면으로 돌아갑니다.\n");
            player.ResetHp();
        }
    }
}