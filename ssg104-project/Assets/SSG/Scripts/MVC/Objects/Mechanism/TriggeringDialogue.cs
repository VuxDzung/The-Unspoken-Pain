using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeringDialogue : MonoBehaviour
{
    private InteractiveObject item => GetComponent<InteractiveObject>();
    private DialogueManager manager => FindObjectOfType<DialogueManager>();
    [SerializeField] private Story story;
    void Start()
    {
        if (manager == null) return;
        if (story != null) item.nonInteractAction += TriggerNonActive;
    }
    private void TriggerNonActive(GameObject obj)
    {
        manager.LoadMainStory(story, 0, 0);
        GameManager.Instance.ChangeToDialogueView();
    }
}
