using UnityEngine;

public class PlayerHappyMovement : PlayerMovement
{
    public PlayerHappyMovement(Transform transform, PlayerSettings npcSettings) : base(transform, npcSettings)
    {
    }

    public override void UpdatePosition()
    {
        m_Transform.position
            += new Vector3(Input.GetAxis(k_InputAxisHorizontal), Input.GetAxis(k_InputAxisVertical), 0f)
            * m_PlayerSettings.movementSpeed
            * Time.deltaTime;
    }
}
