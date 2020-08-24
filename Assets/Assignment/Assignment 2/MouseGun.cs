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

    private List<DamageableObject> targetsHit = new List<DamageableObject>();
    
    private Transform myCamera = default;

    private RaycastHit[] hits;

    private void Start()
    {
        myCamera = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastScan();
        }
    }

    private void RaycastScan()
    {
        hits = Physics.RaycastAll(myCamera.position, myCamera.forward, Mathf.Infinity, hitLayers);
        SortMyTargets();
    }

    private void SortMyTargets()
    {
        SortNode[] newHits = new SortNode[hits.Length];
        int index = 0;
        foreach(RaycastHit hit in hits)
        {

            DamageableObject hitTarget = hit.collider.GetComponent<DamageableObject>();
            if (hitTarget != null)
            {
                newHits[index] = new SortNode(CalculateDistance(myCamera.transform.position, hit.point), hitTarget);
                index++;
            }
        }

        if (index < hits.Length - 1)
        {
            SortNode[] temporaryArray = new SortNode[index + 1];
            newHits = temporaryArray;
        }

        BubbleSort(newHits);
        DealDamageToTargets();
    }


    private float CalculateDistance(Vector3 vectOne, Vector3 vectTwo)
    {
        return (vectOne - vectTwo).magnitude;
    }

    private void BubbleSort(SortNode[] arrayOfNodes)
    {
        bool sorting = false;
        do
        {
            sorting = false;
            for (int i = 0; i < arrayOfNodes.Count() - 1; i++)
            {
                if (arrayOfNodes[i].Distance > arrayOfNodes[i + 1].Distance)
                {
                    SortNode lowerValue = arrayOfNodes[i + 1];
                    arrayOfNodes[i + 1] = arrayOfNodes[i];
                    arrayOfNodes[i] = lowerValue;
                    sorting = true;
                }
            }
        } while (sorting);

        foreach(SortNode node in arrayOfNodes)
        {
            targetsHit.Add(node.HitObject);
        }
    }

    private void DealDamageToTargets()
    {
        float reduceDamage = 0;
        foreach(DamageableObject dmgObj in targetsHit)
        {
            if (reduceDamage >= damage)
                reduceDamage = damage;
            dmgObj.OnDamageTaken(damage - reduceDamage);
            reduceDamage += damage/3f;
        }

        targetsHit.Clear();
    }

}

public class SortNode
{
    public float Distance { get; set; }
    public DamageableObject HitObject { get; set; }

    public SortNode(float distance, DamageableObject hitObject)
    {
        Distance = distance;
        HitObject = hitObject;
    }
}
