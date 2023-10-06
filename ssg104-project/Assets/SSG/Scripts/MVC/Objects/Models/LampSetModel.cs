using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampSetModel : MonoBehaviour
{
    InteractiveModel model => GetComponent<InteractiveModel>();
    Light light => GetComponent<Light>();
    void Start()
    {
        model.interactAction += TurnLight;
    }
    void TurnLight()
    {
        try
        {
            if (light.enabled == false) light.enabled = true;
            else light.enabled = false;
        }
        catch (InvalidOperationException e) { Debug.LogError(e); }
    }
}
