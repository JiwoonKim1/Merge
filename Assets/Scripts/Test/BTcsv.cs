using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BTcsv : MonoBehaviour
{
    public Stopwatch timer;
    public Text timerText;
    public string elapsedTime;

    private CsvFileWriter writer;
    private List<string> timeLine;
    private bool flag = true;
    private int pressed = 0;
    private List<Tuple<int, int>> records;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        setFilePath();
        timeLine = new List<string>();

        InputSystem.onEvent += onEvent;

        Debug.Log("Start" + timer.Elapsed);
        records = new List<Tuple<int, int>>();
        //InvokeRepeating("countPressedButton", 1f, 1f);
        //InvokeRepeating("setElapsed", 0f, 1f);
        //InvokeRepeating("countPressedButton", 1f, 1f);
    }

    private void Update()
    {
        timerText.text = timer.Elapsed.ToString();
        /*
        InputSystem.onEvent += (eventPtr, device) =>
        {
            var mydevice = device as myDevice;
            if (mydevice != null) Debug.Log("Update" + timer.Elapsed);
        };
        */

    }

    private void setFilePath()
    {
        var dataPath = Application.persistentDataPath + "/Data/";

        //Debug.Log("Path_csv : " + dataPath);

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);
        if (!Directory.Exists(dataPath + "BTcsv"))
            Directory.CreateDirectory(dataPath + "BTcsv");

        writer = new CsvFileWriter(dataPath + "BTcsv/Result" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".csv");
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        var mydevice = device as myDevice;

        if (mydevice != null)
        {
            if (!flag)
            {
                flag = true;
                //Debug.Log("Event2" + timer.Elapsed);
                return;
            }
            flag = false;
            pressed += 1;

            Debug.Log("Event1 --> " + (String.Format("{0:00}:{1:00}.{2:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds)));

            Tuple<int,int> tuple = new Tuple<int, int>(timer.Elapsed.Minutes, timer.Elapsed.Seconds);
            records.Add(tuple);
        }

    }

    private void setElapsed()
    {
        Debug.Log("write csv1" + timer.Elapsed);
        elapsedTime = (String.Format("{0:00}:{1:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds));
        timeLine.Add(elapsedTime);
    }
    private void countPressedButton()
    {
        //string elapsedTime = (String.Format("{0:00}:{1:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds));
        //timeLine.Add(elapsedTime);
        Debug.Log("write csv2" + timer.Elapsed);
        timeLine.Add(pressed.ToString());
        writer.WriteRow(timeLine);
        timeLine.Clear();
        pressed = 0;
    }

    private void OnApplicationQuit()
    {
        writeRecord();
    }

    private void writeRecord()
    {
        int M = 0;
        int S = 0;
        int count = 0;

        for (int i = 0; i < records.Count; i++)
        {
            int mm = records[i].Item1;
            int ss = records[i].Item2;

            if (M != mm || S != ss)
            {
                writeRecrodRow(M, S, count);
                M = mm;
                S = ss;
                count = 0;
            }

            count += 1;

        }
        writeRecrodRow(M, S, count);
    }

    private void writeRecrodRow(int m, int s, int cnt)
    {
        string str = (String.Format("{0:00}:{1:00}", m, s));
        timeLine.Add(str);
        timeLine.Add(cnt.ToString());
        writer.WriteRow(timeLine);
        timeLine.Clear();
    }
}
