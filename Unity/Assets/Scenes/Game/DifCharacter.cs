using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifCharacter : MonoBehaviour
{
    public Sprite[] models;
    public Image model;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        int character = CharacterSelect.selectedCharacter;
        Debug.Log(character);
        text.text = "Character " + character.ToString();
        model.sprite = models[character - 1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
