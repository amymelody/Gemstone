using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "Settings/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [Header("Neutral")]
    [SerializeField]
    float m_NeutralMovementRadius;

    [Header("Happy")]
    [SerializeField]
    float m_HappyMovementSpeed;
    [SerializeField]
    float m_HappyMovementRadius;

    public float happyMovementSpeed { get { return m_HappyMovementSpeed; } }
    public float happyMovementRadius { get { return m_HappyMovementRadius; } }
}
