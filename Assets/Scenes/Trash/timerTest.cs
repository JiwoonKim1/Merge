using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class timerTest : MonoBehaviour
{
    public Stopwatch stopwatch;
    public float intervalSecond = 0.1f;

    private string[] s;
    private TimeSpan[] records;
    private int cnt = 0;

    private TimeSpan timetaken;

    // Start is called before the first frame update
    void Start()
    {
        stopwatch = new Stopwatch();
        stopwatch.Start();
        timetaken = stopwatch.Elapsed;

        s = new string[5];
        records = new TimeSpan[5];

        UnityEngine.Debug.Log("interval=" + intervalSecond);

        Invoke("logTimer", 1f); 
        Invoke("logTimer", 2f);
        Invoke("logTimer", 3.5f);

        Invoke("printTimer", 5f);
    }


    private void printTimer()
    {
        int pointer = 0;
        int n = 0;

        TimeSpan before = TimeSpan.FromSeconds(intervalSecond * n);
        TimeSpan after = TimeSpan.FromSeconds(intervalSecond * (n+1));

        while (before.TotalSeconds < 5f)
        {
            //사실은 그 다음값도 체크해야함
            if(before <= records[pointer] && records[pointer] < after)
            {
                UnityEngine.Debug.Log("before=" + before + ", record=" + records[pointer] + ", after=" + after);
                pointer++;
            }

            n++;
            before = TimeSpan.FromSeconds(intervalSecond * n);
            after = TimeSpan.FromSeconds(intervalSecond * (n + 1));
        }
    }

    private void logTimer()
    {
        timetaken = stopwatch.Elapsed;
        UnityEngine.Debug.Log(timetaken.ToString());

        s[cnt] = timetaken.ToString();
        records[cnt] = timetaken;
        cnt++;
    }
}
