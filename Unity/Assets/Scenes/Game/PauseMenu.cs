using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button continueButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Text pauseText;

    private Button selectedButton;

    public GameObject Death;
    public GameObject Win;

    private void Start()
    {
        pauseText.gameObject.SetActive(false);
        SelectButton(continueButton);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Death.activeSelf && !Win.activeSelf)
        {
            if (pauseMenuUI.activeSelf)
                Resume();
            else
                Pause();
        }

        if (pauseMenuUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (selectedButton == mainMenuButton)
                    SelectButton(continueButton);
                else if (selectedButton == quitButton)
                    SelectButton(mainMenuButton);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (selectedButton == continueButton)
                    SelectButton(mainMenuButton);
                else if (selectedButton == mainMenuButton)
                    SelectButton(quitButton);
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                ActivateButton();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        pauseText.gameObject.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        pauseText.gameObject.SetActive(true);
        pauseText.text = "Pause";
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MEMEZ");
    }

    public void QuitGame()
    {
        Application.Quit();
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
