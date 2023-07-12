using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrollableArea : MonoBehaviour
{
    public ScrollRect scrollRect;
    public TextMeshProUGUI textContent;

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
        float scrollValue = Input.GetAxis("Mouse ScrollWheel");
        Vector2 scrollDelta = new Vector2(0, scrollValue);

        scrollRect.content.anchoredPosition += scrollDelta;
    }
}
