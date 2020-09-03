using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour
{
    [NonSerialized] public float forwardInput;
    [NonSerialized] public float sideWaysInput;
    [NonSerialized] public float TurnInput;
    [NonSerialized] public bool runInput;
    [NonSerialized] public bool jumpInput;
    [NonSerialized] public bool crouchInput;

    [SerializeField] private float maxDistance;
    [SerializeField] private float skinWidth = 0.01f;
    [SerializeField] private CharacterData characterData;

    private Vector2 originalCapsuleSize;

    private Vector3 moveDirection;
    private float currentSpeed;
    private float adjustVerticalVelocity;
    private float inputAmmount;

    private Transform myTransform;
    private Rigidbody myRigidbody;
    private CapsuleCollider myCollider;

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
