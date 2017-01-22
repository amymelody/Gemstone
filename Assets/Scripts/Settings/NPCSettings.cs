using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "Settings/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [SerializeField]
    float m_BaseDelayBetweenWaves;

    [Header("Neutral")]
    [SerializeField]
    float m_NeutralMovementRadius;

    [Header("Happy")]
    [SerializeField]
    float m_HappyMovementSpeed;
    [SerializeField]
    float m_HappyMovementRadius;

    public float baseDelayBetweenWaves { get { return m_BaseDelayBetweenWaves; } }
    public float happyMovementSpeed { get { return m_HappyMovementSpeed; } }
    public float happyMovementRadius { get { return m_HappyMovementRadius; } }
}
