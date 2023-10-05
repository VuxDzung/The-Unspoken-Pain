using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum Category
{
    Humanoid,
    Object
}

public class InteractiveModel : MonoBehaviour
{
    [SerializeField] private Category category;
    public Action interactAction { get; protected set; } 

    public void DisplayConsoleMessage()
    {
        switch (category)
        {
            case Category.Humanoid:
                Debug.Log($"Start talking with {this.gameObject.transform.name}");
                break;
            case Category.Object:
                Debug.Log($"Start interact with {this.gameObject.name}");
                break;
        }
    }

    public void OnAction()
    {
        interactAction?.Invoke();
    }
}
