using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text minText;
    public Text secText;
    public float limit = 1f;

    private float min = 0f;
    private float sec = 0f;
    private bool flag = true;

    private void Start()
    {
        InputSystem.onEvent += onEvent;
    }
 
    void Update()
    {
        if (min == limit)
        {
            SceneManager.LoadScene("End");
            GetComponent<writeCSV>().writeTimeLine();
        }

        setTime();
    }

    private void setTime()
    {
        sec += Time.deltaTime;

        if(sec-60 > 0)
        {
            sec = 0;
            min += 1;

            minText.text = min.ToString();
        }

        secText.text = Math.Floor(sec).ToString();
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

            GetComponent<writeCSV>().writeTime(min, sec);
            Debug.Log("Pressed Time=> " + minText.text + ":" + secText.text);
        }

    }
}

