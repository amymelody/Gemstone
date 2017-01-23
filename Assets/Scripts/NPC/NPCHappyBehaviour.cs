using UnityEngine;

public class NPCHappyBehaviour : NPCBehaviour
{
    Vector3 m_Origin;
    Vector3 m_MovementDirection;

    public NPCHappyBehaviour(Transform transform, NPCSettings npcSettings) : base(transform, npcSettings)
    {
    }

    public override void InitializeState(Player player)
    {
        m_Origin = m_Transform.position;
        RandomizeMovementDirection();
    }

    public override void UpdatePosition()
    {
        var newPosition = m_Transform.position + m_MovementDirection * m_NPCSettings.happyMovementSpeed * Time.deltaTime;
        if (Vector3.Distance(newPosition, m_Origin) > m_NPCSettings.happyMovementRadius)
        {
            RandomizeMovementDirection();
        }
        else
        {
            m_Transform.position = newPosition;
        }
    }

    public override void ChangeMovementDirection()
    {
        RandomizeMovementDirection();
    }

    void RandomizeMovementDirection()
    {
        var movementDirection = new Vector3(1f - RNG.RandomFloat() * 2f, 1f - RNG.RandomFloat() * 2f, 0f).normalized;
        if (movementDirection.x == 0 && movementDirection.y == 0)
        {
            movementDirection.x = 1f;
        }
        if (Vector3.Dot(movementDirection, m_MovementDirection) > 0f)
        {
            movementDirection *= -1f;
        }
        m_MovementDirection = movementDirection;
    }
}
