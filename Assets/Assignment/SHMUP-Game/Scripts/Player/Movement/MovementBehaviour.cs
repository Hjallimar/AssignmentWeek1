using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    private Transform myTransform = null;

    public float MoveSpeed { get; set; } = 10.0f;


    // Start is called before the first frame update
    void Awake()
    {
        myTransform = transform;
    }


    public void MovePlayer(Vector3 direction)
    {
        myTransform.position += direction * MoveSpeed * Time.deltaTime;
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
