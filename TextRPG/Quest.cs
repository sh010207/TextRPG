using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRPG
{

    public  class QuestData
    {
        public bool IsQuest;
        public string QuestGoal {  get; set; }
        public string QuestName {  get; set; }
        public string QuestDesc { get; set; }
        public string Reward { get; set; }
        public int RewardGold { get; set; }
        public int RewardItemNum { get; set; }
        public QuestData(string questName, string questDesc,string questgoal, string reward,int rewarditemnum, int rewardgold, bool IsQuest)
        {

            QuestName = questName;
            QuestDesc = questDesc;
            Reward = reward;
            RewardGold = rewardgold;
            RewardItemNum = rewarditemnum;
            RewardGold = rewardgold;
            QuestGoal = questgoal;
            IsQuest = false;
        }   
    }
    public class Quest
    {
        Item item;
        QuestData quest;
        QuestData Accept;
        List<QuestData> questData = new List<QuestData>();
        List<QuestData> AcceptQuestList = new List<QuestData>();
        int selectQuest;
        private int QuestNum;

        public void CreateQuestList()
        {
            questData.Clear();
;

            List<QuestData> CreateQuest = new List<QuestData>();
            {
                CreateQuest.Add(new QuestData("샵에서 아이템을 사보자!",
                    "RPG에서는 아이템이 중요하죠! 아이템을 사볼까요?", "Shop에서 아이템 사기", "쓸만한 방패", 1, 100,false));
                CreateQuest.Add(new QuestData("아이템을 장착해보자!", "아이템을 샀으면 장착을 해야겠죠?", "아이템 하나 장착하기", "HP포션"
                    , 5, 100,false));
            }
            for (int i = 0; i < CreateQuest.Count; i++)
            {
                QuestData data1 = CreateQuest[i];
                quest  = new QuestData(data1.QuestName, data1.QuestDesc, data1.QuestGoal,
                    data1.Reward, data1.RewardItemNum, data1.RewardGold,data1.IsQuest);

                questData.Add(quest);
                Console.WriteLine($"{i+1}. {quest.QuestName}");
            }
            //if (Accept == false)
            //{

            //}
            //else
            //{
            //    Console.ForegroundColor = ConsoleColor.Green;
            //    Console.WriteLine($"{i + 1}. {quest.QuestName} (진행중)");
            //    Console.ResetColor();
            //}
        }


        public void QuestUI()
        {
            Console.Clear();    
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            CreateQuestList();

            Console.WriteLine("\n\n원하시는 퀘스트를 선택해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">>");
            Console.ResetColor();


            int result = GameManager.SelectBehavior(1, questData.Count);
            switch (result)
            {
                case 1:
                    QuestinfoText();
                    break;
                case 2:
                    QuestinfoText();
                    break;

                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    QuestUI();
                    break;

            }
        }

        public void QuestinfoText()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            Console.WriteLine($"{quest.QuestName}\n\n{quest.QuestDesc}\n\n-{quest.QuestGoal}\n");

            Console.WriteLine($" -보상-\n {quest.Reward} x{quest.RewardItemNum}\n {quest.RewardGold}G\n");
            Console.WriteLine("1. 수락\n\n2. 거절\n");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">>");
            Console.ResetColor();

            int result = GameManager.SelectBehavior(1, 2);
            switch (result)
            {
                case 1:
                    AcceptQuest();
                    break;
                case 2:
                    Console.WriteLine("거절하셨습니다.");
                    break;
            }
        }

        public void AcceptQuest()
        {

            for(int i = 0; i < questData.Count; i++ )
            {

            }
        }
    }
}
