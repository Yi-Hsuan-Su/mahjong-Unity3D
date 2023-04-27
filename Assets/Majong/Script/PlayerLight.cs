using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLight : MonoBehaviour
{
    public Shufflecard shuffler;
    public Text[] player=new Text[4];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shuffler)
        {
            for(int i=0;i<player.Length;i++)
            {
                if(i!=shuffler.PlayerIndex)
                {
                    player[i].color = Color.black;
                }
                else
                {
                    player[i].color = Color.blue;
                }
            }
            
        }
    }
}
