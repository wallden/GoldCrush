using System;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void Invoke(this MonoBehaviour monoBehaviour, Action action, float delayTime)
    {
        monoBehaviour.Invoke(action.Method.Name, delayTime);
    }
}