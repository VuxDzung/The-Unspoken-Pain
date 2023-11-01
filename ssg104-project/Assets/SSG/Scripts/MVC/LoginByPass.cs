using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginByPass : MonoBehaviour
{
    private bool isFirstPlay = true;
    private void Start()
    {
        if (!SaveLoadSystem.IsFirstPlay())
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void NotFirstPlay()
    {
        SaveLoadSystem.SetFirstPlay();
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
