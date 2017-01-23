using UnityEngine;

public class PlayerAngryMovement : PlayerMovement
{
    float m_TimePassed;
    float m_TargetTime;
    bool m_MovingIrregularly;
    bool m_CanMoveIrregularly = true;
    float m_DelayBetweenIrregularMovements = 3f;

    public PlayerAngryMovement(Transform transform, PlayerSettings npcSettings) : base(transform, npcSettings)
    {
    }

    public override void UpdatePosition()
    {
        var inputAxisVector = new Vector3(Input.GetAxis(k_InputAxisHorizontal), Input.GetAxis(k_InputAxisVertical), 0f);
        if (m_CanMoveIrregularly && !m_MovingIrregularly && RNG.RandomFloat() > 0.5f)
        {
            m_MovingIrregularly = true;
            m_TargetTime = 1f + RNG.RandomFloat() * 1f;
        }
        if (m_MovingIrregularly)
        {
            inputAxisVector.x = -inputAxisVector.x;
            m_TimePassed += Time.deltaTime;
            if (m_TimePassed >= m_TargetTime)
            {
                m_MovingIrregularly = false;
                m_CanMoveIrregularly = false;
                m_TimePassed = 0f;
            }
        }
        if (!m_CanMoveIrregularly)
        {
            m_TimePassed += Time.deltaTime;
            if (m_TimePassed >= m_DelayBetweenIrregularMovements)
            {
                m_CanMoveIrregularly = true;
                m_TimePassed = 0f;
            }
        }
        m_Transform.position
            += inputAxisVector
            * m_PlayerSettings.movementSpeed
            * Time.deltaTime;
    }
}
