using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text minText;
    public Text secText;
    public float limit = 1f;

    private float min = 0f;
    private float sec = 0f;

    // Update is called once per frame
    void Update()
    {
        if (min == limit) SceneManager.LoadScene("End");

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
}

