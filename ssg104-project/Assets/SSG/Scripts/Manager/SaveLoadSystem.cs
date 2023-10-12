using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveLoadSystem
{
    public static void SaveScene(string sceneName) { PlayerPrefs.SetString("Scene", sceneName); }
    public static string LoadScene(string sceneName) { return PlayerPrefs.GetString("Scene", sceneName); }

    public static void Save(string playerName) => PlayerPrefs.SetString("PlayerName", playerName);

    public static string LoadPlayerName() => PlayerPrefs.GetString("PlayerName");

    public static void Save(PlatformerData platformer)
    {
        string saveData = JsonConvert.SerializeObject(platformer);
        PlayerPrefs.SetString("Platformer", saveData);
    }
    public static PlatformerData LoadPlatformer()
    {
        return JsonConvert.DeserializeObject<PlatformerData>(PlayerPrefs.GetString("Platformer"));
    }

    public static void Save(StoryData data) {
        string storyData = JsonConvert.SerializeObject(data);
        PlayerPrefs.SetString("Story", storyData);
    }
    public static StoryData LoadStory() {
        return JsonConvert.DeserializeObject<StoryData>(PlayerPrefs.GetString("Story"));
    }
    public static void LoadingGame()
    {

    }
}

public class StoryData
{
    public int SoT;//current story on track
    public int BoT;//current branch on track
    public int DoT;//current dialogue on track
}


public class PlatformerData
{
    public Dictionary<string, bool[]> sceneItemModels = new Dictionary<string, bool[]>();
}


