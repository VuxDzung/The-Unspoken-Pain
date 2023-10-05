using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public enum Category
{
    Humanoid,
    Object
}

public class InteractiveModel : MonoBehaviour
{
    [SerializeField] private Category category;
    [SerializeField] private InteractiveView view;
    public Action interactAction { get; protected set; }
    private int viewOrder = 0;
    private void Awake()
    {
        interactAction += OnOpenView;
    }
    private void Start()
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
    public void DisplayConsoleMessage()
    {
        switch (category)
        {
            case Category.Humanoid:
                Debug.Log($"Start talking with {this.gameObject.transform.name}");
                break;
            case Category.Object:
                Debug.Log($"Start interact with {this.gameObject.name}");
                break;
        }
    }
    public void NavigateTo()
    {
        GameManager.Instance.SetMoveNavigate(this);
    }
    public void OnAction()
    {
        interactAction?.Invoke();
    }

    public void OnOpenView()
    {
        GameManager.Instance.ChangeCanvas(viewOrder);
    }
}
