using UnityEngine;
using UnityEditor;

public class PerceptionGismo
{
    [DrawGizmo(GizmoType.Selected | GizmoType.Active)]
    static void DrawGizmoForMyScript(Perception scr, GizmoType gizmoType)
    {
        Color oldColor = Gizmos.color;

        float halfFov = scr.fovAngle * 0.5f;
        float coneDirection = -90;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFov + coneDirection, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFov + coneDirection, Vector3.up);

        Transform scrTrans = scr.transform;
        Vector3 leftRayDirection = leftRayRotation * scrTrans.right * scr.perceprionRadius;
        Vector3 rightRayDirection = rightRayRotation * scrTrans.right * scr.perceprionRadius;

        Handles.color = new Color(0f, 1f, 0f, 0.25f);
        Handles.DrawSolidArc(scrTrans.position, Vector3.up, leftRayDirection, scr.fovAngle, scr.perceprionRadius);

        Handles.color = new Color(1f, 0f, 0f, 0.25f);
        Handles.DrawSolidArc(scrTrans.position, Vector3.up, rightRayDirection, 360 - scr.fovAngle, scr.perceprionRadius);



        Gizmos.color = oldColor;
    }
}
