using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringItems : MonoBehaviour
{
    InteractiveObject obj => GetComponent<InteractiveObject>();
    [SerializeField] private Story objectStory;
    [SerializeField] private string[] triggedNames;
    void Start()
    {
        obj.interactAction += Trigger;
    }
    void Trigger()
    { 
        GameManager.Instance.AddInteractedItems(triggedNames);
    }
}
