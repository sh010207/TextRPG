using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class QuestData
    {

        public string QuestGoal { get; set; }
        public string QuestName { get; set; }
        public string QuestDesc { get; set; }
        public int QuestGoalCount { get; set; }
        public int QuestCurrentGoalCount { get; set; }

        public bool IsQuest;
        public bool IsSuccess;

        public QuestData(string questName, string questDesc, string questGoal, int questGoalCount, 
            bool IsQuest, bool isSuccess, int questCurrentGoalCount)
        {

            QuestName = questName;
            QuestDesc = questDesc;
            QuestGoal = questGoal;
            QuestGoalCount = questGoalCount;
            QuestCurrentGoalCount = questCurrentGoalCount;


            IsQuest = false;
            IsSuccess = isSuccess;
        }
    }


    public class QuestRewardItem
    {

        public string RewardItem { get; set; }
        public int RewardItemType { get; set; }
        public int RewardItemValue { get; set; }
        public int RewardItemNum { get; set; }
        public string RewardItemDesc { get; set; }
        public int RewardGold { get; set; }


        public QuestRewardItem(string rewardItem, int rewardItemType, int rewardItemValue, int rewardItemNum, string rewardItemDesc, int rewardGold)
        {
            RewardItem = rewardItem;
            RewardItemType = rewardItemType;
            RewardItemValue = rewardItemValue;
            RewardItemNum = rewardItemNum;
            RewardItemDesc = rewardItemDesc;
            RewardGold = rewardGold;
        }
        public string DisplayTypeText
        {
            get
            {
                return RewardItemType == 0 ? "공격력" : "방어력";
            }
        }
    }
}
