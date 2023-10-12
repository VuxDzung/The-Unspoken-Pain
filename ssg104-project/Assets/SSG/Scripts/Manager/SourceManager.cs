using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using VNCreator;
public class SourceManager : MonoBehaviour
{
    public string currentScene;
    public GameObject[] gameViews;
    private InteractiveModel[] itemModels => FindObjectsOfType<InteractiveModel>();
    private void Awake()
    {
        LoadProcess();
    }

    public void SaveProcess()
    {
        PlatformerData data = new PlatformerData();
        Dictionary<string, bool[]> ditionary = data.sceneItemModels;
        foreach (var note in ditionary)
            if (note.Key.Equals(currentScene))
            {
                for (int i = 0; i < itemModels.Length; i++)
                {
                    if (itemModels[i].interactable) note.Value[i] = true;
                }
                break;
            }
        SaveLoadSystem.Save(data);
    }
    public void LoadProcess()
    {
        PlatformerData data = SaveLoadSystem.LoadPlatformer();
        Dictionary<string, bool[]> ditionary = data.sceneItemModels;
        foreach (var note in ditionary)
            if (note.Key.Equals(currentScene))
            {
                if (note.Value.Length == 0) return;
                for (int i = 0; i < itemModels.Length; i++)
                {
                    if (note.Value[i]) itemModels[i].interactable = true;
                }
                break;
            } 
        
    }
}
