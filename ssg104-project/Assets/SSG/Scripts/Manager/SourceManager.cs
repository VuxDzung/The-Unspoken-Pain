using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using VNCreator;
public class SourceManager : MonoBehaviour
{
    public string currentScene;
    public GameObject[] gameViews;
    private InteractiveObject[] items {
        get {
            GameObject[] itemObjs = GameObject.FindGameObjectsWithTag("Item");
            InteractiveObject[] items = new InteractiveObject[itemObjs.Length];
            for (int i=0; i<itemObjs.Length; i++) items[i] = itemObjs[i].GetComponent<InteractiveObject>();
            return items; 
        }
        set {}
    }
    private void Awake()
    {
        LoadProcess();
    }

    public void SaveProcess()
    {
        PlatformerData data = new PlatformerData();
        Dictionary<string, bool[]> ditionary = data.sceneItems;
        foreach (var note in ditionary)
            if (note.Key.Equals(currentScene))
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].interactable) note.Value[i] = true;
                }
                break;
            }
        SaveLoadSystem.Save(data);
    }
    public void LoadProcess()
    {
        PlatformerData data = SaveLoadSystem.LoadPlatformer();
        if (data != null)
        {
            Dictionary<string, bool[]> ditionary = data.sceneItems;
            foreach (var note in ditionary)
                if (note.Key.Equals(currentScene))
                {
                    if (note.Value.Length == 0) return;
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (note.Value[i]) items[i].interactable = true;
                    }
                    break;
                }
        } 
    }
}
