using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveY : MonoBehaviour
{
    public Material mat;
    public float startTime = 0;
    public float period = 1f;
    public float dist = 2f;
    public float repeat = 100f;

    private float posY = 0;
    private float _period;
    private float _dist;

    private void Start()
    {
        posY = 0;
        _period = period / repeat;
        _dist = dist / repeat;
        InvokeRepeating("PosIncrement", startTime, _period);
    }

    private void PosIncrement()
    {
        posY += _dist;
        mat.SetFloat("_PosY", posY);

        //Debug.Log("posY=" + posY + "totalTime" + totalTime + DateTime.Now.ToString(("--> mm:ss.ff")));

        //if (totalTime >= 1f) CancelInvoke();
    }

}
