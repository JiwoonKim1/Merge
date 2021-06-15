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

    public KeyCode CubeForward;
    public KeyCode CubeBackward;
    public KeyCode StripeUpdown;
    public KeyCode StripeLeftright;
    public KeyCode StarForward;
    public KeyCode StarBackward;

    private int pushed = 0;
    private int connected = 0;
    private bool flag = true;
    private bool flag2 = true;
    private bool flag3 = true;


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
        if (Input.GetKeyDown(CubeForward)) SceneManager.LoadScene("CubeForward");
        else if (Input.GetKeyDown(CubeBackward)) SceneManager.LoadScene("CubeBackward");
        else if (Input.GetKeyDown(StripeUpdown)) SceneManager.LoadScene("StripeUpdown");
        else if (Input.GetKeyDown(StripeLeftright)) SceneManager.LoadScene("StripeLeftright");
        else if (Input.GetKeyDown(StarForward)) SceneManager.LoadScene("StarForward");
        else if (Input.GetKeyDown(StarBackward)) SceneManager.LoadScene("StarBackward");

    }

    private int countMyDevice()
    {
        int cnt = 0;
        var devices = InputSystem.devices.ToArray();

        for (int i = 0; i < devices.Length; i++)
        {
            var mydevice = devices[i] as myDevice;
            var mydevice2 = devices[i] as myDevice2;
            var mydevice3 = devices[i] as myDevice3;

            if (mydevice!=null || mydevice2!=null || mydevice3 != null) cnt++;
        }

        return cnt;
    }

    public void onInputDeviceChange(InputDevice device, InputDeviceChange change)
    {
        var mydevice = device as myDevice;
        var mydevice2 = device as myDevice2;
        var mydevice3 = device as myDevice3;

        {

            if (mydevice != null || mydevice2 !=null || mydevice3 != null)
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

            changePressedtext();
        }
        if (mydevice2 != null)
        {
            if (!flag2)
            {
                flag2 = true;
                return;
            }
            flag2 = false;

            changePressedtext();
        }
        if (mydevice3 != null)
        {
            if (!flag3)
            {
                flag3 = true;
                return;
            }
            flag3 = false;

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
