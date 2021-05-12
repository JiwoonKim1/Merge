using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvokeTest : MonoBehaviour
{
    public float startTime = 2f;
    public float period = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start --> " + DateTime.Now.ToString("HH:mm:ss.ff"));
        //Invoke("LogInvoke", startTime);
        InvokeRepeating("LogInvokeRepeating", startTime, period);
    }


    private void InvokeRepeating(string v, float startTime)
    {
        throw new NotImplementedException();
    }

    private void LogInvoke()
    {
        Debug.Log("Invoke --> "+ DateTime.Now.ToString("HH:mm:ss.ff"));
    }

    private void LogInvokeRepeating()
    {
        Debug.Log("InvokeRepeating --> " + DateTime.Now.ToString("HH:mm:ss.ff"));
    }
}
