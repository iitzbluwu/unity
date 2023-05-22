using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{   
    public static int selectedCharacter;
    public int character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OpenScene() 
    {
        selectedCharacter = character;
        SceneManager.LoadScene("Game");
        //Debug.Log("Character: " + character);
    }
}
