using UnityEngine;

public abstract class PlayerMovement
{
    protected const string k_InputAxisHorizontal = "Horizontal";
    protected const string k_InputAxisVertical = "Vertical";

    protected Transform m_Transform;
    protected PlayerSettings m_PlayerSettings;

    public abstract void UpdatePosition();

    protected PlayerMovement(Transform transform, PlayerSettings npcSettings)
    {
        m_Transform = transform;
        m_PlayerSettings = npcSettings;
    }
}
