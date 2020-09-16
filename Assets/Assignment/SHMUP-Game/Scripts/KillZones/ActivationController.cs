using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationController : MonoBehaviour
{
    [SerializeField] private bool activationStatus;
    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
        {
            enemy.SetActive(activationStatus);
        }
    }
}
