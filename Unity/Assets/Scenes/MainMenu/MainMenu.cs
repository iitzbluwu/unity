using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject triviaWindowPrefab; // Referenz auf das TriviaWindow Prefab

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Trivia()
    {
        GameObject triviaWindow = Instantiate(triviaWindowPrefab); // Erstellt eine Instanz des TriviaWindow Prefabs
        //triviaWindow.GetComponent<TriviaWindow>().CloseTriviaWindow(); // Ruft die CloseTriviaWindow-Funktion des TriviaWindow-Skripts auf
    }

    public void Quit()
    {
        Application.Quit(); // Beendet das Spiel
    }
}
