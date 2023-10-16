using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractiveButton : InteractiveObject
{
    internal Button button => GetComponent<Button>();

    private void Awake()
    {
        button.onClick.AddListener(() => OnAction());
    }
    
}
