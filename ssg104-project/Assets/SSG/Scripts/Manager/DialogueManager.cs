using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
using Unity.Mathematics;

[Serializable]
public class Branch
{
    public List<Dialogue> dialogues = new List<Dialogue>();
    public List<Branch> subBranchs = new List<Branch>();
}
/*
 * Idea: the first element of decision branch is the main one, from 1 beyond are choosen branches that scale from main (element 0)
 */

public class DialogueManager : MonoBehaviour
{
    public Story[] stories;
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

    internal Action<Dialogue> showDialogue;
    internal Action endDialogueAction;
    internal Action endMainStoryAction;

    [SerializeField] private List<Branch> decisionBranches = new List<Branch>();
    internal int SoT = 0; //Story On Track
    internal int BoT = 0; //Branch On Track
    internal int DoT = 0; //Dialogue On Track


    protected Branch branchOnTrack;
    protected Dialogue dialogueOnTrack;

    [SerializeField] private bool connectedStory;
    [SerializeField] private List<int3> connectNote = new List<int3>();


    public void LoadMainStory(Story mainStory)
    {
        this.mainStory = mainStory;
        for (int i = 0; i < stories.Length; i++)
        {
            if (stories[i] == mainStory) SoT = i; break;
        }
        BuildDecision(); 
        DefaultDialogueTrigger();
    }

    public void Skip()
    {
        DoT = branchOnTrack.dialogues.Count - 1;
        DialogueTrigger();
    }

    public void BuildDecision()
    {
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
            if (subCount == 1)
            {
                Dialogue sub = curDialogue.subDialogues[0];
                if (CheckLoop(curBranch, sub)) continue;
                curBranch.dialogues.Add(sub);
            }
            else if (subCount > 1)
            {
                List<Dialogue> subDialogues = curDialogue.subDialogues;
                for (int j = 0; j < subCount; j++)
                {
                    //if (CheckLoop(curBranch, subDialogues[j])) continue;
                    Branch newBranch = new Branch();
                    newBranch.dialogues.Add(subDialogues[j]);

                    decisionBranches.Add(newBranch);
                    curBranch.subBranchs.Add(newBranch);
                }
            }
        }
    }
    #region Connect-code
    private bool CheckLoop(Branch curBranch, Dialogue sub)
    {
        if (!connectedStory) return false;
        foreach (var branch in decisionBranches)
        {
            if (branch.dialogues.Contains(sub))
            {
                
                curBranch.subBranchs.Add(branch);
                int3 pos = ConnectPos(curBranch, branch, sub);
                connectNote.Add(pos);
                return true;
            }
        }
        return false;
    }
    private int3 ConnectPos(Branch curBranch, Branch connectBranch, Dialogue connectNode)
    {
        return new int3(decisionBranches.IndexOf(curBranch), decisionBranches.IndexOf(connectBranch), connectBranch.dialogues.IndexOf(connectNode));
    }
    private void SendToConnectNode()
    {
        foreach (var pos in connectNote)
        {
            if (branchOnTrack == decisionBranches[pos.x])
            {
                BoT = pos.y;
                DoT = pos.z;
                break;
            }
        }
        DialogueTrigger();
    }
    #endregion
    public void nextDialogue()
    {
        DoT++;
        if (DoT >= branchOnTrack.dialogues.Count)
        {
            if (connectedStory && branchOnTrack.subBranchs.Count == 1)
            {
                Debug.Log("Connecting");
                SendToConnectNode();
            }
            else
            {
                Debug.Log("End");
                endDialogueAction?.Invoke();
            }
            return;
        }
        DialogueTrigger();
    }

    public void Choice(int choice)
    {
        Debug.Log("Choose: " + choice);

        Branch choosingBranch = branchOnTrack.subBranchs[choice];
        Debug.Log(decisionBranches.IndexOf(branchOnTrack));
        Debug.Log(decisionBranches.IndexOf(choosingBranch));
        for (int i = 0; i < decisionBranches.Count; i++)
        {
            if (decisionBranches[i] == choosingBranch)
            {
                BoT = i;
                DoT = 0;
                break;
            }
        }

        DialogueTrigger();
    }
    void DefaultDialogueTrigger()
    {
        BoT = 0; DoT = 0;
        DialogueTrigger();
    }

    void DialogueTrigger()
    {
        branchOnTrack = decisionBranches[BoT];
        dialogueOnTrack = branchOnTrack.dialogues[DoT];

        showDialogue.Invoke(dialogueOnTrack);
    }
}
