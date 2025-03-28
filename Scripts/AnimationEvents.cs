using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AnimationEvents
{
  
    public static event Action<bool> OnAnyEnemyGrapple;

    public static void TriggerGrapple(bool grappleMade)
    {
        OnAnyEnemyGrapple?.Invoke(grappleMade);
    }
}
