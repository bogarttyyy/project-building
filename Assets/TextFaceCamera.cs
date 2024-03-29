using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFaceCamera : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [Range(0, 360)]
    [SerializeField]
    private float x = 0;
    [Range(0, 360)]
    [SerializeField]
    private float y = 0;
    [Range(0, 360)]
    [SerializeField]
    private float z = 0;

    private void Update()
    {
        transform.forward = cam.transform.forward;
    }
}
