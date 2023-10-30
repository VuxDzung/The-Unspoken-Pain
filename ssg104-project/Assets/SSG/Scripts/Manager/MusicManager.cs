using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private string currentScene => GameManager.Instance.dialogueManager.CurrentScene;
    private void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("MusicPlayer");
        if (musicPlayers.Length > 1)
        {
            Destroy(this.gameObject);
        }
        if (currentScene == "Living room" || currentScene == "Bedroom")
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(this.gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (currentScene != "Living room" && currentScene != "Bedroom")
            Destroy(this.gameObject);
    }
}
