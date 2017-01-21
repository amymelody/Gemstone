using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "Settings/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [Header("General")]
    [SerializeField]
    float m_MovementSpeed;

    [Header("Neutral")]
    [SerializeField]
    float m_NeutralMovementRadius;

    public float movementSpeed { get { return m_MovementSpeed; } }
}
