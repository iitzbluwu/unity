using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo_Animation_Secutor : MonoBehaviour
{
    public Animator ani;
    public int combo;
    public bool attack;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponent<Animator>();
    }

    public void Start_Combo() 
    {
        attack = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Finish_Ani()
    {
        attack = false;
        combo = 0;
    }

    public void Combos_()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&!attack)
        {
            attack = true;
            ani.SetTrigger("" + combo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Combos_();
    }
}
