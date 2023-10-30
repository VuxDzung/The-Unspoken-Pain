using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;


public class UGS_Analytics : GenericSingleton<UGS_Analytics>
{
    enum Events
    {
        StartGame,
        EasterEgg,
        End
    }
    [SerializeField] private bool loadOnStart = false;
    [SerializeField] private Events gameEvents;

    private void Start()
    {
        if (loadOnStart)
            SendEvents();
    }

    public async void SendEvents()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent(); //Get user consent according to various legislations
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}
