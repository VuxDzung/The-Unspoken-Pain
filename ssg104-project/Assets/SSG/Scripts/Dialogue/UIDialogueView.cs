using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIDialogueView : MonoBehaviour
{
    private DialogueManager manager => GetComponentInParent<DialogueManager>();

    [SerializeField] protected bool playOnStart = false;
    
    private GameObject dialogueBox => GameObject.FindGameObjectWithTag("DialogueBox");
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

    [Header("SFX")]
    [SerializeField] private AudioSource sourceSFX;
    

    private string playerName = "";
    private ChangeColor fader => GetComponent<ChangeColor>();

    private void Awake()
    {
        playerName = SaveLoadSystem.LoadPlayerName();
        manager.showDialogue += ShowDialogue;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            int temp;
            temp = i;
            choiceButtons[temp].onClick.AddListener(() => manager.Choice(temp));
        }

        if (skipButton != null) skipButton.onClick.AddListener(manager.Skip);

        if (nextButton != null) nextButton.onClick.AddListener(manager.nextDialogueByButton);

        if (saveButton != null) saveButton.onClick.AddListener(manager.SaveStoryProgress);

        if (backToHome != null) backToHome.onClick.AddListener(() => GameManager.Instance.ChangeToScene("Home"));
    }

    private void Start()
    {
        StoryData storyData = new StoryData();
        storyData = SaveLoadSystem.LoadStory();
        if (playOnStart && storyData != null)
        {
            manager.LoadMainStory(manager.stories[storyData.SoT], storyData.BoT, storyData.DoT);
            Debug.Log($"SoT: {storyData.SoT}, BoT: {storyData.BoT}, DoT: {storyData.DoT}");
        }
    }

    private void OnDisable()
    {
        
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        if (dialogue.nodeSFX != null) sourceSFX.PlayOneShot(dialogue.nodeSFX, sourceSFX.volume);

        if (characterImg != null) characterImg.sprite = dialogue.characterImage;

        if (background != null) background.sprite = dialogue.backGroundImage;

        if (dialogue.dialogue.Length == 0)
        {
            dialogueBox.SetActive(false);
            return;
        }
        else
        {
            if (!dialogueBox.activeSelf) dialogueBox.SetActive(true);
        }

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
    bool fadingActive = true;
    private void FadingView()
    {
        if (!manager.changeStory) return;
        if (fader != null && fadingActive)
        {
            fader.Active();
            fadingActive = false;
        }
        if(fader.ratio >= 50f)
        {
            manager.nextStory();
            fadingActive = true;
            manager.changeStory = false;
        }
        
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.nextDialogue();
        }
        FadingView();
    }
}
