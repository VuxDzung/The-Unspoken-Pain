using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractiveButton : InteractiveObject
{
    internal Button button => GetComponent<Button>();
    private Image image => GetComponent<Image>();
    private TextMeshProUGUI text => GetComponentInChildren<TextMeshProUGUI>();

    private void Awake()
    {
        button.onClick.AddListener(() => OnAction());
    }
    private void ActiveColor()
    {
        if (interactable)
        {
            if (image.color.a == 1f) return;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1f);
            if(text != null) text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        }
        else
        {
            if (image.color.a == 0f) return;
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0f);
            if (text != null) text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }
    }
    private void Update()
    {
        ActiveColor();
    }
    
}
