using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    public Camera camera;
    public float fadeTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        camera.GetComponent<FadeCamera>().FadeIn(fadeTime);
    }

 
}
