using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveView : MonoBehaviour
{
    [SerializeField] private GameObject linkObject;
    private void OnEnable()
    {
        if (linkObject == null) return;
        linkObject.SetActive(true);
    }
    private void OnDisable()
    {
        if (linkObject == null) return;
        linkObject.SetActive(false);
    }
}
