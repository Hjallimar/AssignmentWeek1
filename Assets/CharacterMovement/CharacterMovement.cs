using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    [NonSerialized] public float forwardInput = 0f;
    [NonSerialized] public float sideWaysInput = 0f;
    [NonSerialized] public float TurnInput = 0f;
    [NonSerialized] public bool runInput = default;
    [NonSerialized] public bool jumpInput = default;
    [NonSerialized] public bool crouchInput = default;

    [SerializeField] private float maxDistance = 0f;
    [SerializeField] private float skinWidth = 0.01f;
    [SerializeField] private CharacterData characterData = default;

    private Vector2 originalCapsuleSize = Vector2.zero;

    private Vector3 moveDirection = Vector3.zero;
    private float currentSpeed = 0f;
    private float adjustVerticalVelocity = 0f;
    private float inputAmmount = 0f;

    private Transform myTransform = default;
    private Rigidbody myRigidbody = default;
    private CapsuleCollider myCollider = default;

    public bool IsCrouching { get; private set; }
    public bool IsRunning { get; private set; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        myTransform = transform;
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<CapsuleCollider>();
        originalCapsuleSize.Set(myCollider.radius, myCollider.height);
    }

    private void LateUpdate()
    {
        //Rotate
        myRigidbody.MoveRotation(myRigidbody.rotation * Quaternion.Euler(Vector3.up * TurnInput));

        //Move
        moveDirection = (sideWaysInput * transform.right + forwardInput * transform.forward).normalized;
        inputAmmount = Mathf.Clamp01(Mathf.Abs(forwardInput) + Mathf.Abs(sideWaysInput));

        adjustVerticalVelocity = myRigidbody.velocity.y;

        if (CheckGrounded())
        {
            if (jumpInput)
            {
                adjustVerticalVelocity = characterData.jumpForce;
            }
            else if(crouchInput || runInput)
            {
                if(IsCrouching || runInput)
                {
                    ExitCrouch();
                    currentSpeed = characterData.runSpeed;
                }
                else
                {
                    if (!runInput)
                    {
                        EnterCrouch();
                    }
                }

            }


            if (IsCrouching)
            {
                currentSpeed = characterData.crouchSpeed;
            }
            else
            {
                currentSpeed = IsRunning ? characterData.runSpeed : characterData.walkSpeed;
            }
        }
        else
        {
            currentSpeed *= characterData.inAirMovementMultiplier;
        }

        SetVelocity();
    }

    private bool CheckGrounded()
    {
        maxDistance = myCollider.height / 2 + skinWidth;
        return Physics.Raycast(myTransform.position + myCollider.center, Vector3.down, maxDistance);
    }


    private void SetVelocity()
    {
        Vector3 velocity = moveDirection * (currentSpeed * inputAmmount);
        velocity.y = adjustVerticalVelocity * characterData.gravityMultiplier;
        myRigidbody.velocity = velocity;
    }

    private void ExitCrouch()
    {
        myCollider.height = originalCapsuleSize.y;
        myCollider.radius = originalCapsuleSize.x;
    }

    private void EnterCrouch()
    {
        myCollider.height = characterData.crouchHeight;
        myCollider.radius = characterData.crouchRadius;
    }
}
