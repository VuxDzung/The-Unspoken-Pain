using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextAppearing : MonoBehaviour
{
    //Use for the item
    InteractiveButton item => GetComponent<InteractiveButton>();
    [SerializeField] private string[] correctTexts;
    [SerializeField] private ChangeColor textCotroller;
    void Start()
    {
        if (!item.interactable)
            textCotroller.SetToDefault();

        item.interactAction += Appearing;
    }
    void Appearing(GameObject obj)
    {
        if (!textCotroller.gameObject.activeSelf) return;
        TextMeshProUGUI[] wrongTexts = textCotroller.texts;
        for (int i = 0; i < wrongTexts.Length; i++)
        {
            wrongTexts[i].text = correctTexts[i];
            InteractiveButton item = wrongTexts[i].GetComponentInParent<InteractiveButton>();
            item.interactable = false;
        }

        textCotroller.Active();
    }
}
