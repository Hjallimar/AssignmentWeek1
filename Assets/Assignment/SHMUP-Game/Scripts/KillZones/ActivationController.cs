using UnityEngine;

public class ActivationController : MonoBehaviour
{
    [SerializeField] private bool activationStatus;
    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
        {
            Debug.Log("Activating enemy: " + other.name);
            enemy.SetActive(activationStatus);
        }
    }
}
