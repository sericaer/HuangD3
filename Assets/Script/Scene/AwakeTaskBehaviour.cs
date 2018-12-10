using System;
using System.Collections;
using UnityEngine;


public abstract class AwakeTaskBehaviour<T> : MonoBehaviour where T: AwakeTaskBehaviour<T>
{
    public static T Inst { get; protected set; }
 
    public static void Task(Action action)
    {
        if (Inst != null)
        {
            action();
        }
        else
        {
            aWakeTask += action;
        }
    }



    void OnEnable()
    {
        if (Inst != null && Inst != this)
        {
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            Inst = (T)this;
        }

        if (aWakeTask != null)
        {
            aWakeTask();
            aWakeTask = null;
        }
    }

    private static event Action aWakeTask;
}