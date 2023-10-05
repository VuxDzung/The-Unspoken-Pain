using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour
{
    [SerializeField] private Movement playerEngine;
    [SerializeField] private LayerMask interactiveMask;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactiveMask))
        {
            InteractiveModel interactModel = hit.transform.GetComponent<InteractiveModel>();
            if (Input.GetMouseButtonDown(0))
            {
                interactModel.OnOpenView();
                interactModel.DisplayConsoleMessage();
            }
        }
    }
}
