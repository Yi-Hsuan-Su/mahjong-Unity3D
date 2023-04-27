using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Shufflecard shuffler;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        shuffler = gameObject.GetComponent<Shufflecard>();
        player= gameObject.GetComponent<Player>();
        shuffler.testplayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
