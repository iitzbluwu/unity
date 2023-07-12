using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetztaber : MonoBehaviour
{
    public GameObject BG1;
    public GameObject BG2;
    public GameObject HB2;
    public GameObject Player2Char1;
    public GameObject Player2Char2;

    public GameObject P1;
    public GameObject P2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Stage1")==1)
        {
            BG1.SetActive(true);
            BG2.SetActive(false);
        }
        else
        {
            BG1.SetActive(false);
            BG2.SetActive(true);
        }

        if (PlayerPrefs.GetInt("Avatar") == 2)
        {
            P1.SetActive(false);
            P2.SetActive(true);
        }

        if(PlayerPrefs.GetInt("Multiplayer") == 1)
        {
            HB2.SetActive(true);
            if (PlayerPrefs.GetInt("Avatar2") == 1)
            {
                Player2Char1.SetActive(true);
            }
            else if(PlayerPrefs.GetInt("Avatar2") == 2)
            {
                Player2Char2.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}