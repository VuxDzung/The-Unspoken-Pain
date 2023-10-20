using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringItems : MonoBehaviour
{
    InteractiveObject obj => GetComponent<InteractiveObject>();
    [SerializeField] private Story objectStory;
    [SerializeField] private string[] triggedNames;
    [SerializeField] private string[] triggedItems;

    private int currentBranch = -1;

    void Start()
    {
        obj.interactAction += Trigger;
    }
    void Trigger()
    { 
        GameManager.Instance.AddInteractedItemsByName(triggedNames);

        GameManager.Instance.AddInventoryItemByName(triggedItems);
    }
}
