using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playCon_2 : MonoBehaviour
{
    public Animator myAnim;
    public bool isAttacking = false;
    public static playCon_2 instance;

    private void Awake()
    {
        instance = this; 
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.W)&& !isAttacking) 
        {
            isAttacking = true;
        }
    }
}
