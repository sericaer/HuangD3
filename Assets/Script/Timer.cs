using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action eventOnTimer;

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

        eventOnTimer();

        StartCoroutine(OnTimer());
    }

    float m_fWaitTime;
}
