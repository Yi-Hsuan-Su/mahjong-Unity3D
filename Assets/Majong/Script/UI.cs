using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public GameObject Handout,Handlight;
    private int length, outlength;
    public int top = 0, jtop = 0 ,ktop=0;
    public Image img;
    Image[] layoutImg, layoutImgOut, layoutLightImg;
    public Sprite[] p, p2;
    public Sprite tmp;
    string[] type = { "wan", "ton", "tiao", "eswn", "mfb", "flower" };
    public Button[] button = new Button[4]; //吃、碰、槓、胡 

    public Sprite[] sp;
    void Start()
    {
        // Handout=  GameObject.Find("HandoutRender");
        LoadingSprite();
    }
    // Update is called once per frame
    void Update()
    {

    }

    //載入圖片元素&創建表格(大小).
    public void LoadingSprite()
    {
        //把所有子圖拉到p陣列
        p = Resources.LoadAll<Sprite>("P");
        p2 = Resources.LoadAll<Sprite>("P2");

    }
    public void CreateImg(int size)
    {

         layoutImg = new Image[size];
        for (int i = 0; i < layoutImg.Length; i++)
        {
            //生img模塊
            layoutImg[i] = Instantiate(img, this.gameObject.transform);
            //layoutImg[i].sprite  = p[i];
            //Debug.Log(layoutImg.Length);
        }
    }

    public void CreateOutImg(int size)
    {

        layoutImgOut = new Image[size];
        for (int i = 0; i < layoutImgOut.Length; i++)
        {
            //生img模塊
            layoutImgOut[i] = Instantiate(img, Handout.transform);
          
        }
    }
    public void CreateLightImg(int size)
    {

        layoutLightImg = new Image[size];
        for (int i = 0; i < layoutLightImg.Length; i++)
        {
            //生img模塊
            layoutLightImg[i] = Instantiate(img, Handlight.transform);

        }
    }
    #region 判斷一隻牌的內容並畫出對應牌的圖(花色,數字).
    public void Render(string type, int num)
    {
        tmp = null;
        //對應表:"ton", "tiao", "wan", "flower", "wesn", "mfb"
        switch (type)
        {
            case "wan":
                switch (num)
                {
                    case 0:
                        tmp = p[0];
                        break;
                    case 1:
                        tmp = p[1];
                        break;
                    case 2:
                        tmp = p[2];
                        break;
                    case 3:
                        tmp = p[3];
                        break;
                    case 4:
                        tmp = p[4];
                        break;
                    case 5:
                        tmp = p[5];
                        break;
                    case 6:
                        tmp = p[6];
                        break;
                    case 7:
                        tmp = p[7];
                        break;
                    case 8:
                        tmp = p[8];
                        break;
                }
                break;
            case "ton":
                switch (num)
                {
                    case 0:
                        tmp = p[9];
                        break;
                    case 1:
                        tmp = p[10];
                        break;
                    case 2:
                        tmp = p[11];
                        break;
                    case 3:
                        tmp = p[12];
                        break;
                    case 4:
                        tmp = p[13];
                        break;
                    case 5:
                        tmp = p[14];
                        break;
                    case 6:
                        tmp = p[15];
                        break;
                    case 7:
                        tmp = p[16];
                        break;
                    case 8:
                        tmp = p[17];
                        break;
                }
                break;
            case "tiao":
                switch (num)
                {
                    case 0:
                        tmp = p[18];
                        break;
                    case 1:
                        tmp = p[19];
                        break;
                    case 2:
                        tmp = p[20];
                        break;
                    case 3:
                        tmp = p[21];
                        break;
                    case 4:
                        tmp = p[22];
                        break;
                    case 5:
                        tmp = p[23];
                        break;
                    case 6:
                        tmp = p[24];
                        break;
                    case 7:
                        tmp = p[25];
                        break;
                    case 8:
                        tmp = p[26];
                        break;
                }
                break;
            case "eswn":
                switch (num)
                {
                    case 0:
                        tmp = p[27];
                        break;
                    case 1:
                        tmp = p[28];
                        break;
                    case 2:
                        tmp = p[29];
                        break;
                    case 3:
                        tmp = p[30];
                        break;

                }
                break;
            case "mfb":
                switch (num)
                {
                    case 0:
                        tmp = p[31];
                        break;
                    case 1:
                        tmp = p[32];
                        break;
                    case 2:
                        tmp = p[33];
                        break;

                }
                break;
            case "flower":
                switch (num)
                {
                    case 0:
                        tmp = p2[0];
                        break;
                    case 1:
                        tmp = p2[1];
                        break;
                    case 2:
                        tmp = p2[2];
                        break;
                    case 3:
                        tmp = p2[3];
                        break;
                    case 4:
                        tmp = p2[4];
                        break;
                    case 5:
                        tmp = p2[5];
                        break;
                    case 6:
                        tmp = p2[6];
                        break;
                    case 7:
                        tmp = p2[7];
                        break;

                }
                break;

        }

        layoutImg[top].sprite = tmp;
        tmp = null;
        top++;
    }
    public void RenderOut(string type, int num)
    {
        tmp = null;
        //對應表:"ton", "tiao", "wan", "flower", "wesn", "mfb"
        switch (type)
        {
            case "wan":
                switch (num)
                {
                    case 0:
                        tmp = p[0];
                        break;
                    case 1:
                        tmp = p[1];
                        break;
                    case 2:
                        tmp = p[2];
                        break;
                    case 3:
                        tmp = p[3];
                        break;
                    case 4:
                        tmp = p[4];
                        break;
                    case 5:
                        tmp = p[5];
                        break;
                    case 6:
                        tmp = p[6];
                        break;
                    case 7:
                        tmp = p[7];
                        break;
                    case 8:
                        tmp = p[8];
                        break;
                }
                break;
            case "ton":
                switch (num)
                {
                    case 0:
                        tmp = p[9];
                        break;
                    case 1:
                        tmp = p[10];
                        break;
                    case 2:
                        tmp = p[11];
                        break;
                    case 3:
                        tmp = p[12];
                        break;
                    case 4:
                        tmp = p[13];
                        break;
                    case 5:
                        tmp = p[14];
                        break;
                    case 6:
                        tmp = p[15];
                        break;
                    case 7:
                        tmp = p[16];
                        break;
                    case 8:
                        tmp = p[17];
                        break;
                }
                break;
            case "tiao":
                switch (num)
                {
                    case 0:
                        tmp = p[18];
                        break;
                    case 1:
                        tmp = p[19];
                        break;
                    case 2:
                        tmp = p[20];
                        break;
                    case 3:
                        tmp = p[21];
                        break;
                    case 4:
                        tmp = p[22];
                        break;
                    case 5:
                        tmp = p[23];
                        break;
                    case 6:
                        tmp = p[24];
                        break;
                    case 7:
                        tmp = p[25];
                        break;
                    case 8:
                        tmp = p[26];
                        break;
                }
                break;
            case "eswn":
                switch (num)
                {
                    case 0:
                        tmp = p[27];
                        break;
                    case 1:
                        tmp = p[28];
                        break;
                    case 2:
                        tmp = p[29];
                        break;
                    case 3:
                        tmp = p[30];
                        break;

                }
                break;
            case "mfb":
                switch (num)
                {
                    case 0:
                        tmp = p[31];
                        break;
                    case 1:
                        tmp = p[32];
                        break;
                    case 2:
                        tmp = p[33];
                        break;

                }
                break;
            case "flower":
                switch (num)
                {
                    case 0:
                        tmp = p2[0];
                        break;
                    case 1:
                        tmp = p2[1];
                        break;
                    case 2:
                        tmp = p2[2];
                        break;
                    case 3:
                        tmp = p2[3];
                        break;
                    case 4:
                        tmp = p2[4];
                        break;
                    case 5:
                        tmp = p2[5];
                        break;
                    case 6:
                        tmp = p2[6];
                        break;
                    case 7:
                        tmp = p2[7];
                        break;

                }
                break;

        }

        layoutImgOut[jtop].sprite = tmp;
        tmp = null;
        jtop++;
    }
    public void RenderLightt(string type, int num)
    {
        tmp = null;
        //對應表:"ton", "tiao", "wan", "flower", "wesn", "mfb"
        switch (type)
        {
            case "wan":
                switch (num)
                {
                    case 0:
                        tmp = p[0];
                        break;
                    case 1:
                        tmp = p[1];
                        break;
                    case 2:
                        tmp = p[2];
                        break;
                    case 3:
                        tmp = p[3];
                        break;
                    case 4:
                        tmp = p[4];
                        break;
                    case 5:
                        tmp = p[5];
                        break;
                    case 6:
                        tmp = p[6];
                        break;
                    case 7:
                        tmp = p[7];
                        break;
                    case 8:
                        tmp = p[8];
                        break;
                }
                break;
            case "ton":
                switch (num)
                {
                    case 0:
                        tmp = p[9];
                        break;
                    case 1:
                        tmp = p[10];
                        break;
                    case 2:
                        tmp = p[11];
                        break;
                    case 3:
                        tmp = p[12];
                        break;
                    case 4:
                        tmp = p[13];
                        break;
                    case 5:
                        tmp = p[14];
                        break;
                    case 6:
                        tmp = p[15];
                        break;
                    case 7:
                        tmp = p[16];
                        break;
                    case 8:
                        tmp = p[17];
                        break;
                }
                break;
            case "tiao":
                switch (num)
                {
                    case 0:
                        tmp = p[18];
                        break;
                    case 1:
                        tmp = p[19];
                        break;
                    case 2:
                        tmp = p[20];
                        break;
                    case 3:
                        tmp = p[21];
                        break;
                    case 4:
                        tmp = p[22];
                        break;
                    case 5:
                        tmp = p[23];
                        break;
                    case 6:
                        tmp = p[24];
                        break;
                    case 7:
                        tmp = p[25];
                        break;
                    case 8:
                        tmp = p[26];
                        break;
                }
                break;
            case "eswn":
                switch (num)
                {
                    case 0:
                        tmp = p[27];
                        break;
                    case 1:
                        tmp = p[28];
                        break;
                    case 2:
                        tmp = p[29];
                        break;
                    case 3:
                        tmp = p[30];
                        break;

                }
                break;
            case "mfb":
                switch (num)
                {
                    case 0:
                        tmp = p[31];
                        break;
                    case 1:
                        tmp = p[32];
                        break;
                    case 2:
                        tmp = p[33];
                        break;

                }
                break;
            case "flower":
                switch (num)
                {
                    case 0:
                        tmp = p2[0];
                        break;
                    case 1:
                        tmp = p2[1];
                        break;
                    case 2:
                        tmp = p2[2];
                        break;
                    case 3:
                        tmp = p2[3];
                        break;
                    case 4:
                        tmp = p2[4];
                        break;
                    case 5:
                        tmp = p2[5];
                        break;
                    case 6:
                        tmp = p2[6];
                        break;
                    case 7:
                        tmp = p2[7];
                        break;

                }
                break;

        }

        layoutLightImg[ktop].sprite = tmp;
        tmp = null;
        ktop++;
    }
    #endregion
    public void Render_handCard(Card[] handcard,int index)//手牌
    {
        top = 0;
        index++;
        if (layoutImg != null)
        {
            for (int j = 0; j <layoutImg.Length; j++)
            {
                Destroy(layoutImg[j].gameObject);
            }
        }
        CreateImg(index);
        for (int i = 0; i < index; i++)
        {
            
            Render(handcard[i].Type, handcard[i].Number);
        }
    }
    public void OutRenderCard(Card[] outhand,int index)//個人海底
    {
        jtop = 0;

        //1.刪除原有容器
        if(layoutImgOut!=null)
        {
            for (int j = 0; j < layoutImgOut.Length; j++)
            {
                Destroy(layoutImgOut[j].gameObject);
            
            }
        }
  

        //2.根據新的大小創造容器
        CreateOutImg(index);
      
        //3.容器裝入資料
        for (int i = 0; i <index; i++)
        {
            RenderOut(outhand[i].Type, outhand[i].Number);
        }
    }
    public void LighthandCard(Card[] handlight,int index)//亮牌區
    {
        ktop = 0;
        if (layoutLightImg != null)
        {
            for (int j = 0; j < layoutLightImg.Length; j++)
            {
                Destroy(layoutLightImg[j].gameObject);

            }
        }
        CreateLightImg(index);
        for (int i = 0; i < index; i++)
        {
            
            RenderLightt(handlight[i].Type, handlight[i].Number);
        }
    }
    public void Handcardpoint(int point)
    {

        for(int i=0;i<layoutImg.Length;i++)
        {
            layoutImg[i].color = Color.white;
        }
        layoutImg[point].color = Color.blue;
    }
    public void HandcardEatpoint(int a,int b)
    {

        for (int i = 0; i < layoutImg.Length; i++)
        {
            layoutImg[i].color = Color.gray;
        }
        layoutImg[a].color = Color.green;
        layoutImg[b].color = Color.green;
       // layoutImg[c].color = Color.green;
        
    }
    
}
