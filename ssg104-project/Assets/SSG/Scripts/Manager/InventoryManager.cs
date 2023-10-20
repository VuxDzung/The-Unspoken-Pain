using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform itemsHolder;
    internal List<string> inventoryItems = new List<string>();

    private void Awake()
    {
        LoadInventory();
    }

    //item in inventory (interactiveButton type) has loaded on start but disable, 'add' mean enable it and transport to package transform
    public void AddItem(GameObject item)
    {
        item.GetComponent<Button>().enabled = true;
        item.GetComponent<Image>().enabled = true;

        item.GetComponent<RectTransform>().SetParent(itemsHolder);

        if (inventoryItems.Contains(item.name)) return;
        inventoryItems.Add(item.name);
        Debug.Log($"Add: {item.name}");
        GameManager.Instance.itemData.itemsInventory = inventoryItems;
    }

    public void AddItemByName(string[] itemNames)
    {
        foreach(var itemName in itemNames)
        {
            GameObject item = GameObject.Find(itemName);
            if (item != null)
            {
                AddItem(item);
            }
        }
    }

    public void LoadInventory() 
    {
        if (GameManager.Instance.itemData.itemsInventory != null)
        {
            inventoryItems = GameManager.Instance.itemData.itemsInventory;
            foreach (var name in inventoryItems)
            {
                GameObject item = GameObject.Find(name);

                if (item != null)
                {
                    AddItem(item);
                }
            }
        }
    }
}
