
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehaviour : MonoBehaviour
{
    [SerializeField] private Image crossHair;

    public enum Status { Default, Interactable, Interact}

    public void ChangeCrosshairStatus(Status status)
    {
        switch (status)
        {
            case Status.Default:
                crossHair.color = Color.white;
                break;
            case Status.Interact:
                crossHair.color = Color.blue;
                break;
            case Status.Interactable:
                crossHair.color = Color.green;
                break;
        }
    }

}
