using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject triviaWindowPrefab; // Referenz auf das TriviaWindow Prefab
    public GameObject playButton; // Referenz auf den Play-Button
    public GameObject triviaButton; // Referenz auf den Trivia-Button
    public GameObject quitButton; // Referenz auf den Quit-Button

    private GameObject selectedButton; // Der aktuell ausgewählte Button

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.index = 0;
        FindObjectOfType<AudioManager>().Stop("Publikum_Background");
        FindObjectOfType<AudioManager>().Stop("Theme");
        FindObjectOfType<AudioManager>().Stop("Publikum_Start");
        FindObjectOfType<AudioManager>().Stop("Win");
        FindObjectOfType<AudioManager>().Stop("Publikum");
        SelectButton(playButton); // Am Anfang wird der Play-Button ausgewählt
    }

    private void Update()
    {
        // Navigation zwischen den Buttons mit den Tasten "w" und "s"
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectedButton == triviaButton)
            {
                SelectButton(playButton);
            }
            else if (selectedButton == quitButton)
            {
                SelectButton(triviaButton);
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectedButton == playButton)
            {
                SelectButton(triviaButton);
            }
            else if (selectedButton == triviaButton)
            {
                SelectButton(quitButton);
            }
        }

        // Button auswählen, wenn die Leertaste oder die Eingabetaste gedrückt wird
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            ActivateButton();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Trivia()
    {
        GameObject triviaWindow = Instantiate(triviaWindowPrefab);
        //triviaWindow.GetComponent<TriviaWindow>().CloseTriviaWindow();
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void SelectButton(GameObject button)
    {
        if (selectedButton != null)
        {
            // Den vorherigen ausgewählten Button zurücksetzen
            selectedButton.GetComponent<UnityEngine.UI.Button>().interactable = true;
        }

        // Den neuen ausgewählten Button markieren
        selectedButton = button;
        selectedButton.GetComponent<UnityEngine.UI.Button>().interactable = false;
    }

    private void ActivateButton()
    {
        if (selectedButton != null)
        {
            selectedButton.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }
}
