using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseBehaviour : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
