using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action evtOnTimer;

    public static void Pause()
    {
        isPause = true;
    }

    public static void unPause()
    {
        isPause = false;
    }

    void Awake()
    {
        m_fWaitTime = 1.0F;
        StartCoroutine(OnTimer()); 
    }

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator OnTimer()
    {
        yield return new WaitForSeconds(m_fWaitTime);

        if(!isPause)
        {
            evtOnTimer();
        }

        StartCoroutine(OnTimer());
    }

    private static bool isPause = false;
    private float m_fWaitTime;
}
