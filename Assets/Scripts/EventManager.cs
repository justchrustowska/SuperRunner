using UnityEngine;
using System;

public class EventManager
{
    public static event Action OnPlayerDeath;
    public static event Action OnPlayerDash;

    public static void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
    }

    public static void PlayerDash()
    {
        OnPlayerDash?.Invoke();
    }
}
