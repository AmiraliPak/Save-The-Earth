using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class MyfloatEvent : UnityEvent<float>
{
}

public class MyStringEvent : UnityEvent<string>
{
}
public sealed class EventSystemCustom
{
    public MyfloatEvent OnIncreaseScore;
    public UnityEvent OnGameOver;
    public MyStringEvent OnMessage;

    private static readonly EventSystemCustom instance = new EventSystemCustom();
    
    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static EventSystemCustom()
    {
    }

    private EventSystemCustom()
    {
       OnIncreaseScore = new MyfloatEvent();
        OnGameOver = new UnityEvent ();
        OnMessage = new MyStringEvent();
    }

    public static EventSystemCustom Instance
    {
        get
        {
            return instance;
        }
    }
}