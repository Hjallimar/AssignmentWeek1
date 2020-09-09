using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[SelectionBase]
[RequireComponent(typeof(CharacterController))]
public class PlayerControllerScript : MonoBehaviour
{

    public float rotationSpeed = 2f;
    public float momementSpeed = 3f;


    private CharacterController characterController;
    private Transform characterTransform;


    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        Assert.IsNotNull(characterController, "characterController != null");
        characterTransform = transform;




    }


    void Update()
    {
        transform.Rotate(0f, Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime, 0f);

        Vector3 forward = characterTransform.TransformDirection(Vector3.forward);

        float currentSpeed = momementSpeed * Input.GetAxis("Vertical");

        characterController.Move(forward.normalized * (currentSpeed * Time.deltaTime));
    }
}
