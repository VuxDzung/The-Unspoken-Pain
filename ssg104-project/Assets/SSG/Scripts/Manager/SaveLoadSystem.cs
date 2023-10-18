using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveLoadSystem
{
    private static string savePlaformerData = "";

    public static void SavePlatformer(PlatformerData platformer)
    {
        savePlaformerData = JsonConvert.SerializeObject(platformer);
        PlayerPrefs.SetString("Platformer", savePlaformerData);
        Debug.Log($"Objects can interacted: {savePlaformerData}");
    }
    public static PlatformerData LoadPlatformer()
    {
        Debug.Log($"Objects can interacted: {PlayerPrefs.GetString("Platformer")}");
        return JsonConvert.DeserializeObject<PlatformerData>(PlayerPrefs.GetString("Platformer"));
    }


    #region SAVE_NON_VOLATILE
    public static void SaveScene(string sceneName) { PlayerPrefs.SetString("Scene", sceneName); }
    public static string LoadScene() { return PlayerPrefs.GetString("Scene"); }

    public static void Save(string playerName) => PlayerPrefs.SetString("PlayerName", playerName);

    public static string LoadPlayerName() => PlayerPrefs.GetString("PlayerName");

    public static void Save(StoryData data)
    {
        string storyData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString("Story", storyData);
    }

    public static StoryData LoadStory() {
        return JsonConvert.DeserializeObject<StoryData>(PlayerPrefs.GetString("Story"));
    }

    public static void SaveSettings(SettingsData settings)
    {
        PlayerPrefs.SetString("Settings", JsonConvert.SerializeObject(settings));
    }

    public static SettingsData LoadSettings()
    {
        return JsonConvert.DeserializeObject<SettingsData>(PlayerPrefs.GetString("Settings"));
    }
    #endregion
}

public class StoryData
{
    public int SoT;//current story on track
    public int BoT;//current branch on track
    public int DoT;//current dialogue on track
}


public class PlatformerData
{
    public List<string> itemCanInteract = new List<string>();
    public List<string> itemsInventory = new List<string>();
}


