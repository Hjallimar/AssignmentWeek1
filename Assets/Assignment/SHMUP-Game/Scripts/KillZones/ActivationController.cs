using UnityEngine;

public class ActivationController : MonoBehaviour
{
    [SerializeField] private bool activationStatus = false;
    private void OnTriggerEnter(Collider other)
    {
        IEnemy enemy = other.GetComponent<IEnemy>();
        if (enemy != null)
        {
            Debug.Log("Activating enemy: " + other.name);
            enemy.SetActive(activationStatus);
        }
        else  if (other.GetComponent<WeaponPowerUp>() != null)
        {
            other.gameObject.SetActive(false);
        }
    }
}
