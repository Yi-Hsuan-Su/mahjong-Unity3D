using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeUI : MonoBehaviour
{
    public GameObject card;//預設3D牌
    public GameObject birthPoint,lightbirth,outbirth;//出生點
    public GameObject[] handcardlayout, lightcardlayout,outcardlayout;//,outcardlayout;//手牌區
    public Sprite[] resource, resource2;//[1] > 花牌 ,[0] > 其他.
    public Button[] button = new Button[4]; //吃、碰、槓、胡 
    public SpriteRenderer tmpRender;
    public bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        
        LoadingSprite();
        
    }
    
    // 載入圖片元素.
    public void LoadingSprite()
    {
        //把所有子圖拉到p陣列
        resource = Resources.LoadAll<Sprite>("P");
        resource2 = Resources.LoadAll<Sprite>("P2");
    }
    // 為手牌創一牌3D牌容器，花色為預設.
    public void Create3DCard(int size)
    {
        birthPoint.transform.eulerAngles=new Vector3(0,0,0);
        handcardlayout = new GameObject[size];
        for (int i=0; i<size;i++)
        {
            handcardlayout[i]=Instantiate(card, birthPoint.transform);
            handcardlayout[i].transform.position += new Vector3(0.21f * i, 0, 0);
        }
    }
    
    // 為亮牌創一牌3D牌容器，花色為預設.
    public void Create3DlightCard(int size)
    {
        lightbirth.transform.eulerAngles = new Vector3(0, 0, 0);
        //Transform birTran = birthPoint.transform;
        lightcardlayout = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            lightcardlayout[i] = Instantiate(card, lightbirth.transform);
            lightcardlayout[i].transform.position += new Vector3(0.21f * i, 0, 0);
        }
    }
    public void Create3DoutCard(int size)
    {
        outbirth.transform.eulerAngles = new Vector3(0, 0, 0);
        //Transform birTran = birthPoint.transform;
        outcardlayout = new GameObject[size];
        for (int i = 0; i < size; i++)
        {
            outcardlayout[i] = Instantiate(card, outbirth.transform);
            outcardlayout[i].transform.position += new Vector3(0.21f * i, 0, 0);
        }
    }
    // 幫牌組裡的某一格選圖貼上
    public void Render(GameObject CardSet,string type, int num)
    {
        Sprite tmp =null;
        Sprite[] p = resource;
        Sprite[] p2 = resource2;
        tmpRender = CardSet.GetComponentInChildren<SpriteRenderer>();
        //Debug.Log(type+","+num);
      
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
                    default:
                        tmp = null;
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

        
        tmpRender.sprite = tmp;
    }

    // 
    public void Render(Card[] handcard, int index)//手牌
    {
        index++;
        if (handcardlayout != null)
        {
            for (int j = 0; j < handcardlayout.Length; j++)
            {
                Destroy(handcardlayout[j]);
            }
        }
       
        Create3DCard(index);
        for (int i = 0; i < index; i++)
        {
            Render(handcardlayout[i], handcard[i].Type, handcard[i].Number );
        }
      
    }
    
    public void Renderlight(Card[] handcard, int index)//手牌
    {
       
        if (lightcardlayout != null)
        {
            for (int j = 0; j < lightcardlayout.Length; j++)
            {
                Destroy(lightcardlayout[j]);
            }
        }
        // CreateImg(index);
        Create3DlightCard(index);
        for (int i = 0; i < index; i++)
        {
            Render(lightcardlayout[i], handcard[i].Type, handcard[i].Number);
        }

    }

    public void Renderout(Card[] handcard, int index)//手牌
    {

        if (outcardlayout != null)
        {
            for (int j = 0; j < outcardlayout.Length; j++)
            {
                Destroy(outcardlayout[j]);
            }
        }
        // CreateImg(index);
        Create3DoutCard(index);
        for (int i = 0; i < index; i++)
        {
            Render(outcardlayout[i], handcard[i].Type, handcard[i].Number);
        }

    }

    public void RenderRotate(int id)
    {

        if(birthPoint.transform.eulerAngles.y != 90 * (4 - id))
        {
          
            birthPoint.transform.Rotate(0, 90 * (4 - id), 0);
            outbirth.transform.Rotate(90, 90 * (4 - id), 0);
            lightbirth.transform.Rotate(90, 90 * (4 - id), 0);
            //outbirth.transform.position += new Vector3(1, 0, 0);
            //lightbirth.transform.Rotate(90, 90 * (4 - id), 0);
        }

        Debug.Log(id+":" + birthPoint.transform.eulerAngles.y);
        //lightbirth.transform.rotation = Quaternion.LookRotation(new Vector3(0,-1,0), Vector3.up);
        //outbirth.transform.rotation = Quaternion.LookRotation(new Vector3(0, -1, 0), Vector3.up);
       
    }
    public void Handcardpoint(int point)
    {
        float tmph = handcardlayout[0].transform.position.y;

        for (int i = 0; i < handcardlayout.Length; i++)
        {
            if (handcardlayout[i].transform.position.y >= 0.17f)
            {
                Transform tmp= handcardlayout[i].transform;
                tmp.position = new  Vector3(tmp.position.x,tmph,tmp.position.z) ;
            }
        }
        
        handcardlayout[point].transform.position += new Vector3(0,0.1f,0);
      
    }

    public void HandcardEatpoint(int a, int b)
    {

        for (int i = 0; i < handcardlayout.Length; i++)
        {
            handcardlayout[i].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        }
        handcardlayout[a].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        handcardlayout[b].GetComponentInChildren<SpriteRenderer>().color = Color.gray;
       
        // layoutImg[c].color = Color.green;

    }
}
