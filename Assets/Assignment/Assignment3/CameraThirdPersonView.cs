using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPersonView : MonoBehaviour
{
    [Header("Target and collidion")]
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private float cameraDistance = 0f;
    [SerializeField] private LayerMask cameraCollisionMask;
    
    [Header("Settings for the camera")]
    [SerializeField] private bool zoomEnable = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float zoomSensitivity = 10f;
    [SerializeField] private float maxDistanceFromPlayer = 10.0f;
    [SerializeField] private float minDistanceFromPlayer = 2.0f;

    [Header("Camera Clamp Scale")]
    [SerializeField] private float maxClampLimit = 45f;
    [SerializeField] private float minClampLimit = -30f;

    private Vector3 direction = Vector3.zero;

    void Awake()
    {
        direction = playerTransform.rotation.eulerAngles;
        transform.position = transform.forward * cameraDistance + playerTransform.position;
    }

    private void Start()
    {
        ToggleVisibleCursor(false);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ToggleVisibleCursor(false);
            direction.x -= mouseSensitivity * Input.GetAxisRaw("Mouse Y");
            direction.y += mouseSensitivity * Input.GetAxisRaw("Mouse X");
            direction.x = Mathf.Clamp(direction.x, minClampLimit, maxClampLimit);
            transform.rotation = Quaternion.Euler(direction.x, direction.y, 0f);
        }
        else
        {
            ToggleVisibleCursor(true);
        }

        CameraPosition();

        if (zoomEnable)
        {
            HandleZoom();
        }
    }

    void CameraPosition()
    {
        RaycastHit hit;
        Debug.DrawRay(playerTransform.position, -transform.forward, Color.red);
        if (Physics.SphereCast(playerTransform.position, 0.4f, -transform.forward, out hit, -cameraDistance, cameraCollisionMask))
        {
            transform.position = transform.forward * -hit.distance + playerTransform.position;
        }
        else
        {
            transform.position = transform.forward * cameraDistance + playerTransform.position;
        }
    }

    void ToggleVisibleCursor(bool status)
    {
        Cursor.visible = status;
        if (!status)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void HandleZoom()
    {
        bool changed = false;
        if(Input.mouseScrollDelta.y < 0)
        {
            cameraDistance -= zoomSensitivity * Time.deltaTime;
            changed = true;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            cameraDistance += zoomSensitivity * Time.deltaTime;
            changed = true;
        }

        if (changed)
        {
            cameraDistance = cameraDistance > -minDistanceFromPlayer ? -minDistanceFromPlayer : cameraDistance;
            cameraDistance = cameraDistance < -maxDistanceFromPlayer ? -maxDistanceFromPlayer : cameraDistance;
        }
    }
}
