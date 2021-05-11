using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BTtest : MonoBehaviour
{
    [SerializeField] private Text pressedText;
    [SerializeField] private Text connectedText;

    private int pushed = 0;
    private int connected = 0;
    private bool flag = true;


    private void Start()
    {
        pressedText.text = pushed.ToString();

        connected = countMyDevice();
        connectedText.text = connected.ToString();

        InputSystem.onDeviceChange += onInputDeviceChange;
        InputSystem.onEvent += onEvent;
    }

    private void Update()
    {
        if (Input.anyKey) SceneManager.LoadScene("Plane");
    }

    private int countMyDevice()
    {
        int cnt = 0;
        var devices = InputSystem.devices.ToArray();

        for (int i = 0; i < devices.Length; i++)
        {
            var mydevice = devices[i] as myDevice;
            if (mydevice != null) cnt++;
        }

        return cnt;
    }

    public void onInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        var mydevice = device as myDevice;
        {

            if (mydevice != null)
                switch (change)
                {
                    case InputDeviceChange.Added:
                        connected += 1;
                        break;
                    case InputDeviceChange.Removed:
                        connected -= 1;
                        break;
                    case InputDeviceChange.ConfigurationChanged:
                        break;
                    case InputDeviceChange.Disconnected:
                        connected -= 1;
                        break;
                    case InputDeviceChange.Reconnected:
                        connected += 1;
                        break;
                }
            if (connected != null) changeConnectedText();
        }
    }

    public void onEvent(InputEventPtr inputEvent, InputDevice device)
    {
        var keyboard = device as Keyboard;
        var mydevice = device as myDevice;

        if (keyboard != null)
        {
            changePressedtext();
        }
        if (mydevice != null)
        {
            if (!flag)
            {
                flag = true;
                return;
            }
            flag = false;

            if (mydevice.IsPressed())
            {
            }
            changePressedtext();
        }

    }

    private void changePressedtext()
    {
        pushed += 1;
        if (pressedText != null) pressedText.text = pushed.ToString();
    }

    public void changeConnectedText()
    {
        if (connectedText != null) connectedText.text = connected.ToString();
    }

}
