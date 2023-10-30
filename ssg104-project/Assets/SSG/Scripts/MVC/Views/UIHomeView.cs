using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class UIHomeView : MonoBehaviour
{
    [SerializeField] private List<string> platformerScenes = new List<string>();
    [SerializeField] private Button newGameBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button aboutUsBtn;

    private void Start()
    {
        if (continueBtn != null)
            continueBtn.onClick.AddListener(ContinueGame);

        if (aboutUsBtn != null)
            aboutUsBtn.onClick.AddListener(GoToDevsPage);

        //if there's no previous saved data, then the continue button is not available
        if (!SaveLoadSystem.HasPlayerData())
        {
            Color continueBtnColor = continueBtn.transform.GetComponent<Image>().color;
            continueBtnColor.a = 0.3f;
            continueBtn.transform.GetComponent<Image>().color = continueBtnColor;
            continueBtn.enabled = false;
        }
        else
        {

        }
    }

    public void ContinueGame()
    {
        //Change to previous game scene
        string sceneName = SaveLoadSystem.LoadScene();

        if (!platformerScenes.Contains(sceneName))
        {
            GameManager.Instance.RemoveAllItem();
        }
        GameManager.Instance.ChangeToScene(sceneName);
    }

    public void GoToDevsPage()
    {
        Application.OpenURL("https://www.facebook.com/61552327743443/posts/pfbid0gA9p5dhjtV3cvckbYnAv1VeYeuPTz2sTs4xtbqSXLLW8cXJS2v2s83o91t5VR4RVl/?mibextid=cr9u03");
    }

    public void StartNewGame()
    {
        SaveLoadSystem.SaveScene("");
        SaveLoadSystem.SavePlatformer(null);
        SaveLoadSystem.SaveStory(null);
    }    
}
