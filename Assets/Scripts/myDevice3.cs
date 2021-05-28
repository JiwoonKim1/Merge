using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.LowLevel;
using System.Runtime.InteropServices;
using UnityEditor;
using System;

//입력이 수신되고 저장되는 형태를 나타내는 C# 구조를 생성
[StructLayout(LayoutKind.Explicit, Size = 32)]
public struct myDeviceState3 : IInputStateTypeInfo
{
    // FourCC type codes are used to identify the memory layouts of state blocks. 
    public FourCC format => new FourCC('H', 'I', 'D');

    [FieldOffset(0)] public byte reportId;

    [InputControl(name = "button", layout = "Button", format = "BYTE")]
    //[InputControl(name = "mybutton", layout = "Key", format = "BIT")]
    [FieldOffset(0)] public int button;


}

[InputControlLayout(stateType = typeof(myDeviceState3))]
#if UNITY_EDITOR
[InitializeOnLoad] // Make sure static constructor is called during startup.
#endif
public class myDevice3 : InputDevice, IInputUpdateCallbackReceiver
{
    public ButtonControl button { get; private set; }


    //생성자
    static myDevice3()
    {
        InputSystem.RegisterLayout<myDevice2>(
            matches: new InputDeviceMatcher()
            .WithInterface("HID")
            .WithCapability("productId", 1970)
            .WithCapability("vendorId", 1118));
    }



    protected override void FinishSetup()
    {
        base.FinishSetup();
        button = GetChildControl<ButtonControl>("button");
        //button = GetChildControl<KeyControl>("mybutton");
    }


    public void OnUpdate()
    {

        //var state = new myDeviceState();

        /*
        InputSystem.onEvent += (eventPtr, device) =>
        {

            if (eventPtr.sizeInBytes == 28)
            {
                //bluetoothControl.GetComponent<BTtest>().changeText();
                Debug.Log(28);
                //Debug.Log(InputDevice.InvalidDeviceId);
                //Debug.Log(eventPtr.ge)
            }
            
        };
        */

        //InputState.onChange += InputState_onChange;

    }


    //=======================================================================

    public static myDevice3 current { get; private set; }

    public static IReadOnlyList<myDevice3> all => s_AllMyDevices;
    private static List<myDevice3> s_AllMyDevices = new List<myDevice3>();

    public override void MakeCurrent()
    {
        base.MakeCurrent();
        current = this;
    }

    protected override void OnAdded()
    {
        base.OnAdded();
        s_AllMyDevices.Add(this);
    }

    protected override void OnRemoved()
    {
        base.OnRemoved();
        s_AllMyDevices.Remove(this);
    }

    protected int getDeviceId()
    {
        return base.deviceId;
    }

    public int allmyDevices()
    {
        return s_AllMyDevices.Count;
    }
    //=======================================================================

    [RuntimeInitializeOnLoadMethod]
    static void Init() { }
}