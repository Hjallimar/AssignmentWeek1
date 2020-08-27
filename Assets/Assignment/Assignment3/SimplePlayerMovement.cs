using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private LayerMask playerCollisionMask;
    [SerializeField] private CapsuleCollider myCollider;

    private Vector3 direction = Vector3.zero;


    // Update is called once per frame
    void Update()
    {

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        
        if(direction.magnitude > 0)
        {
            CheckMyCollision();
        }
    }

    void CheckMyCollision()
    {
        Vector3 point1 = transform.position;
        point1.y += myCollider.height / 2;
        Vector3 point2 = transform.position;
        point2.y -= myCollider.height / 2;
        if (!Physics.CapsuleCast(point1, point2, myCollider.radius, direction.normalized, movementSpeed * Time.deltaTime, playerCollisionMask))
        {
            MoveCharacter();
        }
    }

    void MoveCharacter()
    {
        transform.position += direction.normalized * Time.deltaTime;
    }
}
