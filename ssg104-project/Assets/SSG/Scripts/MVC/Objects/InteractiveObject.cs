using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Load
{
    view,
    scene
}

public class InteractiveObject : MonoBehaviour
{
    public bool interactable = true;
    [SerializeField] private Load loadType;
    [SerializeField] protected InteractiveView view;
    [SerializeField] private string scene;
    [HideInInspector] public int viewOrder = 0;
    public Action<GameObject> interactAction { get; set; }
    public Action<GameObject> nonInteractAction { get; set; }

    protected virtual void Start()
    {
        interactAction += OnOpen;
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
    public void OnOpen(GameObject obj)
    {
        Debug.Log("Click");
        switch (loadType)
        {
            case Load.view:
                GameManager.Instance.ChangeCanvas(viewOrder); break;
            case Load.scene:
                GameManager.Instance.ChangeToScene(scene); break;
        }
    }

    public void OnOpenDefaultView()
    {
        GameManager.Instance.ChangeCanvas(0);
    }
    public void OnAction()
    {
        if (!interactable)
        {
            nonInteractAction?.Invoke(this.gameObject);
        }
        else
        {
            interactAction?.Invoke(this.gameObject);
        }
    }
}
