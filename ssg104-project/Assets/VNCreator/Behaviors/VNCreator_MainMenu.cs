using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;
namespace VNCreator
{
    public class VNCreator_MainMenu : MonoBehaviour
    {
        [Header("Buttons")]
        public Button newGameBtn;
        public Button continueBtn;
        public Button optionsMenuBtn;
        public Button quitBtn;

        [Header("Scenes")]
        [Scene] public string playScene;
        [Scene] public string homeScene;
        [Tooltip("Chapter 1 Opening scene")]
        [Scene] public string scene1; //Opening -> VN
        [Tooltip("Chapter 2 Living room")]
        [Scene] public string scene2; //Living room 
        [Tooltip("Chapter 2 Bedroom")]
        [Scene] public string scene3; //Bedroom scene
        [Tooltip("Chapter 2 Classroom")]
        [Scene] public string scene4; //Classroom scene
        [Tooltip("Chapter 3 find the truth/option")]
        [Scene] public string scene5; //Find justice scene
        [Tooltip("Chapter 3 raise awareness")]
        [Scene] public string scene6;
        [Tooltip("Chapter 3 face-to-face")]
        [Scene] public string scene7;
        [Tooltip("Chapter 3 forgiveness")]
        [Scene] public string scene8;
        [Tooltip("Chapter 4 end scene")]
        [Scene] public string scene9; //end game -> visual novel

        void Start()
        {
            if(newGameBtn != null)
                newGameBtn.onClick.AddListener(NewGame);

            if(quitBtn != null)
                quitBtn.onClick.AddListener(Quit);
            if (continueBtn != null)
            {
                if (PlayerPrefs.HasKey("MainGame"))
                    continueBtn.onClick.AddListener(LoadGame);
                else
                    continueBtn.interactable = false;
            }
        }

        void NewGame()
        {
            GameSaveManager.NewLoad("MainGame");
            SceneManager.LoadScene(playScene, LoadSceneMode.Single);
        }

        void LoadGame()
        {
            string sceneName = GameSaveManager.LoadScene();
            if (sceneName == scene1 || sceneName == scene9) //if previous scene is visual novel style
            {
                GameSaveManager.currentLoadName = "MainGame";
                SceneManager.LoadScene(playScene, LoadSceneMode.Single);
            }
            else //if previous scene is platformer style
            {
                SceneManager.LoadScene(sceneName);
            }
        }

        void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}
