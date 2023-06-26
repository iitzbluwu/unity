using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

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
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ExitToMainMenu()
    {
        // Replace "MainMenuScene" with the name of your main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("MEMEZ");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
