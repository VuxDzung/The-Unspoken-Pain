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
        offComputerButton.interactAction += TurnOffComputer;
    }
    private void TurnOnComputer(GameObject obj)
    {
        screenColor.Active();   
    }
    private void TurnOffComputer(GameObject obj)
    {
        screenColor.SetToDefault();
    }
}
