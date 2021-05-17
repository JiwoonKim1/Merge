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

public class BTcsv : MonoBehaviour
{
    public Stopwatch timer;
    public Text timerText;
    public int timeLimit = 2;

    private CsvFileWriter writer;
    private List<string> timeLine;
    private List<Tuple<int, int>> records;
    private bool flag = true;

    void Start()
    {
        timer = new Stopwatch();
        timer.Start();
        setFilePath();
        timeLine = new List<string>();

        InputSystem.onEvent += onEvent;

        Debug.Log("Start" + timer.Elapsed);
        records = new List<Tuple<int, int>>();
    }

    private void Update()
    {
        timerText.text = timer.Elapsed.ToString();

        if(timer.Elapsed.Minutes >= timeLimit)
        {
            writeRecord();
            SceneManager.LoadScene("End");
        }
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
                return;
            }
            flag = false;

            Debug.Log("Event1 --> " + (String.Format("{0:00}:{1:00}.{2:00}", timer.Elapsed.Minutes, timer.Elapsed.Seconds, timer.Elapsed.Milliseconds)));

            Tuple<int,int> tuple = new Tuple<int, int>(timer.Elapsed.Minutes, timer.Elapsed.Seconds);
            records.Add(tuple);
        }

    }

    //리스트에 저장된 버튼 이벤트 시간들을  전부 csv로 기록
    private void writeRecord()
    {
        int M = 0;
        int ptr = 0;
        while(M < timeLimit)
        {
            for(int S = 0; S < 60; S++)
            {
                int count = 0; 

                for(;ptr < records.Count; ptr++)
                {
                    int s = records[ptr].Item2;

                    if(S != s)
                    {
                        writeRecordRow(M, S, count);
                        break;
                    }
                    else
                    {
                        count++;
                    }
                }
            }

            M++;

        }
    }

    private void writeRecordRow(int m, int s, int cnt)
    {
        string str = (String.Format("{0:00}:{1:00}", m, s));
        timeLine.Add(str);
        timeLine.Add(cnt.ToString());
        writer.WriteRow(timeLine);
        timeLine.Clear();
    }
}
