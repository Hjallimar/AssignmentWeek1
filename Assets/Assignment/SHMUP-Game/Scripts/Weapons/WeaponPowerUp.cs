using UnityEngine;

public class WeaponPowerUp : MonoBehaviour
{
    [SerializeField] private BaseWeapon weapon = null;
    [SerializeField] private float speed = 3f;

    private void Awake()
    {
        EventCoordinator.RegisterEventListener<ResetGameEventInfo>(GameOver);
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerWeaponBehaviour.AddWeapon(weapon);
            Spawner.PowerUpActivated(true);
            gameObject.SetActive(false);
        }
    }

    private void GameOver(EventInfo ei)
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        EventCoordinator.UnregisterEventListener<ResetGameEventInfo>(GameOver);
    }
}
