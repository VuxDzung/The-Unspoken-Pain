using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Load
{
    view,
    scene,
    none
}

public class InteractiveObject : MonoBehaviour
{
    public bool interactable = true;
    [SerializeField] private Load loadType;
    [SerializeField] protected InteractiveView view;
    [SerializeField] private string scene;
    [SerializeField] private bool outExternalView;
    [SerializeField] private bool allowToDefaultView = false;
    internal int viewOrder = 0;
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
        if (GameManager.Instance.dialogueManager != null)
        GameManager.Instance.dialogueManager.allowToDefaultView = allowToDefaultView;
        switch (loadType)
        {
            case Load.view:
                GameManager.Instance.ChangeCanvas(viewOrder);
                if (outExternalView) GameManager.Instance.OutExternalView();
                break;
            case Load.scene:
                GameManager.Instance.ChangeToScene(scene); break;
            case Load.none: break;
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
