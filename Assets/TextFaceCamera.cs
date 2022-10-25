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
        //transform.LookAt(cam.transform);
        //transform.Rotate(new Vector3(x, y, z));

        //Vector3 targetVector = cam.transform.position - transform.position;
        //float newY = Mathf.Atan2(targetVector.z, targetVector.x) * Mathf.Rad2Deg;

        //Mathf.

        //transform.rotation = Quaternion.Euler(0, -1 * newY, 0);
        transform.forward = cam.transform.forward;
    }
}
