using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonPush : MonoBehaviour
{


    public void OnPush(InputAction.CallbackContext context)
    {
        Debug.Log("button pushed");
    }
}
