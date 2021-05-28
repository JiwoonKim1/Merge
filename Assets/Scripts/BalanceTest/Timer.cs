using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    public Stopwatch timer;

    private void Start()
    {
        timer = new Stopwatch();
        timer.Start();
    }
    private void Update()
    {
        timerText.text = timer.Elapsed.ToString();
    }
}

