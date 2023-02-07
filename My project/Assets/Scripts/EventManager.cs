using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action EnemyDied;

    public static void OnEnemyDied()
    {
        EnemyDied?.Invoke();
    }
}
