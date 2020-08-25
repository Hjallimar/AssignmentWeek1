using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class MouseGun : MonoBehaviour
{
    [SerializeField] private float damage = 3;
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform endPoint;
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
        Debug.Log(hits.Length);
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

        BubbleSort(hitTargets, hitTargets.Count - 1);
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
      
        for (int i = 0; i < arrayOfNodes.Count() - 1; i++)
        {
            if (arrayOfNodes[i].IsMyDistanceHigher(arrayOfNodes[i + 1].MyDistance))
            {
                HitNode lowerValue = arrayOfNodes.ElementAt(i + 1);
                arrayOfNodes[i + 1] = arrayOfNodes.ElementAt(i);
                arrayOfNodes[i] = lowerValue;
            }
        }

        BubbleSort(arrayOfNodes, listSize - 1);
    }

    private void DealDamageToTargets()
    {
        float reduceDamage = 0;
        foreach(HitNode dmgObj in hitTargets)
        {
            if (reduceDamage <= damage)
            {
                dmgObj.MyTarget.OnDamageTaken(damage - reduceDamage);
                reduceDamage += damage / 5f;
            }
        }

        hitTargets.Clear();
        Debug.Log("hit target list count: " + hitTargets.Count);
    }

}

public class HitNode
{
    public float MyDistance { get; set; }
    public DamageableObject MyTarget { get; set; }

    public HitNode(float distance, DamageableObject hitObject)
    {
        MyDistance = distance;
        MyTarget = hitObject;
    }

    public bool IsMyDistanceHigher(float distance)
    {
        if (MyDistance > distance)
        {
            return true;
        }
        else 
        { 
            return false;
        }
    }
}
