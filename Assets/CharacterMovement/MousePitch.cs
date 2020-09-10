using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePitch : MonoBehaviour
{
    [NonSerialized] public float input;

    [SerializeField] private bool noBody = false;
    private Vector2 pitchLimits = new Vector2(-45f, 45f);

    private Quaternion cameraRotation = Quaternion.identity;
    private float pitchInput = 0f;
    private float TurnInput = 0f;

    [SerializeField] private float currentPitch = 0.0f;
    [SerializeField] private float currentYaw = 0f;

    private void Awake()
    {
        cameraRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        if (noBody)
        {
            TurnInput = Input.GetAxis("Mouse X");
            currentYaw = (transform.rotation * Quaternion.Euler(Vector3.up * TurnInput)).eulerAngles.y;
        }
        pitchInput = Input.GetAxis("Mouse Y");
        currentPitch -= pitchInput;
        currentPitch = Mathf.Clamp(currentPitch, pitchLimits.x, pitchLimits.y);
        cameraRotation = Quaternion.Euler(currentPitch, currentYaw, 0f);
        transform.localRotation = cameraRotation;
    }
}
