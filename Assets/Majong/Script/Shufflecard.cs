using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Shufflecard : MonoBehaviour
{
    public Player[] PlayerSet = new Player[4];
    public Card[] card_set = new Card[144];
    public Card OpCard=new Card();
    private int[] ton_index = new int[9];   // 桶子索引
    private int[] wan_index = new int[9];  // 萬字索引
    private int[] tiao_index = new int[9]; // 條索引
    private int[] flower_index = new int[8]; //花
    private int[] wesn_index = new int[4]; //東南西北
    private int[] mfb_index = new int[3]; //中發白
    //private readonly string[] Cardtype = { "ton", "tiao", "wan", "flower", "eswn", "mfb" };
    private readonly string[] Cardtype = { "wan", "ton", "tiao", "eswn", "mfb", "flower" };
    public int cardleft;
    public bool key=false;
    //public ShufflecardIO csv;
    
    float timer = 0.1F;
    public  int PlayerIndex ,LastPlayer;
    public bool isgameover=false;
    
    public void Start()
    {
        SearchNSetPlayerID();
        PlayerIndex = LastPlayer = 0;
        cardleft = 143;
        key = false;
        initial_cs();
        Rolldice();
        CyclePlay();
        


    }
    public void Update()
    {
        if(Input.GetKey(KeyCode.X))
        {
            testplayer(PlayerSet[0]);
            testplayerB(PlayerSet[1]);
        }

        //分為兩中回合，一種是發牌員回合，另一種是玩家回合:key為真，進行發牌員回合，則將查看所有玩家中的手牌，有無吃碰槓胡，
        if (key)
        {
            bool playerState = false;
            for (int i = 0; i < 4; i++)
            { 
                PlayerSet[i].Check();
            
            }
          
            if (playerState)
            {
                for (int i = 0; i < PlayerSet.Length; i++)
                {
                    PlayerSet[i].isLook = false;
                }
                PlayerSet[PlayerIndex].M_key = true;
                key = false;
            }
            else
            {
                for (int i = 0; i < PlayerSet.Length; i++)
                {
                    PlayerSet[i].isLook = false;

                }
                PlayerIndex = (LastPlayer + 1) % 4;
                PlayerSet[PlayerIndex].M_key = true;
                key = false;

            }
        }
      
           
    }
    public void ReviewLook()
    {
        for (int i = 0; i < 4; i++)
        {
            if (i != LastPlayer)
                PlayerSet[i].isLook = false;
        }
    }
    void SearchNSetPlayerID()
    {
        PlayerSet[0] = GameObject.Find("playere").GetComponent<Player>();
        PlayerSet[1] = GameObject.Find("players").GetComponent<Player>();
        PlayerSet[2] = GameObject.Find("playerw").GetComponent<Player>();
        PlayerSet[3] = GameObject.Find("playern").GetComponent<Player>();
        PlayerSet[0].ID = 0;
        PlayerSet[1].ID = 1;
        PlayerSet[2].ID = 2;
        PlayerSet[3].ID = 3;
    }
   public  void CyclePlay() 
    {
        for(int i=0;i<PlayerSet.Length;i++)
        {
            PlayerSet[i].M_key = false;
        }

        PlayerSet[PlayerIndex].M_key = true; 
    }
    void NextPlayer()
    {
          PlayerIndex = LastPlayer++; 
    }
    public int Rolldice()   //丟骰子
    {
        int[] dice1 = { 1, 2, 3, 4, 5, 6 };
        int[] dice2 = { 1, 2, 3, 4, 5, 6 };
        int[] dice3 = { 1, 2, 3, 4, 5, 6 };
        int total;
        total = dice1[UnityEngine.Random.Range(0, 6)] + dice2[UnityEngine.Random.Range(0, 6)] + dice3[UnityEngine.Random.Range(0, 6)];

        return total % 4 + 1;

    }

    void initail_card()  //初始化索引值
    {
        for (int i = 0; i < 9; i++)
        {
            ton_index[i] = 4;
            tiao_index[i] = 4;
            wan_index[i] = 4;
        }
        for (int i = 0; i < 8; i++)
        {
            flower_index[i] = 1;
        }
        for (int i = 0; i < 4; i++)
        {
            wesn_index[i] = 4;
        }
        for (int i = 0; i < 3; i++)
        {
            mfb_index[i] = 4;
        }

    }

    public void initial_cs()
    {
        int count = 0;
        int rsd;
        initail_card(); //初始化牌型統計
        for (int i = 0; i < Cardtype.Length; i++)
        {
            if (i == 1)
            {
                for (int j = 0; j < ton_index.Length; j++)
                {
                    for (int k = 0; k < ton_index[j]; k++)
                    {
                        card_set[count] = new Card();
                        card_set[count].Number = j;
                        card_set[count].Type = Cardtype[i];
                        count++;
                    }
                }
            }
            if (i == 0)
            {
                for (int j = 0; j < wan_index.Length; j++)
                {
                    for (int k = 0; k < wan_index[j]; k++)
                    {
                        card_set[count] = new Card();
                        card_set[count].Number = j;
                        card_set[count].Type = Cardtype[i];
                        count++;
                    }
                }
            }
            if (i == 2)
            {
                for (int j = 0; j < tiao_index.Length; j++)
                {
                    for (int k = 0; k < tiao_index[j]; k++)
                    {
                        card_set[count] = new Card();
                        card_set[count].Number = j;
                        card_set[count].Type = Cardtype[i];
                        count++;
                    }
                }
            }
            if (i == 5)
            {
                for (int j = 0; j < flower_index.Length; j++)
                {
                    card_set[count] = new Card();
                    card_set[count].Number = j;
                    card_set[count].Type = Cardtype[i];
                    count++;
                }
            }
            if (i == 3)
            {
                for (int j = 0; j < wesn_index.Length; j++)
                {
                    for (int k = 0; k < wesn_index[j]; k++)
                    {
                        card_set[count] = new Card();
                        card_set[count].Number = j;
                        card_set[count].Type = Cardtype[i];
                        count++;
                    }
                }
            }
            if (i == 4)
            {
                for (int j = 0; j < mfb_index.Length; j++)
                {
                    for (int k = 0; k < mfb_index[j]; k++)
                    {
                        card_set[count] = new Card();
                        card_set[count].Number = j;
                        card_set[count].Type = Cardtype[i];
                        count++;
                    }
                }
            }
            //  Debug.Log(count);
        }
        Card Tempcard;

        for (int i = 0; i < card_set.Length; i++)
        {
            rsd = UnityEngine.Random.Range(0, 144);
            Tempcard = card_set[i];
            card_set[i] = card_set[rsd];
            card_set[rsd] = Tempcard;
            //csv.Save(i);
            //     Debug.Log(card_set[i].Number + " " + card_set[i].Type);
        }

    }  //生成144張亂數牌組
     

    public void Set_Card(Player player)//發出一張卡
    {
        if (cardleft != 0)
        {
           
            player.delivercard(card_set[cardleft].Number, card_set[cardleft].Type);
            //player.playerio.Save(player);
          
           /*if (player.M_key)
            {
                OpCard.Type = card_set[cardleft].Type;
                OpCard.Number = card_set[cardleft].Number;
                Debug.Log("Number:" + card_set[cardleft].Number + ",Type :" + card_set[cardleft].Type);
            }*/
            cardleft--;
        }
        else
        {
            Debug.Log("海底沒牌了");
            //之後流局判斷用
        }
    }


    public void InitialPlayer_card(Player playerE, Player playerS, Player playerW, Player playerN) //初始16張手牌
    {
        for (int i = 0; i < 16; i++)
        {
            Set_Card(playerE);
            Set_Card(playerS);
            Set_Card(playerW);
            Set_Card(playerN);
        }
        playerE.cs_sort();
        playerS.cs_sort();
        playerW.cs_sort();
        playerN.cs_sort();
       /* playerE.Render_card();
        playerS.Render_card();
        playerW.Render_card();
        playerN.Render_card();*/
    }


    public void ground_flower(Player player)
    {
        while (player.Grnd_flw())
        {
            while (player.handcard_index < 15)
            {
                Set_Card(player);
            }
        }


        player.cs_sort();
        player.Render_card(); 
    }  //補花

  /*  public bool Playerdrawcard(Player player)
    {
        if (cardleft == 0)
        {
            return true;
        }
        else if (cardleft != 0 && M_key == true)
        {
            Set_Card(player);
            while (player.Grnd_flw())
            {
                Set_Card(player);
            }
            M_key = false;
            player.M_key = true;
            return false;
        }
        else if (cardleft != 0 && M_key == false) { return false; }
        return true;
    }  //摸牌


        */
    /*public int findtype(string sType)
    {
        string[] Cardtype = { "wan", "ton", "tiao", "eswn", "mfb", "flower" };

        for (int i = 0; i < 7; i++)
        {
            if (Cardtype[i] == sType)
            {
                return i;
            }
        }
        return 0;
    }
    
    public bool ishu(Card[] player_handcards)
    {
        int countCards = player_handcards.Length;
        int[][] handcards = new int[4][] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        for (int i = 0; i < player_handcards.Length; i++)
        {
            //type为0,1,2,3则为万条筒字。   [type = 0,1,2,3 ,  0]代表每种牌型的总数量
            switch (findtype(player_handcards[i].Type))
            {
                case 0:
                    handcards[0][player_handcards[i].Number+1]++;
                    handcards[0][0]++;
                    break;
                case 1:
                    handcards[1][player_handcards[i].Number+1]++;
                    handcards[1][0]++;
                    break;
                case 2:
                    handcards[2][player_handcards[i].Number+1]++;
                    handcards[2][0]++;
                    break;
                case 3:
                    handcards[3][player_handcards[i].Number+1]++;
                    handcards[3][0]++;
                    break;
            }
        }

        bool isJiang = false;   //判断是否有对子
        int jiangNumber = -1;
        for (int i = 0; i < handcards.GetLength(0); i++)
        {
            if (handcards[i][0] % 3 == 2)
            {
                if (isJiang)
                {
                    return false;
                }
                isJiang = true;
                jiangNumber = i;
            }
            //因为对应四种牌型只能有一种且仅包含一个对子
        }
        for (int i = 0; i < handcards.GetLength(0); i++)
        {
            if (i != jiangNumber)
            {
                if (!(IsKanOrShun(handcards[i], i == 3)))
                {
                    return false;
                }
            }
        }

        bool success = false;
        //有将牌的情况下
        for (int i = 1; i <= 9; i++)
        {
            if (handcards[jiangNumber][i] >= 2)
            {
                handcards[jiangNumber][i] -= 2;
                handcards[jiangNumber][0] -= 2;
                if (IsKanOrShun(handcards[jiangNumber], jiangNumber == 3))
                {
                    success = true;
                    break;
                }
                else
                {
                    handcards[jiangNumber][i] += 2;
                    handcards[jiangNumber][0] += 2;
                }
            }
        }
        return success;

    }


    public static bool IsKanOrShun(int[] arr, bool isZi)//針對單一花色做處理
    {
        if (arr[0] == 0)
        {
            return true;
        }

        int index = -1;
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > 0)
            {
                index = i;
                break;
            }
        }
        bool result;
        //是否满足全是砍
        if (arr[index] >= 3)
        {
            arr[index] -= 3;
            arr[0] -= 3;
            result = IsKanOrShun(arr, isZi);
            arr[index] += 3;
            arr[0] += 3;
            return result;
        }
        //是否满足为顺子
        if (!isZi)
        {
            if (index < 8 && arr[index + 1] >= 1 && arr[index + 2] >= 1)
            {
                arr[index] -= 1;
                arr[index + 1] -= 1;
                arr[index + 2] -= 1;
                arr[0] -= 3;
                result = IsKanOrShun(arr, isZi);
                arr[index] += 1;
                arr[index + 1] += 1;
                arr[index + 2] += 1;
                arr[0] += 3;
                return result;
            }
        }

        return false;
    }*/

    public void ResetOpCared()
    {
        OpCard.Number = -1;
        OpCard.Type = null;
    }
    

    public  void  testplayer(Player playerE)
    {
        //歸零重發整副牌
        playerE.handcard_index = -1;
        playerE.delivercard(1, "wan");
        playerE.delivercard(7, "wan");
        playerE.delivercard(8, "wan");
        playerE.delivercard(1, "tiao");
        playerE.delivercard(2, "tiao");
        playerE.delivercard(3, "tiao");
        playerE.delivercard(4, "tiao");
        playerE.delivercard(5, "tiao");
        playerE.delivercard(6, "tiao");
        playerE.delivercard(4, "wan");
        playerE.delivercard(5, "wan");
        playerE.delivercard(6, "wan");
        playerE.delivercard(2, "mfb");
        playerE.delivercard(2, "mfb");
        playerE.delivercard(2, "mfb");
        playerE.delivercard(1, "eswn");
        //playerE.delivercard(1, "eswn");

        playerE.cs_sort();
        playerE.Render_card();
    }
    public void testplayerB(Player player)
    {
        //歸零重發整副牌
        player.handcard_index = -1;
        player.delivercard(1, "wan");
        player.delivercard(2, "wan");
        player.delivercard(3, "wan");
        player.delivercard(1, "tiao");
        player.delivercard(2, "tiao");
        player.delivercard(3, "tiao");
        player.delivercard(4, "tiao");
        player.delivercard(5, "tiao");
        player.delivercard(6, "tiao");
        player.delivercard(4, "wan");
        player.delivercard(5, "wan");
        player.delivercard(6, "wan");
        player.delivercard(0, "mfb");
        player.delivercard(0, "mfb");
        player.delivercard(0, "mfb");
        player.delivercard(1, "eswn");
        //playerE.delivercard(1, "eswn");

        player.cs_sort();
        player.Render_card();
    }
    /*public void PopOpcardFromPlayer()
    {
        Player A;
        A = PlayerSet[LastPlayer];
        A.Outhand_index--;
        A.outhand[A.Outhand_index].Type = null;
        A.outhand[A.Outhand_index].Number = -1;
       
    }*/
    public void RedrawPlayer(Player p)//更新上個出牌者之出牌區
    {
        //指到有資料的格子並刪除資料
        p.Outhand_index--;
        p.outhand[p.Outhand_index].Type=null;
        p.outhand[p.Outhand_index].Number = -1;
        p.cs_sort();
        p.Render_card();
    }
    
    
}



