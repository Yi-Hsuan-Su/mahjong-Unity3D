using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject btns,btns3d;
    public Shufflecard Shuffler;
    /*public PlayercardIO playerio;
    public sortplayercardio sortplayer;*/
    public UI m_UI;
    public CubeUI cubeui;
    public Card[] M_flowercard = new Card[25];
    public Card[] handcard = new Card[17];
    public Card[] outhand = new Card[25];

    public float timer = 0;
    public int ID;
    public int handcard_index;
    public int flwcard_index;
    public int Outhand_index;
    public int temp_cardindex;
    public int[] EatState, EatIndex, offset;
    public bool M_key = false, isLook = true;
    public bool IsWin = false, IsPon = false, IsEat = false, IsGon = false;

    int c = 0;
    void Start()
    {
        flwcard_index = 0;
        temp_cardindex = 0;
        Outhand_index = 0;
        EatState = new int[4];
        EatIndex = new int[2];
        offset = new int[2];
        //playerio = GetComponent<PlayercardIO>();
        Shuffler = GameObject.Find("shuffle").GetComponent<Shufflecard>();
        //cubeui= GameObject.Find("3DRender").GetComponent<CubeUI>();

        for (handcard_index = 0; handcard_index < 17; handcard_index++)
        {
            handcard[handcard_index] = new Card();
        }

        handcard_index = -1;


    }

    private void Update()
    {
        if (M_key == true)//玩家回合開始
        {
            DrawCard();

        }
        else//他人回合
        {


        }

        if (IsWin || IsEat || IsGon || IsPon)
        {
            btns.SetActive(true);
            btns3d.SetActive(true);
        }
        else
        {
            btns.SetActive(false);
            btns3d.SetActive(false);
        }


    }
    public void Check() //判斷可否吃碰槓胡公牌
    {
        if (Shuffler.key && ID != Shuffler.LastPlayer)
        {
            if (!isLook)
            {
                //做一個假空牌(tmpCard)把公牌跟手牌放入做判斷
                isLook = true;
                int tmpIndex = 0;
                tmpIndex = handcard_index;
                Card[] tmpCard = new Card[17];
                for (int i = 0; i <= tmpIndex; i++)
                {
                    tmpCard[i] = new Card();
                    tmpCard[i].Type = handcard[i].Type;
                    tmpCard[i].Number = handcard[i].Number;
                }

                tmpCard[tmpIndex] = new Card();
                tmpCard[tmpIndex].Type = Shuffler.OpCard.Type;
                tmpCard[tmpIndex].Number = Shuffler.OpCard.Number;
                /*Debug.Log("【當前公牌】花色:"+ tmpCard[tmpIndex].Type+"數字:" + tmpCard[tmpIndex].Number);
                Debug.Log("【tmpCard】長度:" + tmpCard.Length);*/

                //有沒有人滿足胡牌
                IsWin = ishu(tmpCard);
                if (IsWin)
                {
                    m_UI.button[0].GetComponentInChildren<Text>().color = Color.blue;
                    cubeui.button[0].GetComponentInChildren<Text>().color = Color.blue;
                }
                //沒人胡再看吃碰槓
                LookCard(tmpCard, Shuffler.OpCard);

            }

        }
    }
    public void delivercard(int num, string type)
    {
        handcard_index++;
        handcard[handcard_index].Type = type;
        handcard[handcard_index].Number = num;
    }  //發牌
    public bool Grnd_flw()
    {
        bool hasflower = false;
        //Debug.Log(handcard[15].Type);

        while (handcard[handcard_index].Type == "flower")
        {
            M_flowercard[flwcard_index] = new Card();
            M_flowercard[flwcard_index].Type = handcard[handcard_index].Type;
            M_flowercard[flwcard_index].Number = handcard[handcard_index].Number;
            handcard_index--;
            flwcard_index++;
            hasflower = true;
            if (handcard[handcard_index].Type != "flower") break;
        }
        //Debug.Log(hasflower);
        return hasflower;
    }  //補花

    public void DrawCard()//出牌控制
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            RealEatBtnOnClick();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            int sum = 0;
            int i = EatState[0];
            for (int j = 1; j < 4; j++)
            {
                sum += EatState[j];
            }
            //一種吃法與多種
            if (sum == 1)
            {
                ChoseEatBtnOnClick();
            }
            else
            {
                while (EatState[i] == 0 || i == EatState[0])
                {
                    i++;
                    if (i > 3)
                    {
                        i = i % 3;
                    }
                }
                EatState[0] = i;
                EatBtnOnClick(EatState[0]);
            }

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (temp_cardindex < 16)
            {
                int tmp;
                temp_cardindex++;
                tmp = temp_cardindex + 1;
                m_UI.Handcardpoint(temp_cardindex);
                cubeui.Handcardpoint(temp_cardindex);
                Debug.Log(tmp);

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (temp_cardindex > 0)
            {
                int tmp;
                temp_cardindex--;
                tmp = temp_cardindex + 1;
                m_UI.Handcardpoint(temp_cardindex);
                cubeui.Handcardpoint(temp_cardindex);
                Debug.Log(tmp);


            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (handcard_index % 3 == 1)
            {
                outhand[Outhand_index] = new Card();
                outhand[Outhand_index].Type = handcard[temp_cardindex].Type;
                outhand[Outhand_index].Number = handcard[temp_cardindex].Number;
                handcard[temp_cardindex].Type = null;
                Shuffler.OpCard.Type = outhand[Outhand_index].Type;
                Shuffler.OpCard.Number = outhand[Outhand_index].Number;
                temp_cardindex = 0;
                handcard_index--;
                Outhand_index++;

                M_key = false;
                Shuffler.key = true;
                Shuffler.LastPlayer = this.ID;
                //Shuffler.PlayerIndex = -1;
                Shuffler.ReviewLook();

                Debug.Log("【當前公牌】花色:" + Shuffler.OpCard.Type + "數字:" + (Shuffler.OpCard.Number + 1));

                cs_sort();
                Render_card();
                timer = 0;
                ResetFuncBtn();

            }
            else
            {
                Debug.Log("張數不對，無法出牌");
            }

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (handcard_index % 3 == 0)
            {
                ResetFuncBtn();
                Shuffler.Set_Card(this);
                cs_sort();

                Grnd_flw();
                IsWin = ishuV0(handcard);

                /* if (Shuffler.OpCard.Type!=null)
                  {
                      LookCard(handcard, Shuffler.OpCard);
                  }*/

                Render_card();
            }
            else
            {
                Debug.Log("張數不對，無法摸牌");
            }

        }

    }

    public void cs_sort()  // 排序牌型
    {
        System.Array.Sort(handcard);

    }
    public int findtype(string sType)
    {
        string[] Cardtype = { "wan", "ton", "tiao", "eswn", "mfb", "flower" };

        for (int i = 0; i < 7; i++)
        {
            if (Cardtype[i] == sType)
            {

                return i;
            }
        }
        return -1;
    }
    //原版胡牌
    public bool ishuV0(Card[] player_handcards)
    {
        int countCards = handcard_index;
        int[][] handcards = new int[4][] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        for (int i = 0; i < countCards; i++)
        {
            //type为0,1,2,3则为万条筒字。   [type = 0,1,2,3 ,  0]代表每种牌型的总数量
            switch (findtype(player_handcards[i].Type))
            {
                case 0:
                    handcards[0][player_handcards[i].Number + 1]++;
                    handcards[0][0]++;
                    break;
                case 1:
                    handcards[1][player_handcards[i].Number + 1]++;
                    handcards[1][0]++;
                    break;
                case 2:
                    handcards[2][player_handcards[i].Number + 1]++;
                    handcards[2][0]++;
                    break;
                case 3:
                    handcards[3][player_handcards[i].Number + 1]++;
                    handcards[3][0]++;
                    break;
                case 4:
                    handcards[3][player_handcards[i].Number + 5]++;
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
                // Debug.Log("眼牌花色:"+ jiangNumber );
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
    public bool ishu(Card[] player_handcards)
    {
        int countCards = handcard_index + 1;
        int[][] handcards = new int[4][] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        for (int i = 0; i < countCards; i++)
        {
            //type为0,1,2,3则为万条筒字。   [type = 0,1,2,3 ,  0]代表每种牌型的总数量
            switch (findtype(player_handcards[i].Type))
            {
                case 0:
                    handcards[0][player_handcards[i].Number + 1]++;
                    handcards[0][0]++;
                    break;
                case 1:
                    handcards[1][player_handcards[i].Number + 1]++;
                    handcards[1][0]++;
                    break;
                case 2:
                    handcards[2][player_handcards[i].Number + 1]++;
                    handcards[2][0]++;
                    break;
                case 3:
                    handcards[3][player_handcards[i].Number + 1]++;
                    handcards[3][0]++;
                    break;
                case 4:
                    handcards[3][player_handcards[i].Number + 5]++;
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
                // Debug.Log("眼牌花色:"+ jiangNumber );
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
    }
    public void LookCard(Card[] player_handcards, Card OpCard)//player_handcards包含公牌了
    {
        Debug.Log("【" + this.ID + "】:判斷可否碰槓吃");
        if (OpCard.Type != "flower")
        {

            int countCards = handcard_index;

            int[][] handcards = new int[4][] { new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
            for (int i = 0; i <= countCards; i++)
            {

                //type为0,1,2,3则为万条筒字。   [type = 0,1,2,3 ,  0]代表每种牌型的总数量
                switch (findtype(player_handcards[i].Type))
                {
                    case 0:
                        handcards[0][player_handcards[i].Number + 1]++;
                        handcards[0][0]++;
                        break;
                    case 1:
                        handcards[1][player_handcards[i].Number + 1]++;
                        handcards[1][0]++;
                        break;
                    case 2:
                        handcards[2][player_handcards[i].Number + 1]++;
                        handcards[2][0]++;
                        break;
                    case 3:
                        handcards[3][player_handcards[i].Number + 1]++;
                        handcards[3][0]++;
                        break;
                    case 4:
                        handcards[3][player_handcards[i].Number + 5]++;
                        handcards[3][0]++;
                        break;
                }
            }

            //公牌中發白補到字區
            int tmpType = -1;
            int tmpNum = -1;
            tmpType = findtype(OpCard.Type);
            tmpNum = OpCard.Number + 1;
            if (findtype((OpCard.Type)) == 4)
            {
                tmpType = 3;
                tmpNum = OpCard.Number + 5;
            }
            else
            {
                
            }



            //從海底進一張同時滿足三張相同
            if (handcards[tmpType][tmpNum] == 3)
            {
                IsPon = true;
                m_UI.button[1].GetComponentInChildren<Text>().color = Color.blue;
                cubeui.button[1].GetComponentInChildren<Text>().color = Color.blue;
                Debug.Log("可碰 :" + handcards[tmpType][tmpNum]);
            }
            //從海底進一張同時滿足四張相同
            if (handcards[tmpType][tmpNum] > 3)
            {
                IsGon = true;
                m_UI.button[2].GetComponentInChildren<Text>().color = Color.blue;
                cubeui.button[2].GetComponentInChildren<Text>().color = Color.blue;
                Debug.Log("可槓 :" + handcards[tmpType][tmpNum]);
            }

            //是下家、不能是字、最後牌數夠吃牌 
            //EatState[0] > 現在使用哪種吃法 ,[1] >

            if (tmpType < 3 && (Shuffler.PlayerIndex + 1) % 4 == this.ID)
            {

                for (int i = 0; i < 4; i++)
                {
                    EatState[i] = 0;
                }

                if (tmpNum < 9 && tmpNum > 1)
                {
                    if (handcards[tmpType][tmpNum + 1] > 0 && handcards[tmpType][tmpNum - 1] > 0)
                    {
                        IsEat = true;
                        EatState[2] = 1;
                    }
                }
                if (tmpNum == 1)
                {
                    if ((handcards[tmpType][tmpNum + 1] > 0 && handcards[tmpType][tmpNum + 2] > 0))
                    {
                        IsEat = true;
                        EatState[1] = 1;
                    }
                }
                if (tmpNum == 9)
                {
                    if ((handcards[tmpType][tmpNum - 1] > 0 && handcards[tmpType][tmpNum - 2] > 0))
                    {
                        IsEat = true;
                        EatState[3] = 1;
                    }
                }



                if (IsEat)
                {
                    Debug.Log("可吃");
                    m_UI.button[0].GetComponentInChildren<Text>().color = Color.blue;
                    cubeui.button[0].GetComponentInChildren<Text>().color = Color.blue;

                }
            }

        }


    }
    public void Render_card()   //生成UI
    {
        m_UI.Render_handCard(handcard, handcard_index);
        m_UI.OutRenderCard(outhand, Outhand_index);
        m_UI.LighthandCard(M_flowercard, flwcard_index);//吃碰區

        
        //3d layout.
        cubeui.Render(handcard, handcard_index);
        cubeui.Renderlight(M_flowercard, flwcard_index);
        cubeui.Renderout(outhand, Outhand_index);
        cubeui.RenderRotate(ID);
     
    }
    public void PonBtnOnClick()
    {
        Debug.Log("按下碰");
        if (IsPon)
        {
            IsPon = false;
            //碰優先權
            Shuffler.PlayerIndex = this.ID;
            Shuffler.CyclePlay();

            //重置燈號
            ResetFuncBtn();

            //公牌插入手牌中並清除上一位玩家手中的牌
            handcard_index++;
            handcard[handcard_index].Type = Shuffler.OpCard.Type;
            handcard[handcard_index].Number = Shuffler.OpCard.Number;
            //Shuffler.PopOpcardFromPlayer();

            //玩家去除相同的牌並放入亮牌區
            int count = 0;
            int length = handcard_index + 1;
            for (int i = 0; i < length; i++)
            {
                if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number && count < 3)
                {

                    M_flowercard[flwcard_index] = new Card();
                    M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
                    M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number;
                    flwcard_index++;

                    handcard[i].Type = null;
                    handcard[i].Number = -1;
                    handcard_index--;

                    count++;

                }
            }

            M_key = true;
            Shuffler.RedrawPlayer(Shuffler.PlayerSet[Shuffler.LastPlayer]);
            Shuffler.ResetOpCared();
            cs_sort();
            Render_card();
        }


    }
    public void GonBtnOnClick()
    {
        Debug.Log("按下槓");
        if (IsGon)
        {
            IsGon = false;

            //槓優先權
            Shuffler.PlayerIndex = this.ID;
            Shuffler.CyclePlay();

            //重置燈號
            ResetFuncBtn();

            //公牌插入手牌中並清除上一位玩家手中的牌
            handcard_index++;
            handcard[handcard_index].Type = Shuffler.OpCard.Type;
            handcard[handcard_index].Number = Shuffler.OpCard.Number;
            //Shuffler.PopOpcardFromPlayer();

            //玩家去除相同的牌並放入亮牌區
            int count = 0;
            int length = handcard_index + 1;
            for (int i = 0; i < length; i++)
            {
                if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number && count < 4)
                {

                    M_flowercard[flwcard_index] = new Card();
                    M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
                    M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number;
                    flwcard_index++;

                    handcard[i].Type = null;
                    handcard[i].Number = -1;
                    handcard_index--;

                    count++;

                }
            }

            M_key = true;
            Shuffler.RedrawPlayer(Shuffler.PlayerSet[Shuffler.LastPlayer]);
            Shuffler.ResetOpCared();
            cs_sort();
            Render_card();
        }


    }
    public void EatBtnOnClick(int index)
    {
        Debug.Log("按下吃");


        int j = 0, k = 0;
        IsEat = false;

        //吃優先權
        Shuffler.PlayerIndex = this.ID;
        Shuffler.CyclePlay();

        //重置燈號
        ResetFuncBtn();

        //玩家去除相同的牌並放入亮牌區
        int count = 0;
        int length = handcard_index;

        //向上選出一個可以吃的方法

        //根據哪種吃法，給偏移量
        int[] offset = { 0, 0 };
        switch (index)
        {
            case 1:
                offset[0] = 1;
                offset[1] = 2;
                break;
            case 2:
                offset[0] = -1;
                offset[1] = 1;
                break;
            case 3:
                offset[0] = -1;
                offset[1] = -2;
                break;

        }


        for (int i = 0; i < length; i++)
        {
            if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number + offset[0])
            {
                j = i;
            }
            if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number + offset[1])
            {
                k = i;
            }
        }

        M_key = true;
        cs_sort();
        Render_card();
        EatIndex[0] = j;
        EatIndex[1] = k;
        m_UI.HandcardEatpoint(j, k);
         cubeui.HandcardEatpoint(j, k);
        Debug.Log("j,k:" + j + "," + k + ",offset[]:" + offset[0] + "," + offset[1]);
        count++;


    }
    public void EatBtnOnClick()
    {
        Debug.Log("按下吃");
        if (IsEat)
        {
            int j = 0, k = 0;
            IsEat = false;

            //吃優先權
            Shuffler.PlayerIndex = this.ID;
            Shuffler.CyclePlay();

            //重置燈號
            ResetFuncBtn();

            //玩家去除相同的牌並放入亮牌區
            int count = 0;
            int length = handcard_index;

            //向上選出一個可以吃的方法
            int tmp = 0;//沒有指出任一個吃法 [0] -> 現在用哪個吃法
            while (EatState[tmp] == 0)
            {
                tmp++;
            }
            EatState[0] = tmp;
            //根據哪種吃法，給偏移量

            switch (tmp)
            {
                case 1:
                    offset[0] = 1;
                    offset[1] = 2;
                    break;
                case 2:
                    offset[0] = -1;
                    offset[1] = 1;
                    break;
                case 3:
                    offset[0] = -1;
                    offset[1] = -2;
                    break;
              
                  
            }


            for (int i = 0; i < length; i++)
            {
                if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number + offset[0])
                {
                    j = i;
                }
                if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number + offset[1])
                {
                    k = i;
                }
            }

            M_key = true;
            cs_sort();
            Render_card();
            EatIndex[0] = j;
            EatIndex[1] = k;
            m_UI.HandcardEatpoint(j, k);
            cubeui.HandcardEatpoint(j, k);
            Debug.Log("j,k:" + j + "," + k + ",offset[]:" + offset[0] + "," + offset[1]);
            count++;

        }
    }
    public void RealEatBtnOnClick()
    {
        Debug.Log("按下吃");
        if (!IsEat)
        {
            IsEat = false;

            //吃優先權
            Shuffler.PlayerIndex = this.ID;
            Shuffler.CyclePlay();

            //重置燈號
            ResetFuncBtn();

            M_flowercard[flwcard_index] = new Card();
            M_flowercard[flwcard_index].Type = handcard[EatIndex[0]].Type;
            M_flowercard[flwcard_index].Number = handcard[EatIndex[0]].Number;
            flwcard_index++;


            M_flowercard[flwcard_index] = new Card();
            M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
            M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number;
            flwcard_index++;

            M_flowercard[flwcard_index] = new Card();
            M_flowercard[flwcard_index].Type = handcard[EatIndex[1]].Type;
            M_flowercard[flwcard_index].Number = handcard[EatIndex[1]].Number;
            flwcard_index++;

            handcard[EatIndex[0]].Type = null;
            handcard[EatIndex[0]].Number = -1;
            handcard_index--;

            handcard[EatIndex[1]].Type = null;
            handcard[EatIndex[1]].Number = -1;
            handcard_index--;

            M_key = true;
            Shuffler.RedrawPlayer(Shuffler.PlayerSet[Shuffler.LastPlayer]);
            Shuffler.ResetOpCared();
            cs_sort();
            Render_card();
        }


    }
    public void ChoseEatBtnOnClick()
    {
        Debug.Log("按下吃");
        if (IsEat)
        {
            IsEat = false;

            //吃優先權
            Shuffler.PlayerIndex = this.ID;
            Shuffler.CyclePlay();

            //重置燈號
            ResetFuncBtn();

            //公牌插入手牌中並清除上一位玩家手中的牌
            handcard_index++;
            handcard[handcard_index].Type = Shuffler.OpCard.Type;
            handcard[handcard_index].Number = Shuffler.OpCard.Number;
            //Shuffler.PopOpcardFromPlayer();

            //玩家去除相同的牌並放入亮牌區
            int count = 0;
            int length = handcard_index + 1;
            for (int i = 0; i < length; i++)
            {
                if (handcard[i].Type == Shuffler.OpCard.Type && handcard[i].Number == Shuffler.OpCard.Number && count == 0)
                {
                    //吃的牌要放中間

                    //哪種吃法

                    //手牌一張來吃
                    int j = i;
                    while (handcard[j].Type == Shuffler.OpCard.Type && handcard[j].Number == Shuffler.OpCard.Number - 1)
                    {
                        j--;
                    }
                    M_flowercard[flwcard_index] = new Card();
                    M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
                    M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number - 1;
                    flwcard_index++;

                    handcard[j].Type = null;
                    handcard[j].Number = -1;
                    handcard_index--;

                    //吃公牌
                    M_flowercard[flwcard_index] = new Card();
                    M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
                    M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number;
                    flwcard_index++;
                    handcard[i].Type = null;
                    handcard[i].Number = -1;
                    handcard_index--;

                    //拿出手牌來吃
                    int k = i;
                    while (handcard[k].Type == Shuffler.OpCard.Type && handcard[k].Number == Shuffler.OpCard.Number + 1)
                    {
                        k++;
                    }

                    M_flowercard[flwcard_index] = new Card();
                    M_flowercard[flwcard_index].Type = Shuffler.OpCard.Type;
                    M_flowercard[flwcard_index].Number = Shuffler.OpCard.Number + 1;
                    flwcard_index++;

                    handcard[k].Type = null;
                    handcard[k].Number = -1;
                    handcard_index--;

                    count++;

                }
            }

            M_key = true;
            Shuffler.RedrawPlayer(Shuffler.PlayerSet[Shuffler.LastPlayer]);
            Shuffler.ResetOpCared();
            cs_sort();
            Render_card();
        }


    }
    public void ResetFuncBtn()
    {
        for (int i = 0; i < m_UI.button.Length; i++)
        {
            IsEat = IsGon = IsWin = IsPon = false;
            m_UI.button[i].GetComponentInChildren<Text>().color = Color.black;

        }
        for (int i = 0; i < cubeui.button.Length; i++)
        {

            cubeui.button[i].GetComponentInChildren<Text>().color = Color.black;
        }

    }

    public void HuOnclick()//如果滿足胡牌的條件，按下胡牌的狀況。
    {
        Shuffler.isgameover = true;

    }
}
