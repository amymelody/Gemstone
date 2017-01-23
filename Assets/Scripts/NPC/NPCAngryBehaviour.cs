using UnityEngine;

public class NPCAngryBehaviour : NPCBehaviour
{
    Player m_Player;

    public NPCAngryBehaviour(Transform transform, NPCSettings npcSettings) : base(transform, npcSettings)
    {
    }

    public override void InitializeState(Player player)
    {
        m_Player = player;
    }

    public override void UpdatePosition()
    {
        m_Transform.position += (m_Player.transform.position - m_Transform.position).normalized * m_NPCSettings.angryMovementSpeed * Time.deltaTime;
    }

    public override void ChangeMovementDirection()
    {
    }
}
