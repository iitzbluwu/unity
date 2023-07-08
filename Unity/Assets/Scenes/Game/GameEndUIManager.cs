using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUIManager : MonoBehaviour
{
    public GameObject gameEndPanel;
    public Text winText;
    public Text timeText;
    public Button mainMenuButton;
    public Button quitButton;

    private int legionaerCount;
    private float sceneStartTime;
    private Button selectedButton;

    private void Start()
    {
        gameEndPanel.SetActive(false);
        winText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Register an event for the legionaer's death
        Enemy.OnLegionaerDeath += HandleLegionaerDeath;

        // Store the scene start time
        sceneStartTime = Time.time;

        SelectButton(mainMenuButton);
    }

    private void HandleLegionaerDeath()
    {
        legionaerCount++;

        // Win Condition
        if (legionaerCount >= 10)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        // Delay the activation of the gameEndPanel by 1 second
        Invoke("ActivateGameEndPanel", 1f);
    }

    private void ActivateGameEndPanel()
    {
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Stop("Publikum_Background");
        FindObjectOfType<AudioManager>().Play("Win");
        gameEndPanel.SetActive(true);
        winText.gameObject.SetActive(true);
        timeText.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);
        Time.timeScale = 0f; // Freeze the game

        // Calculate the time duration
        float sceneDuration = Time.time - sceneStartTime;

        // Display the time duration
        timeText.text = "Time: " + FormatTime(sceneDuration);

        // Optional: You can display a victory message in the "winText" UI Text object if desired
        winText.text = "You Win!";
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Unfreeze the game
        SceneManager.LoadScene(0); // Replace "MainMenu" with the name of your main menu scene
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (gameEndPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (selectedButton == quitButton)
                    SelectButton(mainMenuButton);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (selectedButton == mainMenuButton)
                    SelectButton(quitButton);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ActivateButton();
            }
        }
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

    private void OnDestroy()
    {
        Enemy.OnLegionaerDeath -= HandleLegionaerDeath;
    }
}
