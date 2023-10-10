using UnityEngine;
using cherrydev;
using System.Collections.Generic;
public class DialougeTrigger : MonoBehaviour
{
    [SerializeField] private bool triggerOnStart = false;
    [SerializeField] private DialogBehaviour dialogBehaviour;
    [SerializeField] private DialogNodeGraph dialogGraph;


    public DialogNodeGraph GetGraph => dialogGraph;
    private void Start()
    {
        if (triggerOnStart)
        {
            dialogBehaviour.StartDialog(dialogGraph);//use for visual novel scene
        }
    }

    //Use for interact with other characters or objects
    public void OnTriggerConversation(DialogNodeGraph _dialogGraph)
    {
        dialogBehaviour.StartDialog(_dialogGraph);
    }
}