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
    void Start()
    {
        item.interactAction += Collecting;
    }
    private void Awake()
    {
        if (item.interactable) return;
        Collecting(this.gameObject);
    }
    private void Collecting(GameObject obj)
    {
        containerImage.sprite = altImage;
        image.enabled = false;
        item.interactable = false;
    }
}
