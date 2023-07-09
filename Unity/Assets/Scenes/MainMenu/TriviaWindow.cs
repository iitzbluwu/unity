using UnityEngine;
using UnityEngine.SceneManagement;

public class TriviaWindow : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //SceneManager.LoadScene(0);
            Destroy(gameObject);
        }
    }
    public void CloseTriviaWindow()
    {
        Destroy(gameObject);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
