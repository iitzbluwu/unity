using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Menu : MonoBehaviour
{
    public GameObject C1;
    public GameObject C2;
    public GameObject C3;
    public GameObject C4;
    public GameObject C5;
    public GameObject C6;
    public GameObject C7;
    public GameObject C8;
    public GameObject C9;

    public GameObject secutor;
    public GameObject senator;
    public GameObject P2;
    public GameObject P2C1;
    public GameObject P2C2;


    public GameObject CurrentAvatar;
    public GameObject NextAvatar;

    public int IndexCounter = 1;

    public Menu1 menuScript;
    int index;
    public GameObject cursor2;

    // Start is called before the first frame update
    void Start()
    {
        menuScript = GameObject.FindObjectOfType<Menu1>();
        CurrentAvatar = C1;
        C1.SetActive(true);
        IndexCounter = 1;
        NextAvatar = C1;

        if (menuScript != null)
        {
            index = menuScript.IndexCounter1;
        }
        PlayerPrefs.SetInt("Multiplayer", 0);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && IndexCounter > 0 && IndexCounter < 10)
        {      
            //hier
            IndexCounter += 1;
            if(P2.activeSelf == true) 
            {  
                //das
                if (IndexCounter == menuScript.IndexCounter1)
                {     
                    Debug.Log("1");
                    IndexCounter += 2;
                    CheckIndexBoundaries();       
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("1D");
                }
            }
                else
                {
                    Debug.Log("2");
                    CheckIndexBoundaries();
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("2D");
                }
        }
        if (Input.GetKeyDown(KeyCode.A) && IndexCounter > 0 && IndexCounter < 10)
        {         
            IndexCounter -= 1; 
            if(P2.activeSelf == true) 
            {  
                if (IndexCounter == menuScript.IndexCounter1)
                {          
                    IndexCounter -= 2;
                    CheckIndexBoundaries();  
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("1A");
                }
            }
                else
                {
                    CheckIndexBoundaries();
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("2A");
                }
        }

        if (Input.GetKeyDown(KeyCode.S) && IndexCounter > 0 && IndexCounter < 10)
        {
            IndexCounter += 3;
            if(P2.activeSelf == true) 
            { 
                if (IndexCounter == menuScript.IndexCounter1)
                {            
                    IndexCounter += 6;
                    CheckIndexBoundaries();
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("1S");
                }
            }
                else
                {
                    CheckIndexBoundaries();
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("2S");
                }
        }
        if (Input.GetKeyDown(KeyCode.W) && IndexCounter > 0 && IndexCounter < 10)
        {
            IndexCounter -= 3;
            if(P2.activeSelf == true) 
            { 
                if (IndexCounter == menuScript.IndexCounter1)
                {        
                    IndexCounter -= 6;
                    CheckIndexBoundaries();    
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("1W");
                }
            }
                else
                {
                    CheckIndexBoundaries();
                    NextAvatar = FindAvatar(IndexCounter);
                    CurrentAvatar.SetActive(false);
                    NextAvatar.SetActive(true);
                    //Debug.Log("2W");
                }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            cursor2.SetActive(true);
            P2.SetActive(true);
            PlayerPrefs.SetInt("Multiplayer", 1);
            PlayerPrefs.Save();
        }
           /* if (Input.GetKeyDown(KeyCode.RightArrow) && IndexCounter > 0 && IndexCounter < 10)
            {
                IndexCounter += 1;
                CheckIndexBoundaries();
                NextAvatar = FindAvatar(IndexCounter);
                CurrentAvatar.SetActive(false);
                NextAvatar.SetActive(true);
                //Debug.Log("Right key was pressed. IndexCounter: " +IndexCounter);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && IndexCounter > 0 && IndexCounter < 10)
            {
                IndexCounter -= 1;
                CheckIndexBoundaries();
                NextAvatar = FindAvatar(IndexCounter);
                CurrentAvatar.SetActive(false);
                NextAvatar.SetActive(true);
                //Debug.Log("Left key was pressed. IndexCounter: " +IndexCounter);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && IndexCounter > 0 && IndexCounter < 10)
            {
                IndexCounter += 3;
                CheckIndexBoundaries();
                NextAvatar = FindAvatar(IndexCounter);
                CurrentAvatar.SetActive(false);
                NextAvatar.SetActive(true);
                //Debug.Log("Right key was pressed. IndexCounter: " +IndexCounter);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && IndexCounter > 0 && IndexCounter < 10)
            {
                IndexCounter -= 3;
                CheckIndexBoundaries();
                NextAvatar = FindAvatar(IndexCounter);
                CurrentAvatar.SetActive(false);
                NextAvatar.SetActive(true);
                //Debug.Log("Left key was pressed. IndexCounter: " +IndexCounter);
            }
        }*/

        if (C1.activeSelf || C2.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Return key was pressed.");
                PlayerPrefs.SetInt("Avatar", IndexCounter);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        if (CurrentAvatar != C1)
        {
            secutor.SetActive(false);
        }
        else
        {
            secutor.SetActive(true);
        }
        if (CurrentAvatar == C2)
        {
            senator.SetActive(true);
        }
        else 
        {
            senator.SetActive(false); 
        }


        /*if (CurrentAvatar != C1)
        {
            if (P2.activeSelf)
            {
                P2C1.SetActive(false);
            }
        }
        else
        {
            if (P2.activeSelf)
            {
                P2C1.SetActive(true);
            }
        }


        if (CurrentAvatar == C2)
        {
            if (P2.activeSelf)
            {
                P2C2.SetActive(true);
            }
        }
        else
        {
            if (P2.activeSelf)
            {
                P2C2.SetActive(false);
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
            CurrentAvatar = NextAvatar;

        //Debug.Log("IndexCounter: " + IndexCounter);
    }

    GameObject FindAvatar(int avatarIndex)
    {

        if (avatarIndex == 1)
        {
            return C1;
        }
        else if (avatarIndex == 2)
        {
            return C2;
        }
        else if (avatarIndex == 3)
        {
            return C3;
        }
        else if (avatarIndex == 4)
        {
            return C4;
        }
        else if (avatarIndex == 5)
        {
            return C5;
        }
        else if (avatarIndex == 6)
        {
            return C6;
        }
        else if (avatarIndex == 7)
        {
            return C7;
        }
        else if (avatarIndex == 8)
        {
            return C8;
        }
        else if (avatarIndex == 9)
        {
            return C9;
        }
        else {
            return null;
        }

    }

    void CheckIndexBoundaries()
    {
        if(P2.activeSelf == true) 
            { 
                if (IndexCounter > 9 && menuScript.IndexCounter1 != 9)
                {
                    IndexCounter = 9;
                }
                else if (IndexCounter > 9 && menuScript.IndexCounter1 == 9)
                {
                    IndexCounter = 1;
                }
                if (IndexCounter < 1 && menuScript.IndexCounter1 != 1)
                {
                    IndexCounter = 1;
                }
                else if (IndexCounter < 1 && menuScript.IndexCounter1 == 1)
                {
                    IndexCounter = 9;
                }
            }
        else 
        {
                if (IndexCounter > 9)
                {
                    IndexCounter = 9;
                }
                if (IndexCounter < 1 )
                {
                    IndexCounter = 1;
                }
        }
    }
}