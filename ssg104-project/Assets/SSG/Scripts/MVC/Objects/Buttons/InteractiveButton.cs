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
        //Action action;
        //action +=...
        //UnityAction unityAction = new UnityAction(action);
        //button.onClick.AddListener(unityAction);
        button.onClick.AddListener(() => OnOpen());
    }
    
}
