using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] protected InteractiveView view;
    [HideInInspector] public int viewOrder = 0;
    protected virtual void Start()
    {
        SetViewOrder();
    }

    private void SetViewOrder()
    {
        if (view == null) return;
        InteractiveView[] views = GameManager.Instance.getAllView();
        for (int i = 0; i < views.Length; i++)
        {
            if (view == views[i]) viewOrder = i;
        }
    }
    public void OnOpenView()
    {
        GameManager.Instance.ChangeCanvas(viewOrder);
    }

    public void OnOpenDefaultView()
    {
        GameManager.Instance.ChangeCanvas(0);
    }

    
}
