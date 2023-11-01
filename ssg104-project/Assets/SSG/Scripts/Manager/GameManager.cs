using System;
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
    public SourceManager source => FindObjectOfType<SourceManager>();
    private int dialogueViewOrder = -1;

    public string currentLetter { get; set; }

    //internal int password = 0;
    //internal int[] passDigits = new int[4];

    protected override void Awake()
    {
        base.Awake();
        LoadProcess();
        //password = UnityEngine.Random.Range(1010, 9900);
        //for(int i=0; i<passDigits.Length;i++)
            //passDigits[i] = (int)(password / Math.Pow(10, passDigits.Length - i - 1)) % 10;

    }

    private void Start()
    {
        ChangeCanvas(0);
        dialogueViewOrder = gameViews.Length + 1;
    }

    private void OnLevelWasLoaded(int level)
    {
        LoadInGame();
        dialogueViewOrder = gameViews.Length + 1;
        ChangeCanvas(0);
    }

    private GameObject[] gameViews
    {
        get { return source.gameViews; }
        set {}
    }
    private GameObject[] externalViews
    {
        get { return source.externalViews; }
        set {}
    }
    private InteractiveModel curNavObj = null;
    [HideInInspector] public bool inGame = true;

    public InteractiveView[] getAllView()
    {
        InteractiveView[] views = new InteractiveView[gameViews.Length + externalViews.Length];
        for (int i = 0; i < gameViews.Length + externalViews.Length; i++)
        {
            if (i < gameViews.Length) views[i] = gameViews[i].GetComponent<InteractiveView>();
            else views[i] = externalViews[i - gameViews.Length].GetComponent<InteractiveView>();
        }
        return views;
    }
    // 0: default
    // array order + 1: canvas
    public void ChangeCanvas(int ord)
    {
        if (ord < gameViews.Length)
        {
            if (ord != 0) inGame = false;
            else inGame = true;
            ActiveViews(gameViews, ord);
        }
        else
        {
            ActiveViews(externalViews, ord - gameViews.Length);
            return;
        }
        LoadInGame();
    }
    private void ActiveViews(GameObject[] views, int ord)
    {
        foreach (GameObject v in views)
            if (v.activeSelf) v.SetActive(false);
        foreach (GameObject v in views)
            if (views[ord] == null) continue;
            else if (views[ord] == v) v.SetActive(true);
    }
    public void OutExternalView()
    {
        ChangeCanvas(gameViews.Length);
    }
    public void ChangeToDialogueView()
    {
        ChangeCanvas(dialogueViewOrder);
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
                if (interactive != null && !interactive.interactable)
                {
                    interactive.interactable = true;
                    if (!itemData.itemCanInteract.Contains(name)) itemData.itemCanInteract.Add(name);
                }
                Debug.Log($"Add {item.name} in {dialogueManager.CurrentScene}");
            }
            else // item is in different scene
            {
                if (itemData.itemCanInteract.Contains(name)) continue;
                itemData.itemCanInteract.Add(name);
                Debug.Log($"Add in RAM: {name}");
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
        SaveLoadSystem.SaveScene(dialogueManager.CurrentScene);
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
        if (itemData.itemCanInteract != null) AddInteractedItemsByName(itemData.itemCanInteract.ToArray());
        if (inventory != null) inventory.LoadInventory();
    }

    public void RemoveAllItem()
    {
        if (itemData.itemCanInteract != null) itemData.itemCanInteract.Clear();

        if (itemData.itemsInventory != null) itemData.itemsInventory.Clear();
    }


    public void ChangeToScene(string sceneName) => SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    public void ChangeToScene(string sceneName, LoadSceneMode loadSceneMode) => SceneManager.LoadScene(sceneName, loadSceneMode);
}
