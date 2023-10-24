using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputPassword : MonoBehaviour
{
    private InteractiveObject enterButton => GetComponent<InteractiveObject>();
    [SerializeField] private TMP_InputField input;
    [SerializeField] private string password;
    public void CheckPassWord(GameObject obj)
    {
        if (input.text != password) enterButton.interactable = false;
        else
        {
            enterButton.interactable = true;
            enterButton.OnAction();
        }
    }
}
