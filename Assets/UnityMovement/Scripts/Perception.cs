using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour
{
    public float perceprionRadius = 1f;
    [Range(0f, 360f)] public float fovAngle = 110f;
    public LayerMask perceptionLayerMask;
    public Material unseenMaterial;
    public Material seenMaterial;
    private Transform perceptionTransform;
    private List<Collider> previouslySeen = new List<Collider>();

    private void Awake()
    {
        perceptionTransform = transform;
    }

    private void Update()
    {
        foreach(Collider col in previouslySeen)
        {
            col.GetComponent<Renderer>().sharedMaterial = unseenMaterial;
        }
        previouslySeen.Clear();

        previouslySeen.AddRange(Physics.OverlapSphere(perceptionTransform.position, perceprionRadius, perceptionLayerMask, QueryTriggerInteraction.Ignore));

        foreach(Collider col in previouslySeen)
        {
            Vector3 direction = col.transform.position - perceptionTransform.position;
            float angle = Vector3.Angle(direction, perceptionTransform.forward);
            if (angle <= fovAngle * 0.5f)
            {
                col.GetComponent<Renderer>().sharedMaterial = seenMaterial;
            }
        }

    }
}
