using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveView : MonoBehaviour
{
    [SerializeField] private Button backButton;
    private InteractiveButton backInteract => backButton.GetComponent<InteractiveButton>();
    private void Awake()
    {
        backInteract.viewOrder = 0;
    }
}
