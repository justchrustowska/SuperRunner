using UnityEngine;
using System;

public class EventManager
{
    public static event Action OnPlayerDeath;

    public static void PlayerDied()
    {
        OnPlayerDeath?.Invoke();
    }
}
