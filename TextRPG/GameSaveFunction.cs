using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TextRPG
{
    [Serializable]
    internal class GameSaveFunction
    {
        private Player player;

        public GameSaveFunction(Player player)
        {
            this.player = player;
        }


        public static void SaveGame(Player data, string filePath)
        {
            // GameData 객체를 JSON 문자열로 변환
            string jsonString = JsonSerializer.Serialize(data);
            // JSON 문자열을 파일로 저장
            File.WriteAllText(filePath, jsonString);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" \n\n\n\t\t\t\t          SSSSS   AAAAA  V     V  EEEEE  \r\n \n\t\t\t\t         S       A   A   V   V   E      \r\n \n\t\t\t\t          SSS    AAAAA    V V    EEEE   \r\n  \n\t\t\t\t              S  A   A     V     E      \r\n  \n\t\t\t\t         SSSSS   A   A     V     EEEEE  \r\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n\n\n\t\t\t\t\t       아무키나 눌러주세요");
            Console.ResetColor();
            Console.ReadKey(); // 한글자 입력*
            // 꺼지지않고 돌아가는 방법이 뭘까? 이어가려면.... //클래스 직렬화가 안된다 < 시리얼라이즈블
        }

        public static Player LoadGame(string filePath) 
        {
            if (File.Exists(filePath))
            {
                // 파일에서 JSON 문자열을 읽어옴
                string jsonString = File.ReadAllText(filePath);
                // JSON 문자열을 GameData 객체로 변환 (역직렬화)
                Player data = JsonSerializer.Deserialize<Player>(jsonString);
                Console.Clear();
                Console.ForegroundColor= ConsoleColor.Magenta;
                Console.WriteLine(" \n\n\n\t\t\t\t           L      OOOOO  AAAAA  DDDD   \r\n  \n\t\t\t\t           L     O     O A   A  D   D  \r\n  \n\t\t\t\t           L     O     O AAAAA  D   D  \r\n  \n\t\t\t\t           L     O     O A   A  D   D  \r\n  \n\t\t\t\t           LLLLL  OOOOO  A   A  DDDD   \r\n");
                Console.ResetColor();
                return data;
            }
            else
            {
                Console.WriteLine("Save file not found!");
                return null;
            }
        }
    }
}
