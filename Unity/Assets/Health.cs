using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Sprite[] sprites;
    public string spriteFolderPath = "Assets/Assets/Sprites_Health/Sprites_Health/Secutor_Health";
    public string spriteNamePrefix = "Secutor_Healthbar_";
    public int maxSprites = 6;

    private Player player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        sprites = new Sprite[maxSprites];

        for (int i = 0; i < maxSprites; i++)
        {
            string spriteName = spriteNamePrefix + i;
            string spritePath = spriteFolderPath + "/" + spriteName;
            sprites[i] = Resources.Load<Sprite>(spritePath);

            if (sprites[i] == null)
            {
                Debug.LogWarning("Sprite not found at path: " + spritePath);
            }
        }

        UpdateSprite();
    }

    private void UpdateSprite()
    {
        int spriteIndex = Mathf.Clamp(maxSprites - player.CurrentHealth, 0, maxSprites - 1);

        if (spriteIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        else
        {
            Debug.LogWarning("Sprite not found for index: " + spriteIndex);
        }
    }

    private void Update()
    {
        UpdateSprite();
    }
}
