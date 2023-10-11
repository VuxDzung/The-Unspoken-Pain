using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDialogueView : DialogueManager
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextDialogue();
        }
    }
}
