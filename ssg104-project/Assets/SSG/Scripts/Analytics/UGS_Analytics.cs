using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;


public class UGS_Analytics : MonoBehaviour
{
    enum Events
    {
        StartGame,
        EasterEgg,
        End
    }
    [SerializeField] private string eventName;
    [SerializeField] private string eventParameter;
    [SerializeField] private bool loadOnStart = false;
    [SerializeField] private bool triggerCustomEvent;
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
            if (triggerCustomEvent) ConfigEvent();
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void ConfigEvent()
    {
        // Define Custom Parameters
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { eventParameter, "scene: " + GameManager.Instance.dialogueManager.CurrentScene}
        };

        // The ‘levelCompleted’ event will get cached locally
        //and sent during the next scheduled upload, within 1 minute
        AnalyticsService.Instance.CustomData(eventName, parameters);
        Debug.Log($"Sent: {eventName} at {GameManager.Instance.dialogueManager.CurrentScene}");

        // You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

}
