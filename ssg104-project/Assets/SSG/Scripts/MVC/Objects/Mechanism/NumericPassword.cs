using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumericPassword : MonoBehaviour
{
    private InteractiveButton enterButton => GetComponent<InteractiveButton>();
    [SerializeField] private InteractiveButton[] digitButtons;
    [SerializeField] private int password;
    void Start()
    {
        foreach (var button in digitButtons)
        {
            button.interactAction += Press;
            button.interactAction += CheckPassWord;
        }   
    }

    public void Press(GameObject button)
    {
        TextMeshProUGUI TMP = button.GetComponentInChildren<TextMeshProUGUI>();
        int num = int.Parse(TMP.text);
        num = (num + 1) % 10;
        TMP.text = num.ToString();
    }
    private void CheckPassWord(GameObject obj)
    {
        for(int i = 0; i<digitButtons.Length; i++)
        {
            InteractiveButton button = digitButtons[i];
            int digit = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
            int correctDigit = (int) (password / Math.Pow(10, digitButtons.Length - i - 1)) % 10;
            if (digit != correctDigit)
            {
                enterButton.interactable = false;
                return;
            }
        }
        enterButton.interactable = true;
        enterButton.OnAction();
    }
}
