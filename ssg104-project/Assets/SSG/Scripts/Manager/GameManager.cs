using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : GenericSingleton<GameManager>
{
    PlayerController player => FindObjectOfType<PlayerController>();
    public InventoryManager inventory => FindObjectOfType<InventoryManager>();
    public DialogueManager dialogueManager => FindObjectOfType<DialogueManager>();

    public PlatformerData itemData = new PlatformerData();
    SourceManager source => FindObjectOfType<SourceManager>();

    protected override void Awake()
    {
        base.Awake();
        LoadProcess();
    }

    private void OnLevelWasLoaded(int level)
    {
        LoadInGame();
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
        for (int i = 0; i < gameViews.Length; i++)
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
        LoadInGame();
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
    public void AddInteractedItemsByName(string[] items)
    {
        //add call object's can interacted names 
        foreach (var name in items)
        {
            GameObject item = GameObject.Find(name);
            if (item != null)// item is in the same scene
            {
                InteractiveObject interactive = item.GetComponent<InteractiveObject>();
                if (interactive != null && !interactive.interactable) interactive.interactable = true;
                Debug.Log($"Add {item.name}");
            }
            else // item is in different scene
            {
                itemData.itemCanInteract.Add(name);
            }
        }
    }

    public void AddInventoryItem(GameObject item)
    {
        inventory.AddItem(item);
    }

    public void AddInventoryItemByName(string[] itemNames)
    {
        inventory.AddItemByName(itemNames);
    }

    public void SaveProcess()
    {
        //source.SaveProcess();
        SaveLoadSystem.SavePlatformer(itemData);
    }

    private void LoadProcess()
    {
        if (SaveLoadSystem.LoadPlatformer() != null)
        {
            itemData = SaveLoadSystem.LoadPlatformer();//Load the previous play saved items
        }
    }
    private void LoadInGame()
    {
        Debug.Log(itemData);
        AddInteractedItemsByName(itemData.itemCanInteract.ToArray());
        inventory.LoadInventory();
    }

    public bool HadInteracted(InteractiveModel currentObject)
    {
        return false;
    }

    public void ChangeToScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    public void ChangeToScene(string sceneName, LoadSceneMode loadSceneMode) => SceneManager.LoadScene(sceneName, loadSceneMode);
}
