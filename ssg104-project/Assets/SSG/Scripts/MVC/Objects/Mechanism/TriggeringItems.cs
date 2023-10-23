using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringItems : MonoBehaviour
{
    InteractiveObject obj => GetComponent<InteractiveObject>();
    [SerializeField] private string[] triggedInteractiveObjects;
    [SerializeField] private string[] triggedInventoryItems;

    private int currentBranch = -1;

    void Start()
    {
        obj.interactAction += Trigger;
    }
    void Trigger(GameObject obj)
    { 
        if (triggedInteractiveObjects.Length > 0)
            GameManager.Instance.AddInteractedItemsByName(triggedInteractiveObjects);

        if (triggedInventoryItems.Length > 0)
            GameManager.Instance.AddInventoryItemByName(triggedInventoryItems);
    }
}
