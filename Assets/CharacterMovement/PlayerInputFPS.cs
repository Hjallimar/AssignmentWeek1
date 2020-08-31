using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputFPS : MonoBehaviour
{
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

        Interract();
    }


    private void Interract()
    {
        //Todo interact with stuff
    }
}
