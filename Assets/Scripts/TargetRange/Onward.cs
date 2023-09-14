using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onward : MonoBehaviour
{
    public RectTransform rect;

    void Update()
    {
        rect.anchoredPosition += new Vector2(5, 0);
    }
}