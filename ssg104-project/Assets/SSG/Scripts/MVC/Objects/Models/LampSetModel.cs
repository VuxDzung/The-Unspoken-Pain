using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSetModel : MonoBehaviour
{
    [SerializeField] private new Light light;
    InteractiveModel model => GetComponent<InteractiveModel>();
    void Start()
    {
        model.interactAction += TurnLight;
        model.nonInteractAction += TurnLight;
    }
    void TurnLight()
    {
        try
        {
            if (light.enabled == false)
            {
                light.enabled = true;
                model.interactable = true;
            }
            else
            {
                light.enabled = false;
                model.interactable = false;
            }
        }
        catch (InvalidOperationException e) { Debug.LogError(e); }
    }
}
