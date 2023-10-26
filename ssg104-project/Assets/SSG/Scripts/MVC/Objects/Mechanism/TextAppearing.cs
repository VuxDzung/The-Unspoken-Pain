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
        if (!item.interactable)
            textCotroller.SetToDefault();

        item.interactAction += Appearing;
    }
    void Appearing(GameObject obj)
    {
        if (!textCotroller.gameObject.activeSelf) { Debug.Log("1"); return; }
        
        textCotroller.Active();
    }
}
