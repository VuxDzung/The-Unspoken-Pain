using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractiveButton : InteractiveObject
{
    [SerializeField] private bool isSaveBtn = false;
    internal Button button => GetComponent<Button>();
    private Image image => GetComponent<Image>();
    private TextMeshProUGUI text
    {
        get { TextMeshProUGUI _text = GetComponentInChildren<TextMeshProUGUI>();
            if (GetComponentInChildren<InteractiveButton>() != null)
                return null;
            else return _text;
        }
    } 
        

    private void Awake()
    {
        button.onClick.AddListener(() => OnAction());

        if (isSaveBtn) button.onClick.AddListener(SaveProcess);
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
    
    public void SaveProcess()
    {
        GameManager.Instance.SaveProcess();
    }
}
