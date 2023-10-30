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
    [SerializeField] private string currentScene = "";
    [SerializeField] private string nextScene = "";
    public bool allowToDefaultView = false;

    public string CurrentScene => currentScene;

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

    [SerializeField] private List<Branch> decisionBranches = new List<Branch>();
    internal int SoT = 0; //Story On Track
    internal int BoT = 0; //Branch On Track
    internal int DoT = 0; //Dialogue On Track

    protected Branch branchOnTrack;
    protected Dialogue dialogueOnTrack;

    [SerializeField] private bool connectedStory;
    [SerializeField] private List<int3> connectNote = new List<int3>();

    [SerializeField] private float skipDelay = 0.8f;
    internal bool onSkip = false;
    internal bool changeStory = false;
    public void LoadMainStory(Story mainStory, int BoT, int DoT)
    {
        //Clear the previous story
        decisionBranches.Clear();
        this.mainStory = mainStory;
        for (int i = 0; i < stories.Length; i++)
        {
            if (stories[i].Equals(mainStory))
            {
                SoT = i;
                break;
            }
        }

        BuildDecision(); 
        OnStartDialogueTrigger(BoT, DoT);
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
    public void nextDialogueByButton()
    {
        if (onSkip) return;
        nextDialogue();
    }
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
            else if(branchOnTrack.subBranchs.Count == 0)
            {
                if(stories.Length == 0)
                {
                    GameManager.Instance.ChangeCanvas(GameManager.Instance.source.gameViews.Length);
                    if (allowToDefaultView) GameManager.Instance.ChangeCanvas(0);
                    return;
                }
                changeStory = true;
            }
            return;
        }
        DialogueTrigger();

    }

    public void nextStory()
    {
        // Increment the index to move to the next story
        SoT++;
        // Check if there is another story available
        if (SoT <= stories.Length - 1)
        {
            // Reset the branch and dialogue indices for the new story
            BoT = 0;
            DoT = 0;

            //Clear the previous story
            decisionBranches.Clear();

            // Load and build the new decision branches for the new story
            mainStory = stories[SoT];
            BuildDecision();

            // Trigger the first dialogue of the new story
            DialogueTrigger();
        }
        else
        {
            //if the next scene is null, then the dialogue view will turn off
            Debug.LogWarning("No more stories available. End of the game or perform other actions.");
            if (nextScene.Length == 0)
            {
                GameManager.Instance.OutExternalView();
                if (allowToDefaultView) GameManager.Instance.ChangeCanvas(0);
            }
            else
            {
                GameManager.Instance.ChangeToScene(nextScene);
            }
        }
    }
    public void Skip()
    {
        if (onSkip) return;
        onSkip = true;
        StartCoroutine(SkipStep());
    }
    private IEnumerator SkipStep()
    {
        nextDialogue();
        yield return new WaitForSeconds(skipDelay);
        if (DoT == branchOnTrack.dialogues.Count - 1) onSkip = false;
        else StartCoroutine(SkipStep());
    }

    public void Choice(int choice)
    {
        Branch choosingBranch = branchOnTrack.subBranchs[choice];
        
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
    void OnStartDialogueTrigger(int BoT, int DoT)
    {
        this.BoT = BoT; 
        this.DoT = DoT;
        DialogueTrigger();
    }

    void DialogueTrigger()
    {
        branchOnTrack = decisionBranches[BoT];
        dialogueOnTrack = branchOnTrack.dialogues[DoT];

        showDialogue.Invoke(dialogueOnTrack);
    }

    public void SaveStoryProgress()
    {
        if (onSkip) return;
        StoryData savedData = new StoryData();
        savedData.SoT = SoT;
        savedData.BoT = BoT;
        savedData.DoT = DoT;

        SaveLoadSystem.SaveScene(currentScene);
        SaveLoadSystem.SaveStory(savedData);
    }

    public bool ReachEndOfStory()
    {
        return DoT >= branchOnTrack.dialogues.Count - 1;
    }
}
