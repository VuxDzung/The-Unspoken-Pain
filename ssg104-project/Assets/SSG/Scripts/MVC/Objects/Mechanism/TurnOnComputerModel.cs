using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOnComputerModel : MonoBehaviour
{
    InteractiveModel model => GetComponent<InteractiveModel>();
    [SerializeField] InteractiveButton offComputerButton;
    [SerializeField] ChangeColor screenColor;
    private void Start()
    {
        model.interactAction += TurnOnComputer;
        offComputerButton.button.onClick.AddListener(() => TurnOffComputer());
    }
    private void TurnOnComputer(GameObject obj)
    {
        screenColor.Active();   
    }
    private void TurnOffComputer()
    {
        Color col = screenColor.gameObject.GetComponent<Image>().color;
        col.a = 0;
        screenColor.gameObject.GetComponent<Image>().color = col;
    }
}
