using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    [SerializeField] private BaseWeapon weapon;
    [SerializeField] private float speed = 3f;
    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("This has happened with " + other.tag);
        if (other.tag == "Player")
        {
            PlayerWeaponBehaviour.AddWeapon(weapon);
            Debug.Log("Gz sick weapon...");
            Destroy(gameObject);
        }
    }
}
