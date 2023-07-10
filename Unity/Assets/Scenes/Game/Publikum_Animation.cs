using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Publikum_Animation : MonoBehaviour
{
    public Animator Ani_Publikum;

    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerPrefs.GetInt("pub")==1)
        {
            Ani_Publikum.SetTrigger("jubel");
            PlayerPrefs.SetInt("pub", 0);
            PlayerPrefs.Save();
        }
    }
}
