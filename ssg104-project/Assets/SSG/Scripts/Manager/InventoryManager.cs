using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryManager : MonoBehaviour
{
    [SerializeField] private RectTransform defaultHolder;
    [SerializeField] private RectTransform itemsHolder;
    [SerializeField] private RectTransform momChoiceHolder;
    [SerializeField] private RectTransform noteLineHolder;
    [SerializeField] private RectTransform letterHolder;
    internal List<string> inventoryItems = new List<string>();

    private string currentLetterName = "";
    private void Awake()
    {
        LoadInventory();
    }

    //item in inventory (interactiveButton type) has loaded on start but disable, 'add' mean enable it and transport to package transform
    public void AddItem(GameObject item)
    {
        if (item.name.Contains("NoteLine_")) //player note
        {
            if (noteLineHolder != null)
            {
                item.GetComponent<TextMeshProUGUI>().enabled = true;
                item.GetComponent<RectTransform>().SetParent(noteLineHolder);
            }
        }
        else if (item.name.Contains("Letter_"))
        {
            if (letterHolder != null)
            {
                if (letterHolder.childCount > 0 && defaultHolder != null)
                {
                    for (int i = 0; i < letterHolder.childCount; i++)
                    {
                        GameObject letter = letterHolder.GetChild(i).gameObject;
                        if (letter.activeSelf) letter.SetActive(false);
                    }

                    for (int i = 0; i < letterHolder.childCount; i++)
                    {
                        GameObject letter = letterHolder.GetChild(i).gameObject;

                        if (letter.name == GameManager.Instance.currentLetter)
                        {
                            letter.SetActive(true);
                        }
                    }
                }
                
            }
        }

        else if (item.name.Contains("MomChoice_")) //mom choices
        {
            if (momChoiceHolder != null)
            {
                item.GetComponent<Button>().enabled = true;
                item.GetComponent<Image>().enabled = true;
                item.GetComponent<RectTransform>().SetParent(momChoiceHolder);
            }
        }
        else // other items
        {
            if (itemsHolder != null)
            {
                item.GetComponent<Button>().enabled = true;
                item.GetComponent<Image>().enabled = true;
                item.GetComponent<RectTransform>().SetParent(itemsHolder);
            }
        }

        if (item.GetComponent<InteractiveObject>() != null)
            item.GetComponent<InteractiveObject>().interactable = true;

        if (inventoryItems.Contains(item.name)) return;

        inventoryItems.Add(item.name);
        if (item.name.Contains("Letter_")) currentLetterName = item.name;
        Debug.Log($"Add Item: {item.name}");
        GameManager.Instance.itemData.itemsInventory = inventoryItems;
        GameManager.Instance.currentLetter = currentLetterName;
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
