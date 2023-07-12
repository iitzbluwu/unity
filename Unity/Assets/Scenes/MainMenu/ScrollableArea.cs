using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollableArea : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TextMeshProUGUI textContent;
    public float scrollSpeed = 100f;
    public float maxScrollPosition = 0f;

    private bool isScrollingUp;
    private bool isScrollingDown;

    void Start()
    {
        AdjustContentSize();
    }

    void AdjustContentSize()
    {
        RectTransform contentTransform = scrollRect.content;
        contentTransform.sizeDelta = new Vector2(contentTransform.sizeDelta.x, textContent.preferredHeight);
    }

    void Update()
    {
        isScrollingUp = Input.GetKey(KeyCode.UpArrow);
        isScrollingDown = Input.GetKey(KeyCode.DownArrow);

        if (isScrollingUp)
        {
            ScrollUp();
        }
        else if (isScrollingDown)
        {
            ScrollDown();
        }
    }

    void ScrollUp()
    {
        float scrollAmount = scrollSpeed * Time.deltaTime;
        Vector2 newPosition = scrollRect.content.anchoredPosition + new Vector2(0, scrollAmount);

        if (newPosition.y < maxScrollPosition)
        {
            scrollRect.content.anchoredPosition = newPosition;
        }
        else
        {
            scrollRect.content.anchoredPosition = new Vector2(scrollRect.content.anchoredPosition.x, maxScrollPosition);
        }
    }

    void ScrollDown()
    {
        float scrollAmount = scrollSpeed * Time.deltaTime;
        scrollRect.content.anchoredPosition -= new Vector2(0, scrollAmount);
    }
}
