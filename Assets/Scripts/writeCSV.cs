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
using UnityEngine.SceneManagement;

public class writeCSV : MonoBehaviour
{
    public Stopwatch timer;
    public int timeLimit = 2;
    public float Hz = 60f;

    private CsvFileWriter writer;
    private List<string> timeLine;
    private List<Tuple<int, int>> records;

    private bool flag = true;
    private bool flag2 = true;
    private bool flag3 = true;

    private string projectName;
    private float updateTime = 1f;

    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        setFilePath();
        timeLine = new List<string>();

        InputSystem.onEvent += onEvent;

        Debug.Log("Start" + timer.Elapsed);
        records = new List<Tuple<int, int>>();

        projectName = SceneManager.GetActiveScene().name;
        updateTime = 1.0f / Hz;
    }

    private void Update()
    {
        if (timer.Elapsed.Minutes >= timeLimit)
        {
            writeRecord();
            SceneManager.LoadScene("End");
        }
    }

    private void setFilePath()
    {
        var dataPath = Application.persistentDataPath + "/Data/";

        Debug.Log("Path_csv : " + dataPath);

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        if (!Directory.Exists(dataPath + projectName))
            Directory.CreateDirectory(dataPath + projectName);

        writer = new CsvFileWriter(dataPath + projectName + "/BlueTooth" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".csv");
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        var mydevice = device as myDevice;
        var mydevice2 = device as myDevice2;
        var mydevice3 = device as myDevice3;

        if (mydevice != null)
        {
            if (!flag)
            {
                flag = true;
                return;
            }
            flag = false;

            Debug.Log("Event --> " + (String.Format("{0:00}:{1:00}.{2:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds)));

            Tuple<int, int> tuple = new Tuple<int, int>(timer.Elapsed.Minutes, timer.Elapsed.Seconds);
            records.Add(tuple);
        }
        if (mydevice2 != null)
        {
            if (!flag2)
            {
                flag2 = true;
                return;
            }
            flag2 = false;

            Debug.Log("Event --> " + (String.Format("{0:00}:{1:00}.{2:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds)));

            Tuple<int, int> tuple = new Tuple<int, int>(timer.Elapsed.Minutes, timer.Elapsed.Seconds);
            records.Add(tuple);
        }
        if (mydevice3 != null)
        {
            if (!flag3)
            {
                flag3 = true;
                return;
            }
            flag3 = false;

            Debug.Log("Event --> " + (String.Format("{0:00}:{1:00}.{2:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds)));

            Tuple<int, int> tuple = new Tuple<int, int>(timer.Elapsed.Minutes, timer.Elapsed.Seconds);
            records.Add(tuple);

            string s = timer.Elapsed.ToString();
        }

    }

    //리스트에 저장된 버튼 이벤트 시간들을  전부 csv로 기록, 기록하는 시간 간격=updateTime / 
    private void writeRecord()
    {
        int M = 0;
        int ptr = 0;

        while (M < timeLimit)
        {
            for (int S = 0; S < 60; S++)
            {
                int count = 0;

                for (; ptr < records.Count; ptr++)
                {
                    int s = records[ptr].Item2;

                    if (S != s)
                    {
                        //writeRecordRow(M, S, count);
                        break;
                    }
                    else
                    {
                        count++;
                    }
                }

                writeRecordRow(M, S, count);

            }

            M++;

        }

    }

    //리스트에 저장된 버튼 이벤트 시간들을  전부 csv로 기록
    private void writeRecord2()
    {
        int M = 0;
        int ptr = 0;

        while (M < timeLimit)
        {
            for (int S = 0; S < 60; S++)
            {
                int count = 0;

                for (; ptr < records.Count; ptr++)
                {
                    int s = records[ptr].Item2;

                    if (S != s)
                    {
                        //writeRecordRow(M, S, count);
                        break;
                    }
                    else
                    {
                        count++;
                    }
                }

                writeRecordRow(M, S, count);

            }

            M++;

        }

    }

    private string nextTime(string str)
    {
        string res = "";
        string[] arr = str.Split(':');
        int m = int.Parse(arr[0]);
        int s = int.Parse(arr[1]);
        int t = int.Parse(arr[2]);

        int tt = t;

        return res;
    }

    private void writeRecordRow(int m, int s, int cnt)
    {
        string str = (String.Format("{0:00}:{1:00}", m, s));
        timeLine.Add(str);
        timeLine.Add(cnt.ToString());
        writer.WriteRow(timeLine);
        timeLine.Clear();
    }

    private void writeRecordRow2(int m, int s, int f, int cnt)
    {
        string str = (String.Format("{0:00}:{1:00}.{2:00}", m, s, f));
        timeLine.Add(str);
        timeLine.Add(cnt.ToString());
        writer.WriteRow(timeLine);
        timeLine.Clear();
    }
}