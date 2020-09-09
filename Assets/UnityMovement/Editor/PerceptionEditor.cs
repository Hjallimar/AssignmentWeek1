using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

[CustomEditor(typeof(Perception))]
public class PerceptionEditor : Editor
{
    private ArcHandle arcHandle = new ArcHandle();

    private void OnEnable()
    {
        arcHandle.fillColor = new Color(0f, 1f, 0f, 0.25f);
        arcHandle.wireframeColor = arcHandle.fillColor;
        arcHandle.angleHandleColor = Color.red;
        arcHandle.radiusHandleColor = Color.red;
    }

    private void OnSceneGUI()
    {
        if (!target)
        {
            return;
        }

        Color oldColor = Handles.color;
        Perception perception = target as Perception;

        float halfFov = perception.fovAngle * 0.5f;
        float coneDirection = -90f;
        Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFov + coneDirection, Vector3.up);
        Quaternion rightRayRotation = Quaternion.AngleAxis(halfFov + coneDirection, Vector3.up);

        Transform scrTrans = perception.transform;
        Vector3 leftRayDirection = leftRayRotation * scrTrans.right * perception.perceprionRadius;
        Vector3 rightRayDirection = rightRayRotation * scrTrans.right * perception.perceprionRadius;


        arcHandle.angle = perception.fovAngle;
        arcHandle.radius = perception.perceprionRadius;

        Vector3 handleDirection = leftRayDirection;
        Vector3 handleNormal = Vector3.Cross(handleDirection, Vector3.forward);
        Matrix4x4 handleMatrix = Matrix4x4.TRS(scrTrans.position, Quaternion.LookRotation(handleDirection, handleNormal), Vector3.one);

        //Using statments not using directive as declared above the class.
        using (new Handles.DrawingScope(handleMatrix))
        {
            EditorGUI.BeginChangeCheck();
            arcHandle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(perception, "Changed perception properties");

                perception.fovAngle = Mathf.Clamp(arcHandle.angle, 0f, 359.9999f);
                perception.perceprionRadius = Mathf.Max(arcHandle.radius, 1f);

            }
        }

        Handles.color = new Color(1f, 0f, 0f, 0.25f);
        Handles.DrawSolidArc(scrTrans.position, Vector3.up, rightRayDirection, 360.0f - perception.fovAngle, perception.perceprionRadius);
        
        Handles.color = oldColor;
    }
}
