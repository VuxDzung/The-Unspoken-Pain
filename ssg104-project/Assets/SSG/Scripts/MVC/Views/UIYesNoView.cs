using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
public class UIYesNoView : MonoBehaviour
{
    [SerializeField] private bool isHomeView = true;
    [SerializeField] private string homeSceneName = "Home";
    [Header("UI properties")]
    [SerializeField] private TextMeshProUGUI titleName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    [Header("Display message")]
    [SerializeField] private string titleText;
    [SerializeField] private string descriptionText;
    [SerializeField] private string confimText;
    [SerializeField] private string cancelText;

    private void Start()
    {
        titleName.text = titleText;
        description.text = descriptionText;
        yesButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = confimText;
        noButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = cancelText;

        if (isHomeView)
        {
            yesButton.onClick.AddListener(() => ExitApplication());

#if UNITY_EDITOR
            yesButton.onClick.AddListener(() => { EditorApplication.ExitPlaymode(); });
#endif
        }
        else
        {
            yesButton.onClick.AddListener(() => GameManager.Instance.ChangeToScene(homeSceneName));
        }
    }

    public void ExitApplication() => Application.Quit();
}
