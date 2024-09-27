using System.Globalization;

namespace TextRPG
{
    public class BattleResult
    {
         Player player;

        public BattleResult(Player player)
        {
            this.player = player;
        }

        //게임종료의 조건 / 승패 판별
        public void GameEndLogic()
        {
            if(player.hp <= 0)
            {
                LoseUI();
            }
            else
            {
                WinUI();
            }
        }

        //승리 시 결과화면
        public void WinUI()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Battle!! - Result\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Victory\n");
            Console.ResetColor();

            //아마도 랜덤 소환 몬스터 리스트 크기만큼 잡았다고 할 예정
            Console.WriteLine($"던전에서 몬스터 {4}마리를 잡았습니다.\n"); //추후 수정

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
            Console.WriteLine($"HP 100 -> {player.hp}\n\n0.다음\n");

            player.ResetHp();
        }


    }
}
