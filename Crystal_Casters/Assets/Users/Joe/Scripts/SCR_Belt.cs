using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;

public class SCR_Belt : MonoBehaviour
{
    private Transform belt, camera;
    private Vector3 offset;

    private void Awake()
    {
        belt = transform;
        camera = Camera.main.transform;
        offset = Vector3.down * 0.5f;
    }
    private void FixedUpdate()
    {
        belt.position = camera.position + offset;
        Quaternion rot = camera.rotation;
        rot.x = 0;
        rot.z = 0;
        belt.rotation = rot;
    }
}
