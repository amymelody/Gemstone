using UnityEngine;

public abstract class NPCBehaviour
{
    protected Transform m_Transform;
    protected NPCSettings m_NPCSettings;

    public abstract void InitializeState(Player player);
    public abstract void UpdatePosition();
    public abstract void ChangeMovementDirection();

    protected NPCBehaviour(Transform transform, NPCSettings npcSettings)
    {
        m_Transform = transform;
        m_NPCSettings = npcSettings;
    }
}
