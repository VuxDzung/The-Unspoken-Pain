using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveButton : InteractiveObject
{
    private Button button => GetComponent<Button>();
    private void Awake()
    {
        button.onClick.AddListener(() => OnOpenView());
    }
}
