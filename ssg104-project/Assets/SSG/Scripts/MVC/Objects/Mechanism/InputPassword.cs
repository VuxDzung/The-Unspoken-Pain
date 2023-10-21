using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputPassword : MonoBehaviour
{
    private InteractiveObject enterButton => GetComponent<InteractiveObject>();
    [SerializeField] private TMP_InputField input;
    [SerializeField] private string password;
    void Start()
    {
        enterButton.nonInteractAction += CheckPassWord;
    }
    private void CheckPassWord(GameObject obj)
    {
        if (input.text != password) return;
        enterButton.interactable = true;
        enterButton.OnAction();
    }
}
