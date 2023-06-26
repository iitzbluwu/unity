using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startTime = 0f; // Starting time in seconds
    private float currentTime;
    private Text timerText;

    private void Start()
    {
        timerText = GetComponent<Text>();
        currentTime = startTime;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        UpdateTimerUI();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
