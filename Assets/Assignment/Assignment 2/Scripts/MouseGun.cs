using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MouseGun : MonoBehaviour
{
    [SerializeField] private float damage = 3;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform firePoint = default;

    List<HitNode> hitTargets = new List<HitNode>();
    private RaycastHit[] hits;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastScan();
        }
    }

    private void RaycastScan()
    {
        hits = Physics.RaycastAll(firePoint.position, firePoint.forward, Mathf.Infinity, hitLayers);
        SortMyTargets();
    }

    private void SortMyTargets()
    {
        DamageableObject hitTarget;

        foreach (RaycastHit hit in hits)
        {

            hitTarget = hit.collider.GetComponent<DamageableObject>();
            if (hitTarget != null)
            {
                hitTargets.Add(new HitNode(CalculateDistance(firePoint.position, hit.point), hitTarget));
            }
        }

        BubbleSort(hitTargets, hitTargets.Count);
        DealDamageToTargets();
    }

    private float CalculateDistance(Vector3 vectOne, Vector3 vectTwo)
    {
        return (vectOne - vectTwo).magnitude;
    }

    private void BubbleSort(List<HitNode> arrayOfNodes, int listSize)
    {
        if(listSize <= 1)
        {
            return;
        }

        HitNode lowerValue;

        for (int i = 0; i < arrayOfNodes.Count() - 1; i++)
        {
            if (arrayOfNodes[i].IsMyDistanceHigher(arrayOfNodes[i + 1].MyDistance))
            {
                lowerValue = arrayOfNodes.ElementAt(i + 1);
                arrayOfNodes[i + 1] = arrayOfNodes.ElementAt(i);
                arrayOfNodes[i] = lowerValue;
            }
        }

        BubbleSort(arrayOfNodes, listSize - 1);
    }

    private void DealDamageToTargets()
    {
        float reducedDamage = 0;

        foreach(HitNode dmgObj in hitTargets)
        {
            if (reducedDamage < damage)
            {
                float reduce = dmgObj.MyTarget.myInstance.Health;
                dmgObj.MyTarget.OnDamageTaken(damage - reducedDamage);
                reducedDamage += reduce;
            }
        }

        hitTargets.Clear();
    }
}
