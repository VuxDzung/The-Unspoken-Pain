using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using cherrydev;
using VNCreator;
public enum Category
{
    Humanoid,
    Object
}

public class InteractiveModel : InteractiveObject
{
    [SerializeField] private bool hasInfor;
    [SerializeField] private DialogNodeGraph mainDialogSO;
    [SerializeField] private DialogNodeGraph subDialogSO;
    [SerializeField] private Category category;
    [SerializeField] private GameObject highline;

    private DialougeTrigger dialougeTrigger;
    public Action interactAction { get; set; }
    private void Awake()
    {
        dialougeTrigger = FindObjectOfType<DialougeTrigger>();
        interactAction += OnOpenView;
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
        if (hasInfor)
        {
            if (!System.Object.ReferenceEquals(mainDialogSO, null))
            {
                if (!GameManager.Instance.HadInteracted(this))
                {
                    Debug.Log("HadInteracted_mainDialogSO");
                    interactAction?.Invoke();
                    dialougeTrigger.OnTriggerConversation(mainDialogSO);
                    mainDialogSO = null;
                    return;
                }
            }
            if (!System.Object.ReferenceEquals(subDialogSO, null))
            {
                Debug.Log("HadInteracted_subDialogSO");
                interactAction?.Invoke();
                dialougeTrigger.OnTriggerConversation(subDialogSO);
                return;
            }
        }
        else
        {
            interactAction?.Invoke();
        }
    }

    public void SetInteracted()
    {
        GameManager.Instance.AddInteractedObjects(this);
    }
}
