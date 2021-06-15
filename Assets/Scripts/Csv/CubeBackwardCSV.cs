using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class CubeBackwardCSV : MonoBehaviour
{
    public float limitSec = 60f; //초 단위 지정 
    public float intervalSec = 0.1f; //초 단위 지정, 1f=1초, 0.1f=100밀리초

    private List<TimeSpan> records;
    private List<String> row;

    public Stopwatch stopwatch;
    private TimeSpan timetaken;
    private CsvFileWriter writer;

    private bool flag = true;
    private bool flag2 = true;
    private bool flag3 = true;

    void Start()
    {
        setFilePath();

        stopwatch = new Stopwatch();
        stopwatch.Start();
        timetaken = stopwatch.Elapsed;

        records = new List<TimeSpan>();

        row = new List<string>();
        row.Add("SystemTime");
        row.Add("StartTime");
        row.Add("Count");
        writer.WriteRow(row);
        row.Clear();

        InputSystem.onEvent += onEvent;
    }

    private void Update()
    {
        timetaken = stopwatch.Elapsed;

        if (timetaken.TotalSeconds >= limitSec)
        {
            Debug.Log("stop");
            stopwatch.Stop();
            writeRecords();
            SceneManager.LoadScene("End");
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            records.Add(timetaken);

            Debug.Log("F1 Event--> " + timetaken.ToString());
        }
    }

    private void setFilePath()
    {
        var dataPath = Application.persistentDataPath + "/Data/";

        Debug.Log("Path_csv : " + dataPath);

        if (!Directory.Exists(dataPath))
            Directory.CreateDirectory(dataPath);

        if (!Directory.Exists(dataPath + "/Cube(backward)"))
            Directory.CreateDirectory(dataPath + "/Cube(backward)");

        writer = new CsvFileWriter(dataPath + "/Cube(backward)" + "/BlueTooth" + System.DateTime.Now.ToString("yyyyMMddHHmm") + ".csv");
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

            timetaken = stopwatch.Elapsed;
            records.Add(timetaken);

            Debug.Log("Event --> " + timetaken.ToString());
        }
        if (mydevice2 != null)
        {
            if (!flag2)
            {
                flag2 = true;
                return;
            }
            flag2 = false;

            timetaken = stopwatch.Elapsed;
            records.Add(timetaken);

            Debug.Log("Event --> " + timetaken.ToString());
        }
        if (mydevice3 != null)
        {
            if (!flag3)
            {
                flag3 = true;
                return;
            }
            flag3 = false;

            timetaken = stopwatch.Elapsed;
            records.Add(timetaken);

            Debug.Log("Event --> " + timetaken.ToString());
        }

    }

    private void writeRecordRow(TimeSpan time, int count)
    {
        string s = string.Format("{0:00}:{1:00}.{2:00}", time.Minutes, time.Seconds, time.Milliseconds);

        row.Add(System.DateTime.Now.ToString("HH:mm:ss.ff")); //시스템 시간 기록
        row.Add(s);
        row.Add(count.ToString());
        writer.WriteRow(row);
        row.Clear();
    }

    private void writeRecords()
    {
        int pointer = 0;
        int n = 0;
        int cnt = 0;

        TimeSpan before = TimeSpan.FromSeconds(intervalSec * n);
        TimeSpan after = TimeSpan.FromSeconds(intervalSec * (n + 1));

        Debug.Log("writeRecords " + stopwatch.Elapsed.ToString());

        while (before.TotalSeconds < limitSec)
        {
            cnt = 0;

            //사실은 그 다음 레코드 값도 체크해야함 -> for문
            while (pointer < records.Count && records[pointer] < after)
            {
                if (before <= records[pointer])
                {
                    cnt++;
                    pointer++;
                }
            }

            writeRecordRow(before, cnt);

            n++;
            before = TimeSpan.FromSeconds(intervalSec * n);
            after = TimeSpan.FromSeconds(intervalSec * (n + 1));
        }
    }
}
