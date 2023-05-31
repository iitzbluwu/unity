using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetztaber : MonoBehaviour
{
    public GameObject BG1;
    public GameObject BG2;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Stage1")==1)
        {
            BG1.SetActive(true);
        }
        else
        {
            BG1.SetActive(false);
            BG2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}