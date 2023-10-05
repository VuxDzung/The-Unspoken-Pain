using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingleton<MonoBehaviour>
{
    PlayerController player => FindObjectOfType<PlayerController>();
    [SerializeField] private GameObject[] activeObject;
    [SerializeField] private Canvas[] gameCanvas;
    void Start()
    {
        
    }
    private void setObjInGame(int ord)
    {
        if (ord != 0) 
            foreach (var obj in activeObject)  obj.SetActive(false); 
        else
            foreach (var obj in activeObject)  obj.SetActive(true); 
    }
    // 0: default
    // array order + 1: canvas
    public void ChangeCanvas(int ord)
    {
        setObjInGame(ord);
        foreach (Canvas c in gameCanvas)
            if (gameCanvas[ord] == c) c.gameObject.SetActive(true);
            else if (gameCanvas[ord].enabled) c.gameObject.SetActive(false);
    }
    void Update()
    {
        
    }
}
