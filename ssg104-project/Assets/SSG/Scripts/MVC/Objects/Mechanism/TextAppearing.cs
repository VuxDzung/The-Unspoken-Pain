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
    }
    void Appearing(GameObject obj)
    {
        textCotroller.Activate = true;
        item.interactable = false;
    }
}
