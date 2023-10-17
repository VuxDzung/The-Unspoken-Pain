using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    PlayerController player => FindObjectOfType<PlayerController>();

    public PlatformerData objectCanIneracts = new PlatformerData();
    SourceManager source => FindObjectOfType<SourceManager>();

    protected override void Awake()
    {
        base.Awake();
        
        if (SaveLoadSystem.LoadPlatformer() != null)
        {
            objectCanIneracts = SaveLoadSystem.LoadPlatformer();//Load the previous play saved items
        }
    }

    private GameObject[] gameViews
    {
        get { return source.gameViews; }
        set { gameViews = source.gameViews; }
    }
    private InteractiveModel curNavObj = null;
    [HideInInspector] public bool inGame = true;
    public InteractiveView[] getAllView()
    {
        InteractiveView[] views = new InteractiveView[gameViews.Length];
        for(int i = 0; i < gameViews.Length; i++)
        {
            views[i] = gameViews[i].GetComponent<InteractiveView>();
        }
        return views;
    }
    // 0: default
    // array order + 1: canvas
    public void ChangeCanvas(int ord)
    {
        if (ord != 0) inGame = false;
        else inGame = true;
        foreach (GameObject v in gameViews) 
            if (v.activeSelf) v.SetActive(false);
        foreach (GameObject v in gameViews)
            if (gameViews[ord] == null) continue;
            else if (gameViews[ord] == v) v.SetActive(true);
    }
    public void SetMoveNavigate(InteractiveModel obj)
    {
        if(!inGame) return;
        player.SetNavigate(obj.transform.position.x);
        curNavObj = obj;
    }
    void Update()
    {
        if (!inGame) return;
        if(curNavObj != null && !player.onNav)
        {
            curNavObj.OnAction();
            curNavObj = null;
        }
    }
    public void AddInteractedItems(string[] items)
    {
        //add call object's can interacted names 
        foreach (var name in items)
        {
            /*
            GameObject item = GameObject.Find(name);
            //if(item == null) SaveLoadSystem.queueItems.Add(name);
            InteractiveObject interactive = item.GetComponent<InteractiveObject>();
            
            if (interactive != null)
            {
                interactive.interactable = true;
                
            }
            */
            if (objectCanIneracts.objectWaits.Contains(name)) continue;//If the list has already containned the object, then ignore it

            objectCanIneracts.objectWaits.Add(name);
            Debug.Log($"Added: {name}");
        }
    }
    public void PlatformerSaveProcess()
    {
        //source.SaveProcess();
        SaveLoadSystem.SavePlatformer(objectCanIneracts);
    }


    public bool HadInteracted(InteractiveModel currentObject)
    {
        return false;
    }

    public void ChangeToScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
}
