using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private void Awake()
    {
        // Überprüfe, ob bereits eine Instanz des GameManagers existiert
        if (instance == null)
        {
            // Wenn nicht, setze diese Instanz als die aktuelle Instanz
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Wenn bereits eine Instanz existiert, zerstöre dieses GameObject
            Destroy(gameObject);
        }
    }
}
