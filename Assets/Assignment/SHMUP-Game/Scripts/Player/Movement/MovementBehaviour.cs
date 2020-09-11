using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask collisonLayers = default;
    private Transform myTransform = null;

    public float MoveSpeed { get; set; } = 10.0f;
    private SphereCollider coll;

    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
        coll = GetComponent<SphereCollider>();
    }


    public void MovePlayer(Vector3 direction)
    {
        float distance = MoveSpeed * Time.deltaTime;
        RaycastHit hit;
        if (!Physics.SphereCast(transform.position, coll.radius, direction, out hit, distance, collisonLayers))
        {
            myTransform.position += direction * distance;
        }
        
        myTransform.position = GameSupport.ApplyCorrectPossition(myTransform.position);
    }

    public void MoveTowards(Vector3 direction)
    {
        if (direction.magnitude <= 0.1)
            return;
        myTransform.position += direction.normalized * MoveSpeed * Time.deltaTime;
        myTransform.position = GameSupport.ApplyCorrectPossition(myTransform.position);
    }
}
