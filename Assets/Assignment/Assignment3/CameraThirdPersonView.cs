using System.Collections;
using UnityEngine;

public class CameraThirdPersonView : MonoBehaviour
{
    [Header("Target and collidion")]
    [SerializeField] private Transform playerTransform = default;
    [SerializeField] private LayerMask cameraCollisionMask = default;
    
    [Header("Settings for the camera")]
    [SerializeField] private bool zoomEnable = true;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float mouseSensitivity = 2f;
    [Range(1.0f, 10.0f)]
    [SerializeField] private float zoomSensitivity = 10f;
    [Range(8.0f, 16.0f)]
    [SerializeField] private float maxDistanceFromPlayer = 10.0f;
    [Range(0.5f, 2.0f)]
    [SerializeField] private float minDistanceFromPlayer = 2.0f;

    [Header("Camera Clamp Scale")]
    [Range(30.0f, 45.0f)]
    [SerializeField] private float maxClampLimit = 45f;
    [Range(-30.0f, -45.0f)]
    [SerializeField] private float minClampLimit = -30f;

    RaycastHit hit;
    private Vector3 direction = Vector3.zero;
    private float cameraDistance = -10f;
    private Coroutine cameraRotation;

    void Awake()
    {
        if (cameraDistance > maxDistanceFromPlayer)
        {
            cameraDistance = maxDistanceFromPlayer;
        }
        direction = playerTransform.rotation.eulerAngles;
        transform.position = transform.forward * cameraDistance + playerTransform.position;
    }

    private void Start()
    {
        ToggleVisibleCursor(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            cameraRotation = StartCoroutine(RotateMyCamera());
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            StopCoroutine(cameraRotation);
            ToggleVisibleCursor(true);
        }

        CameraPosition();

        if (zoomEnable)
        {
            HandleZoom();
        }
    }

    private IEnumerator RotateMyCamera()
    {
        ToggleVisibleCursor(false);
        while (true)
        {
            direction.x -= mouseSensitivity * Input.GetAxisRaw("Mouse Y");
            direction.y += mouseSensitivity * Input.GetAxisRaw("Mouse X");
            direction.x = Mathf.Clamp(direction.x, minClampLimit, maxClampLimit);
            transform.rotation = Quaternion.Euler(direction.x, direction.y, 0f);
            yield return new WaitForFixedUpdate();
        }
    }

    void CameraPosition()
    {
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
            cameraDistance -= zoomSensitivity;
            changed = true;
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            cameraDistance += zoomSensitivity;
            changed = true;
        }

        if (changed)
        {
            cameraDistance = cameraDistance > -minDistanceFromPlayer ? -minDistanceFromPlayer : cameraDistance;
            cameraDistance = cameraDistance < -maxDistanceFromPlayer ? -maxDistanceFromPlayer : cameraDistance;
        }
    }
}
