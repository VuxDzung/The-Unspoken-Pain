using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    PlayerController player => FindObjectOfType<PlayerController>();
    Canvas[] gameCanvas => FindObjectsOfType<Canvas>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
