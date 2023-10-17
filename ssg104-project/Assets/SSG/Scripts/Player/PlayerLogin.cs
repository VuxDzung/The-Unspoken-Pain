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

    private void Start()
    {
        if (confirmBtn != null)
            confirmBtn.onClick.AddListener(SetNewGameData);
    }


    public void SetNewGameData()
    {
        string name = tmpPlayerNameInput.text;
        if (name.Length == 0)
        {
            Debug.LogError("Player name input field cannot be empty");
        }
        else
        {
            SaveLoadSystem.Save(name);
            GameManager.Instance.ChangeToScene(newGameScene);
        }
    }
}
