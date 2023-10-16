using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringItems : MonoBehaviour
{
    InteractiveObject obj => GetComponent<InteractiveObject>();
    [SerializeField] private GameObject[] TriggedItems;
    void Start()
    {
        obj.interactAction += Trigger;
    }
    void Trigger() 
    {
        string[] names = new string[TriggedItems.Length];
        for (int i = 0; i < TriggedItems.Length; i++)
            names[i] = TriggedItems[i].name;
        GameManager.Instance.AddInteractedItems(names);
    }
}
