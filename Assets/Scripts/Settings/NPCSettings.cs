using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "Settings/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [SerializeField]
    float m_BaseDelayBetweenWaves;

    [SerializeField]
    float m_WaveScaleRate;

    [SerializeField]
    float m_WaveMaxDeltaScale;

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
    public float waveScaleRate { get { return m_WaveScaleRate; } }
    public float waveMaxDeltaScale { get { return m_WaveMaxDeltaScale; } }
}
