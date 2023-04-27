using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Player playerE, playerS, playerW, playerN;
    public Shufflecard card_shuffling;
   
    void Start()
    {
        card_shuffling.InitialPlayer_card(playerE, playerS, playerW, playerN);
        card_shuffling.ground_flower(playerE);
        card_shuffling.ground_flower(playerS);
        card_shuffling.ground_flower(playerW);
        card_shuffling.ground_flower(playerN);
        
        
        //card_shuffling.Playerdrawcard(playerE);

    }


    private void Update()
    {

    }

  
}

   
