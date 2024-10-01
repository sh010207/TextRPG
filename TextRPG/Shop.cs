using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {
        private Player player;
        public static Item[] itemDb = new Item[10];
        public static GameManager gameManager;


        public Shop(Player player)
        {
            this.player = player;
        }


        // 상점
        // 1. 물건 리스트 표현
        // 2. 구매 이동

        public void DisplayShopUI() // 물건 리스트 표현
        {
            Console.Clear();
            Console.WriteLine("딸랑, 상점입니다.");
            Console.WriteLine("원하시는 아이템을 구경하세요\n");

            Console.WriteLine($"[ 보유골드 : {player.gold}] \n "); // {player.gold}

            Console.WriteLine("[ 아이템 목록 ]\n");


            //구매 할 수 있는 리스트들 나열, 구매 한건 했다고 떠야함

            for (int i = 0; i < GameManager.items.Count; i++) //item
            {
                //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
                Item curItem = GameManager.items[i];

                string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.itemPrice}G";
                Console.WriteLine($"- {curItem.ItemInfoText()} | {displayPrice}");
            }


            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기\n\n");

            Console.Write(" 원하시는 행동을 선택해주세요.  ");

            // 집에갈래? 사러갈래?

            int result = GameManager.SelectBehavior(1, 3);

            switch (result)
            {
                case 0:
                    GameManager.GameStartUI();
                    break;

                case 1:
                    DisplayBuyUI();
                    break;
            }


        }

        public void DisplayBuyUI() // 진짜 구매
        {
            Console.Clear();
            Console.WriteLine("아이쇼핑을 그만하신다구요? 좋습니다.");
            Console.WriteLine("구매하실 아이템을 골라주세요.\n");

            Console.WriteLine($"[ 보유골드 : {player.gold} ] \n "); // {player.gold}

            Console.WriteLine("[아이템 목록]\n");

            for (int i = 0; i < GameManager.items.Count; i++) //item
            {
                //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
                Item curItem = GameManager.items[i];

                string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.itemPrice}G";
                Console.WriteLine($"[{i + 1}]  {curItem.ItemInfoText()} | {displayPrice}");
            }

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int result = GameManager.SelectBehavior(1, 3);

            switch (result)
            {
                case 0:
                    DisplayShopUI();
                    break;

                default: // 0 이 아닌 다른값이 들어온다면 진입

                    int itemIdx = result - 1;
                    Item targetItem = itemDb[itemIdx];
                    if (player.HasItem(targetItem))

                    {
                        Console.WriteLine(" ==== 허허, 이 아이템, 두개는 없소 =====");
                        Console.WriteLine("enter를 입력해서 다른 아이템을 구매하세요.");
                        Console.ReadLine();
                    }
                    //구매 완료 표시가 떠야함
                    else // 살 수 있다! 구매 표시가 없을때!
                    {
                        if (player.gold >= targetItem.itemPrice) // 돈이 많을경우 (구매가능)
                        {
                            Console.WriteLine(" === 진짜 살겁니까? 뒤로가긴 없소. === ");
                            Console.WriteLine("enter를 입력하면 구매됩니다.");
                            Console.ReadLine();

                            player.DecreaseGold(targetItem);
                        }
                        else
                        {
                            Console.WriteLine(" === 아직 이 물건을 살 골드를 챙겨오지 못했군.. === ");
                            Console.WriteLine("enter를 입력해서 가격에 맞춰 구매해주세요.");
                            Console.ReadLine();

                        }
                    }

                    DisplayBuyUI();
                    break;
            }

        }

    }
}
//for (int i = 0; i < itemDb.Length; i++) //item
//{
//    //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
//    Item curItem = itemDb[i];

//    string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.Price}G";
//    Console.WriteLine($"-{i + 1} {curItem.ItemInfoText()} | {displayPrice}");
//}
//if (player.HasItem(curItem)) {
//  
//      if (item.pstack == 0 ) { displayPrice = " 구매완료 " }
//      else --item.pstack
//}



