using UnityEngine;
using UnityEngine.SceneManagement;

public class TriviaWindow : MonoBehaviour
{
    public void CloseTriviaWindow()
    {
        Destroy(gameObject);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
