using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/*
 * Summary: Add this component to a view
 */

public class PlayerLogin : MonoBehaviour
{
    private static string newGameScene = "OpeningScene";
    [SerializeField] private TMP_InputField tmpPlayerNameInput;
    [SerializeField] private Button confirmBtn;
    [SerializeField] private TextMeshProUGUI warningTxt;
    [SerializeField] private TextMeshProUGUI warning_HasData; 

    private void Start()
    {
        warningTxt.gameObject.SetActive(false);

        if (SaveLoadSystem.HasPlayerData()) warning_HasData.gameObject.SetActive(true);
        else warning_HasData.gameObject.SetActive(false);

        if (confirmBtn != null)
            confirmBtn.onClick.AddListener(SetNewGameData);
    }


    public void SetNewGameData()
    {
        SaveLoadSystem.SaveScene("");
        SaveLoadSystem.SavePlatformer(null);
        SaveLoadSystem.SaveStory(null);

        string name = tmpPlayerNameInput.text;
        if (name.Length == 0)
        {
            Debug.LogError("Player name input field cannot be empty");
            warningTxt.gameObject.SetActive(true);
            warningTxt.text = "Player name input field cannot be empty";
            return;
        }
        else if (name.Length > 13)
        {
            Debug.LogError("Player name cannot longer than 13 digits");
            warningTxt.gameObject.SetActive(true);
            warningTxt.text = "Player name cannot longer than 13 digits";
            return;
        }
        else
        {
            SaveLoadSystem.SavePlayerName(name);
            GameManager.Instance.ChangeToScene(newGameScene);
        }
    }
}
