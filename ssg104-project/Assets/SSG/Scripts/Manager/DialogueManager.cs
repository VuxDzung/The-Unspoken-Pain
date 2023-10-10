using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch
{
    internal List<Dialogue> dialogues = new List<Dialogue>();
    internal List<Branch> subBranchs = new List<Branch>();
}


public class DialogueManager : MonoBehaviour
{
    [SerializeField] Story[] stories;
    internal Story mainStory;
    private Dialogue[] storyDialogues
    {
        get
        {
            if (mainStory == null)
                return null;
            return mainStory.dialogues;
        }
        set { }
    }

    private List<Branch> decisionBranches = new List<Branch>();
    internal int SoT = 0; //Story On Track
    internal int BoT = 0; //Branch On Track
    internal int DoT = 0; //Dialogue On Track
    Branch branchOnTrack => decisionBranches[BoT];
    Dialogue dialogueOnTrack => branchOnTrack.dialogues[DoT];
    public void LoadMainStory(Story mainStory)
    {
        this.mainStory = mainStory;
        for(int i = 0;i < stories.Length; i++) 
        {
            if (stories[i] == mainStory) SoT = i; break;
        }
        BuildDecision();
    }

    public void BuildDecision() {
        Branch curBranch = new Branch();
        Branch firstBranch = new Branch();
        firstBranch.dialogues.Add(storyDialogues[0]);
        decisionBranches.Add(firstBranch);
        for (int i = 0; i < storyDialogues.Length; i++)
        {
            Dialogue curDialogue = storyDialogues[i];
            int subCount = curDialogue.subDialogues.Count;
            foreach (var branch in decisionBranches)
            {
                if (branch.dialogues.Contains(curDialogue))
                {
                    curBranch = branch;
                    break;
                }
            }
            if (subCount == 1) {
                Dialogue sub = curDialogue.subDialogues[0];
                curBranch.dialogues.Add(sub);
            }
            else if(subCount > 1){
                List<Dialogue> subDialogues = curDialogue.subDialogues;
                for (int j = 0; j < subCount; j++)
                {
                    Branch newBranch = new Branch();
                    newBranch.dialogues.Add(subDialogues[i]);
                    decisionBranches.Add(newBranch);
                    curBranch.subBranchs.Add(newBranch);
                }
            }
        }
    }
    void DialogueTrigger()
    {
        ShowDialogue(dialogueOnTrack);
    }
    public void nextDialogue()
    {
        if (DoT == branchOnTrack.dialogues.Count) return;
        DoT++;
        DialogueTrigger();
    }
    public void ShowDialogue(Dialogue dialogue) 
    {
        //UI coding for this dialogue
        List<Dialogue> subDialogues = dialogue.subDialogues;

    }
    public void Choice(int choice)
    {
        Branch choosingBranch = branchOnTrack.subBranchs[choice];
        for (int i = 0; i < decisionBranches.Count; i++)
        {
            if (decisionBranches[i] == choosingBranch) BoT = i; break;
        }
        DialogueTrigger();
    }
}
