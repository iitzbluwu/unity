using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win_Lose : MonoBehaviour
{
    public GameObject deathScreenPanel;
    public Button mainMenuButton;
    public Button retryButton;

    private Button selectedButton;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        deathScreenPanel.SetActive(false);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        retryButton.onClick.AddListener(RetryLevel);

        SelectButton(mainMenuButton);
    }

    private void Update()
    {
        if (deathScreenPanel.activeSelf)
        {
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Stop("Publikum_Background");
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (selectedButton != mainMenuButton)
                    SelectButton(mainMenuButton);
                    //SelectButton(retryButton);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (selectedButton != retryButton)
                    SelectButton(retryButton);
                    //SelectButton(mainMenuButton);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ActivateButton();
            }
        }
    }

    public void ShowDeathScreen()
    {
        deathScreenPanel.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        FindObjectOfType<AudioManager>().Play("Theme");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SelectButton(Button button)
    {
        if (selectedButton != null)
        {
            selectedButton.interactable = true;
        }

        selectedButton = button;
        selectedButton.interactable = false;
    }

    private void ActivateButton()
    {
        if (selectedButton != null)
        {
            selectedButton.onClick.Invoke();
        }
    }
}
