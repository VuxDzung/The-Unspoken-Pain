using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VNCreator;
public class GameManager : GenericSingleton<GameManager>
{
    PlayerController player => FindObjectOfType<PlayerController>();
    //internal Dictionary<string, bool> mainStorySubjects = new Dictionary<string, bool>();
    //internal Dictionary<string, bool> inactiveStorySubjects = new Dictionary<string, bool>();
    internal Inventory platformerInventory;
    SourceManager source => FindObjectOfType<SourceManager>();
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
    public void AddInteractedItems(List<InteractiveModel> items)
    {
        foreach(var item in items)
        {
            item.interactable = true;
        }
        Debug.Log("Added");
    }
    public void PlatformerSaveProcess()
    {
        source.SaveProcess();
    }


    public bool HadInteracted(InteractiveModel currentObject)
    {
        return false;
    }
}
