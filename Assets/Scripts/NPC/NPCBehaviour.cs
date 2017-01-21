using UnityEngine;

public abstract class NPCBehaviour
{
    protected Transform m_Transform;
    protected NPCSettings m_NPCSettings;

    public abstract void UpdatePosition();

    protected NPCBehaviour(Transform transform, NPCSettings npcSettings)
    {
        m_Transform = transform;
        m_NPCSettings = npcSettings;
    }
}
