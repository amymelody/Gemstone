using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField]
    float m_MovementSpeed;

    [SerializeField]
    KeyCode m_SendWaveKey;

    public float movementSpeed { get { return m_MovementSpeed; } }
    public KeyCode sendWaveKey { get { return m_SendWaveKey; } }
}
