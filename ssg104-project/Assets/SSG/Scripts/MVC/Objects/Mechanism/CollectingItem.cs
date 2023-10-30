using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingItem : MonoBehaviour
{
    InteractiveObject item => GetComponent<InteractiveObject>();
    Image image => GetComponent<Image>();
    [SerializeField] private Image containerImage;
    [SerializeField] private Sprite altImage;
    [SerializeField] private InteractiveObject inventoryItem;
    void Start()
    {
        item.interactAction += Collecting;
    }
    private void OnEnable()
    {
        if (inventoryItem.interactable)
        {
            Collecting(this.gameObject);
        }
    }
    private void Collecting(GameObject obj)
    {
        containerImage.sprite = altImage;
        image.enabled = false;
        item.interactable = false;
    }
}
