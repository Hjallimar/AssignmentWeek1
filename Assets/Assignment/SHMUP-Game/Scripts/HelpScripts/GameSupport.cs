using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSupport
{
    private static RaycastHit hit;

    public static bool CheckSphereCast(SphereCollider collider, Vector3 direction, float distance, LayerMask layermask)
    {
        if (Physics.SphereCast(collider.transform.position, collider.radius, direction, out hit, distance, layermask))
        {
            return true;
        }
        return false;
    }
}
