using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Load
{
    view,
    scene
}

public class InteractiveObject : MonoBehaviour
{
    [SerializeField] private Load loadType;
    [SerializeField] protected InteractiveView view;
    [SerializeField] private string scene;
    [HideInInspector] public int viewOrder = 0;
    protected virtual void Start()
    {
        SetViewOrder();
    }

    private void SetViewOrder()
    {
        if (view == null) return;
        InteractiveView[] views = GameManager.Instance.getAllView();
        for (int i = 0; i < views.Length; i++)
        {
            if (view == views[i]) viewOrder = i;
        }
    }
    public void OnOpen()
    {
        switch (loadType)
        {
            case Load.view: 
                GameManager.Instance.ChangeCanvas(viewOrder); break;
            case Load.scene:
                SceneManager.LoadScene(scene); break;
        }
    }

    public void OnOpenDefaultView()
    {
        GameManager.Instance.ChangeCanvas(0);
    }

    
}
