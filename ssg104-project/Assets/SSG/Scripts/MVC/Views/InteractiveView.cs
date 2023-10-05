using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InteractiveView : MonoBehaviour
{
    [SerializeField] private Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(() => OnBackButtonClick());
    }

    public void OnBackButtonClick()
    {
        this.gameObject.SetActive(false);
    }
}
