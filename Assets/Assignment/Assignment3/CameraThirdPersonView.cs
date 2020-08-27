using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraThirdPersonView : MonoBehaviour
{
    [Header("Target and collidion")]
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private Vector3 cameraDistance = Vector3.zero;
    [SerializeField] private LayerMask cameraCollisionMask;
    
    [Header("Settings for the camera")]
    [SerializeField] private bool zoomEnable = true;
    [SerializeField] private bool visibleCursor = false;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float zoomSensitivity = 10;
    [SerializeField] private float maxDistanceFromPlayer = 10.0f;
    [SerializeField] private float minDistanceFromPlayer = 2.0f;

    private Vector3 direction = Vector3.zero;
    private SphereCollider myCollider;


    void Awake()
    {
        direction = playerTransform.rotation.eulerAngles;
        myCollider = GetComponent<SphereCollider>();
        //transform.LookAt(playerTransform);
        //transform.position = playerTransform.position;
        //transform.localPosition += new Vector3(0, 0, -5f);
    }

    private void Start()
    {
        ToggleVisibleCursor(visibleCursor);
    }

    void Update()
    {
        HandleZoom();
        if (Input.GetMouseButton(1))
        {
            ToggleVisibleCursor(false);
            direction.x -= mouseSensitivity * Input.GetAxisRaw("Mouse Y");
            direction.y += mouseSensitivity * Input.GetAxisRaw("Mouse X");
            direction.x = Mathf.Clamp(direction.x, -50, 50);
            transform.rotation = Quaternion.Euler(direction.x, direction.y, 0f);
        }
        else
        {
            ToggleVisibleCursor(true);
        }

        CameraPosition();
    }

    void CameraPosition()
    {
        RaycastHit hit;
        Vector3 point = transform.rotation * cameraDistance;
        Debug.DrawRay(transform.position, point.normalized, Color.red);

        if (Physics.SphereCast(transform.position, myCollider.radius, point.normalized, out hit, cameraDistance.magnitude, cameraCollisionMask))
        {
            transform.position = transform.rotation * (cameraDistance.normalized * hit.distance) + playerTransform.position;
        }
        else
        {
            transform.position = transform.rotation * cameraDistance + playerTransform.position;
        }
    }

    /// <summary>
    /// Toggles the mouse cursor's visible and locked state.
    /// </summary>
    /// <param name="status"></param>
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

    /// <summary>
    /// Handles the zoom for the camera.
    /// </summary>
    void HandleZoom()
    {
        bool changed = false;

        if(Input.mouseScrollDelta.y < 0)
        {
            cameraDistance.z -= zoomSensitivity * Time.deltaTime;
            changed = true;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            cameraDistance.z += zoomSensitivity * Time.deltaTime;
            changed = true;
        }

        if (changed)
        {
            cameraDistance.z = cameraDistance.z > -minDistanceFromPlayer ? -minDistanceFromPlayer : cameraDistance.z;
            cameraDistance.z = cameraDistance.z < -maxDistanceFromPlayer ? -maxDistanceFromPlayer : cameraDistance.z;
        }
    }
}
