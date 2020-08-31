using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePitch : MonoBehaviour
{
    [NonSerialized] public float input;
    private Vector2 pitchLimits = new Vector2(-45f, 45f);

    private Transform myTransform;
    private Quaternion cameraRotation;
    private float pitchInput;

    [SerializeField] private float currentPitch = 0.0f;

    private void Awake()
    {
        myTransform = transform;
        cameraRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        pitchInput = Input.GetAxis("Mouse Y");
        currentPitch -= pitchInput;
        currentPitch = Mathf.Clamp(currentPitch, pitchLimits.x, pitchLimits.y);
        cameraRotation = Quaternion.Euler(currentPitch, 0f, 0f);
        myTransform.localRotation = cameraRotation;
    }
}
