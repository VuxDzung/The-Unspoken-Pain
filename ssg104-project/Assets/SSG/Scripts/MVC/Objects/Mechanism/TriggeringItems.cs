using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringItems : MonoBehaviour
{
    InteractiveObject itemObj => GetComponent<InteractiveObject>();
    [SerializeField] private string[] triggedInteractiveObjects;
    [SerializeField] private string[] triggedInventoryItems;

    private int currentBranch = -1;

    void Start()
    {
        itemObj.interactAction += Trigger;
    }
    void Trigger(GameObject obj)
    { 
        if (triggedInteractiveObjects.Length > 0)
        {
            GameManager.Instance.AddInteractedItemsByName(triggedInteractiveObjects);
            Debug.LogError("Add object");
        }

        if (triggedInventoryItems.Length > 0)
            GameManager.Instance.AddInventoryItemByName(triggedInventoryItems);
        itemObj.interactAction -= Trigger;
    }
}
