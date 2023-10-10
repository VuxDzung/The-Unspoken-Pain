using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VNCreator
{
    public class DisplayBase : MonoBehaviour
    {
        [SerializeField] private bool playOnAwake = true;
        public StoryObject story;

        protected NodeData currentNode;
        protected bool lastNode;
        public bool isSkipping { get; set; }

        protected List<string> _loadList = new List<string>();
        protected List<string> loadList = new List<string>();

        void Awake()
        {
            if (playOnAwake) TriggerDialog();
        }

        protected virtual void Update()
        {
            
        }

        public void TriggerDialog()
        {
            if (PlayerPrefs.GetString(GameSaveManager.currentLoadName) == string.Empty)
            {
                currentNode = story.GetFirstNode();
                loadList.Add(currentNode.guid);
                _loadList.Add(currentNode.guid);
            }
            else
            {
                loadList = GameSaveManager.Load();
                if (loadList == null || loadList.Count == 0)
                {
                    currentNode = story.GetFirstNode();
                    loadList = new List<string>();
                    loadList.Add(currentNode.guid);
                    _loadList.Add(currentNode.guid);
                }
                else
                {
                    currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
                }
            }
        }

        protected virtual void NextNode(int _choiceId)
        {
            if (!lastNode) 
            {
                currentNode = story.GetNextNode(currentNode.guid, _choiceId);
                lastNode = currentNode.endNode;
                loadList.Add(currentNode.guid);
            }
            else
            {
                Debug.Log("Last node reach");
            }
        }

        protected virtual void Previous()
        {
            loadList.RemoveAt(loadList.Count - 1);
            currentNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
            lastNode = currentNode.endNode;
        }

        public NodeData GetPreviousNode()
        {

            loadList.RemoveAt(loadList.Count - 1);
            NodeData previousNode = story.GetCurrentNode(loadList[loadList.Count - 1]);
            lastNode = previousNode.endNode;

            return previousNode;
        }

        protected void Save()
        {
            GameSaveManager.Save(loadList);
            Debug.Log("Saved");
        }
    }
}
