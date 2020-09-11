using UnityEngine;

/// <summary>
/// Base class for alll types of events or <see cref="EventInfo"/>. All events needs to use an EventInfo class that inherits from this class.
/// </summary>
public abstract class EventInfo
{
    public GameObject GO { get; private set; }
    public string EventDescription { get; private set; }

    public EventInfo(GameObject gO, string description = "")
    {
        GO = gO;
        EventDescription = description;
    }
}

/* How to Listen to the EventCoordinator and EventInfo
 * Awake()
 *      EventCoordinator.RegisterEventListener<EventInfo>(MethodName);
 *      
 * private void OverChargeReactorStart(EventInfo ei)
 *  {
 *      ReactorOverchargeBeginEventInfo robei = (ReactorOverchargeBeginEventInfo)ei;
 *  }
 * 
 * OnDestroy()/OnDisable()
 *      EventCoordinator.UnregisterEventListener<EventInfo>(MethodName);
 * 
 * How to activate/fire an event so others can use it.
 * EventInfo ei = new EventInfo(gameObject, "Event description");
 * EventCoordinator.ActivateEvent(ei);
 */


public class DefeatedEnemyEventInfo : EventInfo
{
    public DefeatedEnemyEventInfo(GameObject gO, string description) : base (gO, description)
    {
    }
}

public class SpawnProjectileEventInfo : EventInfo
{
    public TypeOfProjectile ProjectileType { get; private set; }

    public SpawnProjectileEventInfo(GameObject gO, string description, TypeOfProjectile projectileType): base(gO, description)
    {
        ProjectileType = projectileType;
    }
}

public class UpdateProjectileMovementEventInfo : EventInfo
{
    public UpdateProjectileMovementEventInfo(GameObject gO, string description) : base( gO, description)
    {

    }
}

