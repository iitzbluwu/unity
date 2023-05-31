using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pfeiltasten : MonoBehaviour
{
    public GameObject Stage1;
    public GameObject Stage2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Stage1.SetActive(true);
            Stage2.SetActive(false);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Stage1.SetActive(false);
            Stage2.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        if (Stage1.activeSelf)
        {
            PlayerPrefs.SetInt("Stage1", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("Stage1", 0);
            PlayerPrefs.Save();
        }

        if (Stage2.activeSelf)
        {
            PlayerPrefs.SetInt("Stage2", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("Stage2", 0);
            PlayerPrefs.Save();
        }
    }
    /*void save()
    {
            // Um den SetActive-Zustand zu speichern
            PlayerPrefs.SetInt("Stage1", Stage1.activeSelf ? 1 : 0);
            PlayerPrefs.Save();
    }

    void load()
    {
        // Um den SetActive-Zustand in einer neuen Szene wiederherzustellen
        if (PlayerPrefs.GetInt("Stage1", 0) == 1)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }*/
}
