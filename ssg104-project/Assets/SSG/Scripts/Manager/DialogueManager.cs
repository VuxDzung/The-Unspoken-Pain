using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

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
    [SerializeField] protected Story[] stories;
    [SerializeField] private TextMeshProUGUI tmp;
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

    protected Action endDialogueAction;
    protected Action endMainStoryAction;

    [SerializeField] protected List<Branch> decisionBranches = new List<Branch>();
    internal int SoT = 0; //Story On Track
    internal int BoT = 0; //Branch On Track
    internal int DoT = 0; //Dialogue On Track


    protected Branch branchOnTrack;
    protected Dialogue dialogueOnTrack;

    [SerializeField] protected int startStory = 0;
    [SerializeField] protected bool playOnStart = false;
    [Header("Character View")]
    [SerializeField] private Image characterImg;
    [SerializeField] private TextMeshProUGUI tmpCharacterName;
    [SerializeField] private Image background;

    [Header("Dialogue view")]
    [SerializeField] private GameObject choicesView;
    [SerializeField] private TextMeshProUGUI tmpDialouge;

    [Header("Buttons")]
    [SerializeField] private Button backToHome;
    [SerializeField] private Button autoButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button[] choiceButtons;

    private bool isSkipping = false;
    private bool choiceIsClicked = false;
    private bool hasReachChoiceNode = false;
    protected virtual void Start()
    {
        for(int i = 0; i < choiceButtons.Length; i++)
        {
            int temp = i;
            Debug.Log(temp);
            choiceButtons[temp].onClick.AddListener(() => Choice(temp));
        }

        if (playOnStart)
            LoadMainStory(stories[startStory]);

        if (skipButton != null)
        {
            skipButton.onClick.AddListener(Skip);
        }
    }

    public void LoadMainStory(Story mainStory)
    {
        this.mainStory = mainStory;
        for(int i = 0;i < stories.Length; i++) 
        {
            if (stories[i] == mainStory) SoT = i; break;
        }
        BuildDecision();
    }

    protected virtual void Update()
    {
        
    }

    public void Skip()
    {
        DoT = branchOnTrack.dialogues.Count - 1;
        DialogueTrigger();
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
                    newBranch.dialogues.Add(subDialogues[j]);
                    
                    decisionBranches.Add(newBranch);
                    curBranch.subBranchs.Add(newBranch);
                }
            }
        }

        branchOnTrack = decisionBranches[0];
        dialogueOnTrack = branchOnTrack.dialogues[0];
        DialogueTrigger();
    }

    protected void nextDialogue()
    {
        DoT++;
        if (DoT >= branchOnTrack.dialogues.Count) { Debug.Log("End"); endDialogueAction?.Invoke(); return; }

        DialogueTrigger();
    }

    protected void Choice(int choice)
    {
        Branch choosingBranch = branchOnTrack.subBranchs[choice];
        for (int i = 0; i < decisionBranches.Count; i++)
        {
            int temp = i;

            if (decisionBranches[temp] == choosingBranch)
            {
                /*
                 * change branch story here
                 * remove 2 changes after if neccessary
                 */
                BoT = temp;
                DoT = 0;
                break;
            }
        }
        
        DialogueTrigger();
    }

    protected void ShowDialogue(Dialogue dialogue)
    {
        Debug.Log(dialogue.dialouge);
        tmpCharacterName.text = dialogue.characterName;
        tmpDialouge.text = dialogue.dialouge;
        characterImg.sprite = dialogue.characterImage;


        //UI coding for this dialogue
        List<Dialogue> nextDialogues = dialogue.subDialogues;
        if (nextDialogues.Count > 1)
        {
            choicesView.SetActive(true);
            
        }
        else
        {
            if (choicesView.activeSelf)
            {
                choicesView.SetActive(false);
            }
        }
    }

    void DialogueTrigger()
    {
        branchOnTrack = decisionBranches[BoT];
        dialogueOnTrack = branchOnTrack.dialogues[DoT];

        ShowDialogue(dialogueOnTrack);
    }
}
