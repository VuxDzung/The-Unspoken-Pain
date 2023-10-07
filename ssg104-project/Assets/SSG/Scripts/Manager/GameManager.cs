using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<GameManager>
{
    PlayerController player => FindObjectOfType<PlayerController>();
    SourceManager source => FindObjectOfType<SourceManager>();
    private List<GameObject> activeObject
    {
        get { return source.activeObject; }
        set { activeObject = source.activeObject; }
    }
    private GameObject[] gameViews
    {
        get { return source.gameViews; }
        set { gameViews = source.gameViews; }
    }
    private InteractiveModel curNavObj = null;
    [HideInInspector] public bool inGame = true;
    void Start()
    {
        SetUpActiveObj();
    }
    void SetUpActiveObj()
    {
        InteractiveModel[] model = FindObjectsOfType<InteractiveModel>();
        foreach (InteractiveModel m in model)
        {
            GameObject obj = m.gameObject;
            if (obj == null) continue;
            if (obj.activeSelf) activeObject.Add(m.gameObject);
        }
    }
    public InteractiveView[] getAllView()
    {
        InteractiveView[] views = new InteractiveView[gameViews.Length];
        for(int i = 0; i < gameViews.Length; i++)
        {
            views[i] = gameViews[i].GetComponent<InteractiveView>();
        }
        return views;
    }
    private void SetObjInGame(int ord)
    {

        if (ord != 0)
        {
            foreach (var obj in activeObject) obj.SetActive(false);
            inGame = false;
        }    
        else
        {
            foreach (var obj in activeObject) obj.SetActive(true);
            inGame = true;
        }
             
    }
    // 0: default
    // array order + 1: canvas
    public void ChangeCanvas(int ord)
    {
        SetObjInGame(ord);
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
}
