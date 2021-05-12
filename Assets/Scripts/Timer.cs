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
    public float startTime = 0f;
    public float period = 0.1f;

    private float min = 0f;
    private float sec = 0f;
    private bool flag = true;
    private int pressed = 0;

    private void Start()
    {
        InputSystem.onEvent += onEvent;
    }
 
    void Update()
    {
        pressed = 0;

        if (min == limit)
        {
            SceneManager.LoadScene("End");
            //GetComponent<writeCSV>().writeTimeLine();
        }

        setTime();
        GetComponent<writeCSV>().writeRow(min, sec, pressed);
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

        secText.text = Math.Round(sec, 3).ToString();
        //secText.text = sec.ToString();
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        var mydevice = device as myDevice;

        if (mydevice != null)
        {
            if (flag)
            {
                //GetComponent<writeCSV>().writeRow(min, sec, 1);
                pressed = 1;
                Debug.Log("Pressed Time=> " + minText.text + ":" + secText.text);
                flag = false;
            }
            else
            {
                flag = true;
                return;
            }
            
        }

    }
}

