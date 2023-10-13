using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDialogueView : MonoBehaviour
{
    private DialogueManager manager => GetComponent<DialogueManager>();
    [SerializeField] protected bool playOnStart = false;
    [Header("Character View")]
    [SerializeField] private Image characterImg;
    [SerializeField] private TextMeshProUGUI tmpCharacterName;
    [SerializeField] private Image background;

    [Header("Dialogue view")]
    [SerializeField] private GameObject choicesView;
    [SerializeField] private TextMeshProUGUI tmpDialogue;

    [Header("Buttons")]
    [SerializeField] private Button backToHome;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button[] choiceButtons;

    private void Start()
    {
        manager.showDialogue += ShowDialogue;
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int temp;
            temp = i;
            choiceButtons[temp].onClick.AddListener(() => manager.Choice(temp));
        }

        StoryData storyData = new StoryData();
        storyData = SaveLoadSystem.LoadStory();
        if (playOnStart && storyData != null)
        {
            manager.LoadMainStory(manager.stories[storyData.SoT], storyData.BoT, storyData.DoT);
            Debug.Log($"SoT: {storyData.SoT}, BoT: {storyData.BoT}, DoT: {storyData.DoT}");
        }
            

        if (skipButton != null) skipButton.onClick.AddListener(manager.Skip);
        if (nextButton != null) nextButton.onClick.AddListener(manager.nextDialogue);
        if (saveButton != null) saveButton.onClick.AddListener(manager.SaveStoryProgress);
    }

    

    private void ShowDialogue(Dialogue dialogue)
    {
        tmpCharacterName.text = dialogue.characterName;
        tmpDialogue.text = dialogue.dialogue;
        characterImg.sprite = dialogue.characterImage;
        //triggering interable of obj has name in current dialogue node's name of trigged items
        foreach(var name in dialogue.nameOfTriggedItems)
        {
            GameObject item = GameObject.Find(name);
            InteractiveModel interactive = item.GetComponent<InteractiveModel>();
            if(interactive != null) interactive.interactable = true;
        }

        //UI coding for this dialogue
        List<Dialogue> subDialogues = dialogue.subDialogues;
        if (subDialogues.Count > 1)
        {
            choicesView.SetActive(true);
            for (int i = 0; i < subDialogues.Count; i++)
            {
                int temp = i;
                TextMeshProUGUI choiceDialogue = choiceButtons[temp].transform.GetChild(0).GetComponent<TextMeshProUGUI>();
                choiceDialogue.text = subDialogues[i].dialogue;
            }
        }
        else
        {
            if (choicesView.activeSelf)
            {
                choicesView.SetActive(false);
            }
        }
    }
    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.nextDialogue();
        }
    }
}
