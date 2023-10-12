using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cherrydev;
public enum Category
{
    Humanoid,
    Object
}

public class InteractiveModel : InteractiveObject
{
    internal bool interactable;
    [SerializeField] private Category category;
    [SerializeField] private GameObject highline;

    private DialougeTrigger dialougeTrigger;
    public Action interactAction { get; set; }
    private void Awake()
    {
        dialougeTrigger = FindObjectOfType<DialougeTrigger>();
        interactAction += OnOpen;
    }
    protected override void Start()
    {
        base.Start();
        SetMouseOn(false);
    }

    public void SetMouseOn(bool active) 
    {
        if(highline == null) return;
        highline.SetActive(active);
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
        if (!interactable)
        {

        }
        else
        {
            interactAction?.Invoke();
        }
    }

    public void SetInteracted()
    {
        List<InteractiveModel> items = new List<InteractiveModel> { this };
        GameManager.Instance.AddInteractedItems(items);
    }
}
