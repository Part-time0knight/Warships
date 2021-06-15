using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollButton : MonoBehaviour
{
    [SerializeField] private RectTransform newContent;
    private ScrollView scroll;
    public ScrollView Scroll { get { return scroll; } }
    private void Awake()
    {
        scroll = transform.parent.parent.GetComponent<ScrollView>();
    }
    public void NextScroll()
    {
        scroll.UpdateScroll(newContent);
    }
}
