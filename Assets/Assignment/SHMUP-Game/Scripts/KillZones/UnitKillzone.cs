using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class UnitKillzone : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<EnemyBaseBehaviour>().ReturnToObjectPool();
    }
}
