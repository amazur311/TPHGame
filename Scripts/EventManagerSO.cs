using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventState
{
    public string eventID;
    public bool isTriggered;
}

[CreateAssetMenu(fileName = "EventManagerSO", menuName = "Custom/EventManagerSO", order = 4)]
public class EventManagerSO : ScriptableObject
{
    public List<EventState> eventStates = new List<EventState>();

    public bool GetEventState(string eventID)
    {
        var state = eventStates.Find(e => e.eventID == eventID);
        return state != null && state.isTriggered;
        
    }

    public void SetEventState(string eventID, bool triggered)
    {
        var state = eventStates.Find(e => e.eventID == eventID);
        if (state != null)
        {
            state.isTriggered = triggered;
        }
        else
        {
            eventStates.Add(new EventState { eventID = eventID, isTriggered = triggered });
        }
    }

    public void LogAllEventIDs()
    {
        Debug.Log("EventManagerSO: All Stored Event IDs");
        foreach (var state in eventStates)
        {
            Debug.Log($"EventID: {state.eventID}, Triggered: {state.isTriggered}");
        }
    }
}