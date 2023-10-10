using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace VNCreator
{
    public static class GameSaveManager
    {
        public static string currentLoadName = string.Empty;

        public static List<string> Load(string loadName)
        {
            if (loadName == string.Empty)
            {
                currentLoadName = loadName;
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            string _loadString = PlayerPrefs.GetString(currentLoadName);
            List<string> _loadList = _loadString.Split('_').ToList();
            _loadList.RemoveAt(_loadList.Count - 1);
            currentLoadName = loadName;
            return _loadList;
        }

        public static List<string> Load()
        {
            if (currentLoadName == string.Empty)
            {
                return null;
            }

            if (!PlayerPrefs.HasKey(currentLoadName))
            {
                Debug.LogError("You have not saved anything with the name " + currentLoadName);
                return null;
            }

            string _loadString = PlayerPrefs.GetString(currentLoadName);
            List<string> _loadList = _loadString.Split('_').ToList();
            return _loadList;
        }

        public static void Save(List<string> storyPath)
        {
            string _save = string.Join("_", storyPath.ToArray());
            PlayerPrefs.SetString(currentLoadName, _save);
        }

        public static void SaveScene(string scene)
        {
            PlayerPrefs.SetString("Scene", scene);
        }

        public static void SavePlatformer(UserData data)
        {
            string platformData = JsonConvert.SerializeObject(data);
            PlayerPrefs.SetString("User", platformData);
        }

        public static string LoadScene() => PlayerPrefs.GetString("Scene");

        public static UserData LoadPlatformer()
        {
            UserData loadedData = JsonConvert.DeserializeObject<UserData>(GetCurrentPlatformerData());
            return loadedData;
        }

        public static string GetCurrentPlatformerData()
        {
            return PlayerPrefs.GetString("User");
        }

        public static void NewLoad(string saveName)
        {
            currentLoadName = saveName;
            PlayerPrefs.SetString(saveName, string.Empty);
        }
    }

    public class UserData
    {
        public Dictionary<string, bool> mainSubject = new Dictionary<string, bool>();
        public Inventory inventory { get; set; }
    }

    [System.Serializable]
    public class Inventory
    {
        public GameObject[] items;
    }
}
