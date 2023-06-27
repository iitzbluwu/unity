using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Text pauseText; // Add a reference to the UI Text object for displaying the pause text
    private bool isPaused = false;

    private void Start()
    {
        pauseText.gameObject.SetActive(false); // Initially hide the pause text
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }

        pauseMenuUI.SetActive(isPaused);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        pauseText.gameObject.SetActive(false); // Hide the pause text when resuming
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseText.gameObject.SetActive(true); // Show the pause text when pausing
        pauseText.text = "Pause"; // Set the text to "Pause"
    }

    public void ExitToMainMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MEMEZ");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
