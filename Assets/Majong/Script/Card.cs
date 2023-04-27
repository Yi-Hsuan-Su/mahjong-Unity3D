using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Card : IComparable
{
    private int number;

    public int Number
    {
        get { return number; }
        set { number = value; }
    }
    private string type;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    public int CompareTo(object obj)
    {
        Card card = obj as Card;

        if (findtype(type) > findtype(card.type))
        {
            return 1;
        }
        else if (findtype(type) == findtype(card.type))
        {
            if (Number > card.Number)
            {
                return 1;
            }
            else if (Number == card.Number)
            {
                return 0;
            }
            else
            {
                return -1;
            }

        }
        else return -1;
    }

    private int findtype(string sType)
    {
        string[] Cardtype = { "wan", "ton", "tiao", "eswn", "mfb", "flower", null };

        for (int i = 0; i < 7; i++)
        {
            if (Cardtype[i] == sType)
            {
                return i;
            }
        }
        return 0;
    }


}

