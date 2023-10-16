using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Image[] packages;
    internal List<string> inventoryItems;
    private void Awake()
    {
        LoadInventory();
    }
    //item in inventory (interactiveButton type) has loaded on start but disable, 'add' mean enable it and transport to package transform
    public void AddItem(GameObject item)
    {
        item.SetActive(true);
        RectTransform package = packages[inventoryItems.Count].rectTransform;
        item.GetComponent<RectTransform>().position = package.position;
        inventoryItems.Add(item.name);
    }
    public void SaveInventory() 
    {
        //save inventoryItems
    }
    public void LoadInventory() 
    {
        //inventoryData = platform... (save form of inventoryItems);
        //foreach(var name in inventoryData)
        //{
        //GameObject item = GameObject.Find(name);
        //AddItem(item);
        //}
    }
}
