using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{
    [SerializeField] private float time = 1f;
    private bool inAnim = false;
    private RectTransform rectTransform;
    private float hidePosition;
    private ScrollRect scrollRect;
    private RectTransform newContent;
    public bool InAnimation { get { return inAnim; } }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponent<ScrollRect>();
        hidePosition = rectTransform.rect.width;
    }
    public void UpdateScroll(RectTransform newContent)
    {
        this.newContent = newContent;
        if (!inAnim)
        {
            StartCoroutine(HideAnim());
        }
    }
    private void SetContent()
    {
        GameObject oldContent = scrollRect.content.gameObject;
        scrollRect.content = Instantiate(newContent, transform);
        Destroy(oldContent);
    }
    private IEnumerator HideAnim()
    {
        inAnim = true;
        float start = rectTransform.localPosition.x;
        Vector3 line = Vector3.zero;
        line.x = (hidePosition) / time;

        while (rectTransform.localPosition.x < start + hidePosition)
        {
            rectTransform.localPosition += line * Time.deltaTime;
            yield return null;
        }

        if (newContent)
            SetContent();
        StartCoroutine(ShowAnim());
    }
    private IEnumerator ShowAnim()
    {
        float start = rectTransform.localPosition.x;
        Vector3 line = Vector3.zero;
        line.x = (hidePosition) / time;
        while (rectTransform.localPosition.x > start - hidePosition)
        {
            rectTransform.localPosition -= line * Time.deltaTime;
            yield return null;
        }
        inAnim = false;
    }
}
