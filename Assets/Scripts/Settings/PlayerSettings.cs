using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/PlayerSettings")]
public class PlayerSettings : ScriptableObject
{
    [SerializeField]
    float m_MovementSpeed;

    public float movementSpeed { get { return m_MovementSpeed; } }
}
