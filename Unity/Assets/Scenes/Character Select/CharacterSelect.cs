using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("test");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
