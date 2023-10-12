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

        if (playOnStart)
            manager.LoadMainStory(manager.stories[0]);

        if (skipButton != null) skipButton.onClick.AddListener(manager.Skip);
        if (nextButton != null) nextButton.onClick.AddListener(manager.nextDialogue);
    }
    private void ShowDialogue(Dialogue dialogue)
    {
        tmpCharacterName.text = dialogue.characterName;
        tmpDialogue.text = dialogue.dialouge;
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
    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space))
        {
            manager.nextDialogue();
        }
    }
}
