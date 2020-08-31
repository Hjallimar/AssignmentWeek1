using UnityEngine;

[CreateAssetMenu(fileName = "New Character Data", menuName = "FG/Character")]
public class CharacterData : ScriptableObject
{
    [Header("Moving")]
    public float walkSpeed = 6.75f;
    public float runSpeed = 10f;

    [Header("Crouching")]
    public float crouchSpeed = 4f;
    public float crouchHeight = 1;
    public float crouchRadius = 0.5f;

    [Header("Jumping")]
    public float jumpForce = 8f;
    public float gravityMultiplier = 1f;
    public float inAirMovementMultiplier = 1f;

}
