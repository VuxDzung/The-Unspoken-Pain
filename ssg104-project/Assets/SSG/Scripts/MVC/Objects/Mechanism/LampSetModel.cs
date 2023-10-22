using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSetModel : MonoBehaviour
{
    [SerializeField] private new Light[] lights;
    InteractiveModel model => GetComponent<InteractiveModel>();
    void Start()
    {
        model.interactAction += TurnLight;
        model.nonInteractAction += TurnLight;
    }
    void TurnLight(GameObject obj)
    {
        if(model.interactable) model.interactable = false;
        else model.interactable = true;

        foreach(var light in lights)
        try
        {
            if (light.enabled == false)
            {
                light.enabled = true;

            }
            else
            {
                light.enabled = false;
            }
        }
        catch (InvalidOperationException e) { Debug.LogError(e); }
    }
}
