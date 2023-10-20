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

    private string playerName = "";
    private bool hasChanged = false;
    private ChangeColor fader => GetComponent<ChangeColor>();

    private void Awake()
    {
        playerName = SaveLoadSystem.LoadPlayerName();
    }

    private void Start()
    {
        manager.endDialogueAction += FadingView;
        manager.showDialogue += ShowDialogue;
        //fader.onHalfRationAction += manager.nextStory;
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
        if (backToHome != null) backToHome.onClick.AddListener(() => GameManager.Instance.ChangeToScene("Home"));
    }

    private void OnDisable()
    {
        
    }

    private void ShowDialogue(Dialogue dialogue)
    {
        if (tmpCharacterName != null)
        {
            string rawName = dialogue.characterName;

            if (rawName.Contains("NVC")) tmpCharacterName.text = playerName;
            else tmpCharacterName.text = rawName;
        }    

        if (tmpDialogue != null)
        {
            string rawDialogue = dialogue.dialogue;

            if (rawDialogue.Contains("NVC")) tmpDialogue.text = rawDialogue.Replace("NVC", playerName);
            else tmpDialogue.text = rawDialogue;
        }
            

        if (characterImg != null)
            characterImg.sprite = dialogue.characterImage;

        if (background != null)
            background.sprite = dialogue.backGroundImage;

        //UI coding for this dialogue
        List<Dialogue> subDialogues = dialogue.subDialogues;
        if (subDialogues.Count > 1)
        {
            int choices = 0;
            choices = subDialogues.Count;
            choicesView.SetActive(true);
            for (int i = 0; i < choices; i++)
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

    private void FadingView()
    {
        if (fader != null)
        {
            fader.Activate = true;
            fader.halfwayActionInvoke = false;
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
