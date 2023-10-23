using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringDialogue : MonoBehaviour
{
    private InteractiveObject item => GetComponent<InteractiveObject>();
    private DialogueManager manager => FindObjectOfType<DialogueManager>();

    [SerializeField] private Story interactStory;
    [SerializeField] private Story noninteractStory;

    void Awake()
    {
        if (manager == null) { return; }

        if (interactStory != null) item.interactAction += TriggerActive;

        if (noninteractStory != null) item.nonInteractAction += TriggerNonActive;
    }

    private void TriggerNonActive(GameObject obj)
    {
        GameManager.Instance.ChangeToDialogueView();
        manager.LoadMainStory(noninteractStory, 0, 0);
    }

    private void TriggerActive(GameObject obj)
    {
        GameManager.Instance.ChangeToDialogueView();
        manager.LoadMainStory(interactStory, 0, 0);
    }
}
