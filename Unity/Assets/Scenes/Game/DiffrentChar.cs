using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffrentChar : MonoBehaviour
{
    public GameObject BG1;
    public GameObject BG2;

    public GameObject P1;
    public GameObject P2;
    public GameObject P3;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Stage1") == 1)
        {
            BG1.SetActive(true);
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}
