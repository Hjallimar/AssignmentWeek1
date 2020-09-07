using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputFPS : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayers;
    [SerializeField] private float interactableRayLenght = 50f;
    [SerializeField] private Camera myCamera;
    [SerializeField] private CrosshairBehaviour myCrosshair;
    //add a chrosshair that can change icons and what'not
    private CharacterMovement movement;


    private void Awake()
    {
        movement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        //Todo mouse input

        movement.TurnInput = Input.GetAxis("Mouse X") * GamePlaySettings.mouseSensitivity.x;
        movement.forwardInput = Input.GetAxis("Vertical");
        movement.sideWaysInput = Input.GetAxis("Horizontal");
        movement.runInput = Input.GetKey(KeyCode.LeftShift);
        movement.jumpInput = Input.GetButtonDown("Jump");
        movement.crouchInput = Input.GetButtonDown("Crouch");

        Ray screenRay = myCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(screenRay, interactableRayLenght, interactableLayers, QueryTriggerInteraction.Ignore))
        {
            myCrosshair.ChangeCrosshairStatus(CrosshairBehaviour.Status.Interactable);
        }
        else
        {
            myCrosshair.ChangeCrosshairStatus(CrosshairBehaviour.Status.Default);
        }

            if (Input.GetMouseButtonDown(0))
        {
            Interract();
        }
    }


    private void Interract()
    {
        Ray screenRay = myCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(screenRay, out hitInfo, interactableRayLenght, interactableLayers, QueryTriggerInteraction.Ignore))
        {
            I_Interactable interactable = hitInfo.collider.GetComponent<I_Interactable>();
            if (interactable != null)
            {
                myCrosshair.ChangeCrosshairStatus(CrosshairBehaviour.Status.Interact);
                interactable.Interact(transform);
            }
            else
            {
                myCrosshair.ChangeCrosshairStatus(CrosshairBehaviour.Status.Default);
                //set the crosshair to default
            }
        }
    }
}
