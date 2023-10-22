using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAppearing : MonoBehaviour
{
    //Use for the item
    InteractiveButton item => GetComponent<InteractiveButton>();
    [SerializeField] private ChangeColor textCotroller;
    void Start()
    {
        item.interactAction += Appearing;
        item.nonInteractAction += Appearing;
    }
    void Appearing(GameObject obj)
    {
        if (item.interactable)
        {
            textCotroller.Active();
            item.interactable = false;
        }
        else
        {
            textCotroller.SetToDefault();
            item.interactable = true;
        }
        
    }
}
