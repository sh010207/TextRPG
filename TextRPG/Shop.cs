﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Shop
    {

        // 상점
        // 1. 물건 리스트 표현
        // 2. 구매 이동

        public void DisplayShop()
        {
            Console.WriteLine("딸랑, 상점입니다.");
            Console.WriteLine("원하시는 아이템을 구경하세요\n");

            Console.WriteLine($"[ 보유골드 ] \n "); // {player.gold}

            Console.WriteLine("[ 아이템 목록 ]");
            //Store_ItemList(false);

            //구매 할 수 있는 리스트들 나열, 구매 한건 했다고 떠야함
            /* 
             
            //for (int i = 0; i < itemDb.Length; i++) //item
            //{
            //    //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
            //    Item curItem = itemDb[i];

            //    string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.Price}G";
            //    Console.WriteLine($"-{i + 1} {curItem.ItemInfoText()} | {displayPrice}");
            //}

            // 여기서 itemInfoText는 ItemClass에서 가져와서
            
            public string ItemInfoText() 
            {
            // 아이템 정보 출력, 조금씩 다르기때문에 그래서 공통적인 부분만 표시해준다.
            // 이름 | 공격력 or 방어력 + n | 설명

            return ($"{Name} |{DisplayTypeText} + {Value} | {Desc}");
            }

            */
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기\n\n");

            Console.Write(" 원하시는 행동을 선택해주세요.  ");

            // 집에갈래? 사러갈래?
            //switch (result)
            //{
            //    case 0:
            //        DisplayMainUI();
            //        break;

            //    case 1:
            //        DisplayBuyUI();
            //        break;
            //}


        }

        public void DisplayBuy()
        {
            Console.WriteLine("아이쇼핑을 그만하신다구요? 좋습니다.");
            Console.WriteLine("구매하실 아이템을 골라주세요.\n");

            Console.WriteLine($"[보유골드] \n "); // {player.gold}

            Console.WriteLine("[아이템 목록]");

            //for (int i = 0; i < itemDb.Length; i++) //item
            //{
            //    //구매 완료 시 금액이 아니라 구매완료가 떠야한다.
            //    Item curItem = itemDb[i];

            //    string displayPrice = player.HasItem(curItem) ? "구매완료" : $"{curItem.Price}G";
            //    Console.WriteLine($"-{i + 1} {curItem.ItemInfoText()} | {displayPrice}");
            //}

            Console.WriteLine();
            Console.WriteLine("0 : 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            /*
            switch (result)
            {
                case 0:
                    DisplayShop();
                    break;

                default: // 0 이 아닌 다른값이 들어온다면 진입

                    int itemIdx = result - 1;
                    Item targetItem = itemDb[itemIdx];
                    if (player.HasItem(targetItem)) // Has는 bool값으로 player클래스에 추가로 넣어주면 될듯
                        
                    {            
                        Console.WriteLine(" ==== 허허, 이 아이템, 두개는 없소 =====");
                        Console.WriteLine("enter를 입력해서 다른 아이템을 구매하세요.");
                        Console.ReadLine();
                    }
                    //구매 완료 표시가 떠야함
                    else // 살 수 있다! 구매 표시가 없을때!
                    {
                        if (player.Gold >= targetItem.Price) // 돈이 많을경우 (구매가능)
                        {
                            Console.WriteLine(" === 진짜 살겁니까? 뒤로가긴 없소. === ");
                            Console.WriteLine("enter를 입력하면 구매됩니다.");
                            Console.ReadLine();

                            player.BuyItem(targetItem); // 
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
            */

            //Has 함수=> 가지고 있어요
            //public bool HasItem(Item item)
            //{
            //    return Inventory.Contains(item);
            //}

            // Buy함수 => 구매 후 금액차감 + 인벤토리 리스트에 배열추가
            //public void BuyItem(Item item)
            //{
            //    Gold -= item.Price; // 금액 차감
            //    Inventory.Add(item); // 인벤토리리스트에 배열 추가
            //}


        }




        static void Main(string[] args)
        {

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



