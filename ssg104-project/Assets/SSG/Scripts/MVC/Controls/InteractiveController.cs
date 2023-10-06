using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveController : MonoBehaviour
{
    [SerializeField] private Movement playerEngine;
    [SerializeField] private LayerMask interactiveMask;
    private InteractiveModel pointToModel = null;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactiveMask))
        {
            pointToModel = hit.transform.GetComponent<InteractiveModel>();
            pointToModel.SetMouseOn(true);
            if (Input.GetMouseButtonDown(0))
            {
                pointToModel.NavigateTo();
            }
        }
        else
        {
            if (pointToModel == null) return;
            pointToModel.SetMouseOn(false);
            pointToModel = null;
        }
    }
}
