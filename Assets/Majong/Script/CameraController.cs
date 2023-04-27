using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Shufflecard shuffler;
    public Camera[] cam=new Camera[4];
    // Start is called before the first frame update
    void Start()
    {
        shuffler = GameObject.Find("shuffle").GetComponent<Shufflecard>();
        /*cam[0] = this.gameObject.GetComponentInChildren<Camera>();
        cam[1] = this.gameObject.GetComponentInChildren<Camera>();
        cam[2] = this.gameObject.GetComponentInChildren<Camera>();
        cam[3] = this.gameObject.GetComponentInChildren<Camera>();*/
    }

    // Update is called once per frame
    void Update()
    {
        // if(shuffler.PlayerIndex)
        CallCam();
      
    }

    public void CallCam()
    {
        for(int i=0;i<4;i++)
        {
            cam[i].gameObject.SetActive(false);
        }
           

        switch (shuffler.PlayerIndex)
        {
            case 0:
                cam[0].gameObject.SetActive(true);
                break;
            case 1:
                cam[1].gameObject.SetActive(true);
                break;
            case 2:
                cam[2].gameObject.SetActive(true);
                break;
            case 3:
                cam[3].gameObject.SetActive(true);
                break;
        }
    }
}
