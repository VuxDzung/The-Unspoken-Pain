using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Đẩy hết vào game manager
public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform itemsHolder;
    [SerializeField] private Image[] packages;
    internal List<string> inventoryItems = new List<string>();

    private void Awake()
    {
        LoadInventory();
    }

    public void SetItemParent(GameObject item)
    {
        item.GetComponent<RectTransform>().SetParent(itemsHolder);//using RectTransform.SetParent instead
    }

    //item in inventory (interactiveButton type) has loaded on start but disable, 'add' mean enable it and transport to package transform
    public void AddItem(GameObject item)
    {
        
        item.SetActive(true);
        RectTransform package = packages[inventoryItems.Count].rectTransform;
        item.GetComponent<RectTransform>().position = package.position;

        if (inventoryItems.Contains(item.name)) return;
        inventoryItems.Add(item.name);
        Debug.Log($"Add: {item.name}");
        GameManager.Instance.itemData.itemsInventory = inventoryItems;
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
