using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer : MonoBehaviour
{
    public static event Action evtOnTimer;

    public static void Pause()
    {
        _pauseCount++;
    }

    public static void unPause()
    {
        _pauseCount--;
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

        if(_pauseCount == 0)
        {
            evtOnTimer();

            foreach(var elem in _list)
            {
                if(MainScene._gmData.date.Match(elem.Item1))
                {
                    elem.Item2();
                }
            }
        }

        StartCoroutine(OnTimer());
    }

    public static void Register(string date, Action act)
    {

    }

    private static int _pauseCount = 0;
    private float m_fWaitTime;

    private List<Tuple<string, Action>> _list = new List<Tuple<string, Action>>();
}
