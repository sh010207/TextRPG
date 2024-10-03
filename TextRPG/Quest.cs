using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRPG
{
    public class Quest
    {
        Item item1;
        GameManager gameManager;
        static Inventory inventory;
        public Player player;
        QuestData quest;
        List<QuestData> QuestDataList =new List<QuestData>();
        List<QuestData> AcceptQuestList = new List<QuestData>();
        List<QuestData> CompletedQuestList = new List<QuestData>();


        List<QuestRewardItem> RewardItemList = new List<QuestRewardItem>();
        List<QuestRewardItem> AcceptRewardItem = new List<QuestRewardItem>();
        List<QuestRewardItem> ComletedRewardItem = new List<QuestRewardItem>();


        public enum QuestType
        {
            Buy,
            Equip,
            Monster
        }

        int QuestCount;
        private int QuestNum;
        int selectNum;
        int GoalCount = 0;


        public Quest()
        {
            QuestDataList.Add(new QuestData(
                "샵에서 아이템을 사보자!", "RPG에서는 아이템이 중요하죠! 아이템을 사볼까요?", "Shop에서 아이템 사기",
                     1, 0, false, false,"Buy"));
            QuestDataList.Add(new QuestData(
                "아이템을 장착해보자!",
                "아이템을 샀으면 장착을 해야겠죠?", "아이템 하나 장착하기",
                   1, 0, false, false,"Equip"));
            QuestDataList.Add(new QuestData(
                  "몬스터를 잡아보자!",
                 "RPG의 꽃! 전투를 해봅시다! 몬스터를 잡아봐요!", "고블린 처치하기",
                 5, 0, false, false,"Monster"));
            //////
            RewardItemList.Add(new QuestRewardItem(" 게 등딱지 방패 ", 1, 5, 1, "        등딱지..? 간장게장 먹고싶어지는 방패다..!       ", 500,0,"Buy")); // Quest 1
            RewardItemList.Add(new QuestRewardItem(" 조개껍질 단검  ", 0, 5, 1, "   보다시피 조개껍질을 갈아만든 무기..생각보다 뾰족하다!", 700,0,"Equip")); // Quest 2
            RewardItemList.Add(new QuestRewardItem(" 고블린 모자    ", 1, 7, 1, "          고블린 모자다..내가 고블린보단 낫겠지,,?      ", 1000,0,"Monster")); // Quest 3
        }
        public static Item RewardItemToItem(QuestRewardItem rewardItem)
        {
            return new Item(rewardItem.RewardItem,
                                rewardItem.RewardItemType,
                                rewardItem.RewardItemValue,
                                rewardItem.RewardItemDesc,
                                rewardItem.RewardItemPrice);
        }

        public void CreatedQuestList()
        {
            for(int i = 0 ; i < QuestDataList.Count; i++)
            {
                QuestData Q_data = QuestDataList[i];
                string ShowQuestNumber = "!";
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"- [{ShowQuestNumber}] {Q_data.QuestName}");
                Console.ResetColor();

            }
        }

        public void QuestMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  \t\t\t\t=================Quest!!================\n\n");
            Console.ResetColor();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  \t\t\t\t\t     1.퀘스트 목록         \n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("  \t\t\t\t\t     2.진행중인 퀘스트 목록\n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("  \t\t\t\t\t     3.완료된 퀘스트 목록  \n\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  \t\t\t\t\t     0.나가기                ");
            Console.ResetColor();
            Console.WriteLine("\n\t\t\t\t\t     원하시는 행동을 입력하세요.            ");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.Write(">>");
            Console.ResetColor();
            int result = GameManager.SelectBehavior(0, 3);
            switch (result)
            {
                case 0:
                    GameManager.GameStartUI();
                    break;
                case 1:
                    QuestUI();
                    break;
                case 2:
                    QuestInProgressUI();
                    break;
                case 3:
                    QuestCompletedUI();
                    break;
            }
        }


        public void QuestUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            if(QuestDataList.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("[퀘스트가 없습니다.]");
                Console.ResetColor();
            }
            else
            {
                CreatedQuestList();
            }

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t0. 나가기\n");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("\n\n원하시는 퀘스트를 선택해주세요.");

            selectNum = GameManager.SelectBehavior(0, QuestDataList.Count);
            switch (selectNum)
            {
                case 0:
                    QuestMenu();    
                    break;
                case 1:
                    CreateQuestinfoText(selectNum);
                    break;
                case 2:
                    CreateQuestinfoText(selectNum);
                    break;
                case 3:
                    CreateQuestinfoText(selectNum);
                    break;
                default:
                    break;
            }
        }

        public void CreateQuestinfoText(int selectNum)
        {
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();
            QuestData currentQuest = QuestDataList[selectNum - 1];
            QuestRewardItem currentRewardItem = RewardItemList[selectNum - 1];


            Console.WriteLine($"{currentQuest.QuestName}\n\n{currentQuest.QuestDesc}\n\n -{currentQuest.QuestGoal}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            //Console.Write($"({GoalCount}/{currentQuest.QuestGoalCount})\n");
            Console.ResetColor();
            Console.WriteLine($" -보상-\n {currentRewardItem.RewardItem} x{currentRewardItem.RewardItemNum}\n {currentRewardItem.RewardGold}G\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("1. 수락");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("2. 거절\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t0. 나가기\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">>");
            Console.ResetColor();

            int result = GameManager.SelectBehavior(0, QuestDataList.Count);
            switch (result)
            {
                case 0:
                    QuestMenu();
                    break;
                case 1:
                    AcceptQuest();
                    QuestUI();
                    break;
                case 2:
                    //Console.WriteLine("거절하셨습니다.");
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("   [정말로 거절하시겠습니까..?]");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("   [1.예]             ");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("   [2.아니오]          \n");
                    Console.Write(">>");
                    Console.ResetColor();


                    int result_2 = GameManager.SelectBehavior(1,2);
                    switch (result_2)
                    {
                        case 1:
                            QuestUI();
                            break;
                        case 2:
                            CreateQuestinfoText(selectNum);
                            break;
                    }
                    break;
            }
        }


        public void QuestInProgressUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            if(AcceptQuestList.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("\n\n       [진행중인 퀘스트가 없습니다.]       \n\n");
                Console.ResetColor();
            }
            for (int i = 0; i < AcceptQuestList.Count; i++)
            {
                QuestData AcceptQuest = AcceptQuestList[i];
                if(AcceptQuest.IsSuccess == false)
                {
                    string ShowQuestNumber = "?";
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"- [{ShowQuestNumber}] {AcceptQuest.QuestName}");
                    Console.ResetColor();
                }
            }

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t0. 나가기\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">>");
            Console.ResetColor();

            selectNum = GameManager.SelectBehavior(0, AcceptQuestList.Count);
            switch (selectNum)
            {
                case 0:
                    QuestMenu();
                    break;
                case 1:
                    ProgressQuestInfoText(selectNum);
                    break;
                case 2:
                    ProgressQuestInfoText(selectNum);
                    break;
                case 3:
                    ProgressQuestInfoText(selectNum);
                    break;
            }
        }

        public void ProgressQuestInfoText(int selectNum)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();
            QuestData current_Quest = AcceptQuestList[selectNum - 1];
            QuestRewardItem current_RewardItem = AcceptRewardItem[selectNum - 1];


            Console.WriteLine($"{current_Quest.QuestName}\n\n{current_Quest.QuestDesc}\n\n -{current_Quest.QuestGoal} " +
                $"({current_Quest.QuestCurrentGoalCount}/{current_Quest.QuestGoalCount})");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.ResetColor();
            Console.WriteLine($" -보상-\n {current_RewardItem.RewardItem} x{current_RewardItem.RewardItemNum}\n {current_RewardItem.RewardGold}G\n");

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\t0. 나가기\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">>");
            Console.ResetColor();
            int Choice_1 = GameManager.SelectBehavior(0, 0);
            switch (Choice_1)
            {
                case 0:
                    QuestInProgressUI();
                    break;
            }
        }

        public void QuestCompletedUI()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            if (CompletedQuestList.Count <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n\n       [완료된 퀘스트가 없습니다.]       \n\n");
                Console.ResetColor();
            }
            for (int i = 0; i < CompletedQuestList.Count; i++)
            {
                QuestData completedQuest = CompletedQuestList[i];
                string ShowQuestNumber = "?";
                completedQuest.IsSuccess = true;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine($"- [{ShowQuestNumber}] {completedQuest.QuestName}");
                Console.ResetColor();
            }

            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\n\t0. 나가기\n");
            Console.ResetColor();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(">>");
            Console.ResetColor();


            selectNum = GameManager.SelectBehavior(0, CompletedQuestList.Count);
            switch (selectNum)
            {
                case 0:
                    QuestMenu();
                    break;
                case 1:
                    CompletedQuestInfoText(selectNum);
                    break;
                case 2:
                    CompletedQuestInfoText(selectNum);
                    break;
            }

        }

        public void CompletedQuestInfoText(int selectNum)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!\n");
            Console.ResetColor();

            QuestData current_Quest_1 = CompletedQuestList[selectNum - 1];
            QuestRewardItem current_RewardItem_1= ComletedRewardItem[selectNum - 1];

            Console.WriteLine($"{current_Quest_1.QuestName}\n\n{current_Quest_1.QuestDesc}\n\n -{current_Quest_1.QuestGoal} " +
            $"({current_Quest_1.QuestCurrentGoalCount}/{current_Quest_1.QuestGoalCount})");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.ResetColor();
            Console.WriteLine($" -보상-\n {current_RewardItem_1.RewardItem} x{current_RewardItem_1.RewardItemNum}\n {current_RewardItem_1.RewardGold}G\n");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("1.보상받기");
            Console.ResetColor();
            Console.Write("0.나가기\n\n");
            Console.WriteLine("원하시는 행동을 입력하세요.");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(">> ");
            Console.ResetColor();

            selectNum = GameManager.SelectBehavior(0, 1);
            switch(selectNum)
            {
                case 0:
                    QuestCompletedUI();
                    break;
                case 1:
                    RewardPayMent(item1);
                    QuestCompletedUI();
                    break;
            }
        }

        public void AcceptQuest()
        {
            QuestData AcceptQuest = QuestDataList[selectNum - 1];
            QuestRewardItem questRewardItem = RewardItemList[selectNum - 1];
            AcceptQuestList.Add(AcceptQuest);
            AcceptRewardItem.Add(questRewardItem);
            QuestDataList.RemoveAt(selectNum - 1);
            RewardItemList.RemoveAt(selectNum - 1);

        }

        public void QuestProgress(QuestType questType)
        {
            for (int i = 0; i < AcceptQuestList.Count; i++)
            {
                QuestRewardItem questRewardItem = AcceptRewardItem[i];
                QuestData questData = AcceptQuestList[i];
                switch (questType)
                {
                    case QuestType.Buy:
                        questData.QuestCurrentGoalCount++;
                        if (questData.QuestGoalCount == questData.QuestCurrentGoalCount)
                        {
                            AcceptRewardItem.RemoveAt(i);
                            ComletedRewardItem.Add(questRewardItem);
                            AcceptQuestList.RemoveAt(i);
                            CompletedQuestList.Add(questData);
                        }
                        break; 

                    case QuestType.Equip:
                        questData.QuestCurrentGoalCount++;
                        if (questData.QuestGoalCount == questData.QuestCurrentGoalCount)
                        {
                            AcceptRewardItem.RemoveAt(i);
                            ComletedRewardItem.Add(questRewardItem);
                            AcceptQuestList.RemoveAt(i);
                            CompletedQuestList.Add(questData);
                        }

                        break;

                    case QuestType.Monster:
                        questData.QuestCurrentGoalCount++;
                        if (questData.QuestGoalCount == questData.QuestCurrentGoalCount)
                        {
                            AcceptRewardItem.RemoveAt(i);
                            ComletedRewardItem.Add(questRewardItem);
                            AcceptQuestList.RemoveAt(i);
                            CompletedQuestList.Add(questData);
                        }

                        break;
                }
            }
        }

        public void RewardGoldPayMent()
        {
            List <QuestRewardItem> list = new List <QuestRewardItem>(ComletedRewardItem);
            for(int i = 0; i < CompletedQuestList.Count; i++)
            {
                QuestRewardItem rewardGold = list[i];
                switch (rewardGold.ItemType)
                {
                    case "Buy":
                        GameManager.player.gold += rewardGold.RewardGold;
                        break;
                    case "Equip":
                        GameManager.player.gold += rewardGold.RewardGold;   
                        break;
                    case "Monster":
                        GameManager.player.gold += rewardGold.RewardGold;
                        break;
                }

            }
            //QuestRewardItem rewardGold = RewardItemList[selectNum - 1];
            

        }

        public void RewardPayMent(Item Item)
        {
            List<Item> items = ComletedRewardItem.ConvertAll(new Converter<QuestRewardItem, Item>(RewardItemToItem));

            for (int i = 0; i < CompletedQuestList.Count; i++)
            {
                QuestData questData = CompletedQuestList[i];
                switch(questData.QuestType)
                {
                    case "Buy":
                        Item item_1 = items.Find(x => x.itemName == " 게 등딱지 방패 ");
                        RewardGoldPayMent();
                        GameManager.player.Inventory.Add(item_1);
                        CompletedQuestList.RemoveAt(i);
                        ComletedRewardItem.RemoveAt(i);

                        break;
                    case "Equip":
                        Item item_2 = items.Find(x => x.itemName == " 조개껍질 단검  ");
                        RewardGoldPayMent();
                        GameManager.player.Inventory.Add(item_2);
                        CompletedQuestList.RemoveAt(i);
                        ComletedRewardItem.RemoveAt(i);


                        break;
                    case "Monster":
                        Item item_3 = items.Find(x => x.itemName == " 고블린 모자    ");
                        RewardGoldPayMent();
                        GameManager.player.Inventory.Add(item_3);
                        CompletedQuestList.RemoveAt(i);
                        ComletedRewardItem.RemoveAt(i);


                        break;
                }
            }

        }
    }
}
