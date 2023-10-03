using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class MouseHover : MonoBehaviour
{
    public static event Action<Vector2> UISubject;

    [SerializeField] private GameObject interactable;
    [SerializeField] private LayerMask hoverMask;
    [SerializeField] private string message = "Left click to talk";

    private TextMeshProUGUI tmpMessage;

    private void Start()
    {
        tmpMessage = interactable.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        tmpMessage.text = message;
        interactable.SetActive(false);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, hoverMask))
        {
            interactable.SetActive(true);
            interactable.transform.position = Camera.main.WorldToScreenPoint(new Vector3(hit.point.x + 1, hit.point.y + 1, hit.point.z));
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"I click on {hit.transform.name}");
            }
        }
        else
        {
            interactable.SetActive(false);
        }
    }
}
