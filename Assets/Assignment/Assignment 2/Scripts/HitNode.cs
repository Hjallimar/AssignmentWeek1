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
