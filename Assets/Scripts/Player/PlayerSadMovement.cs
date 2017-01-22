using UnityEngine;

public class PlayerSadMovement : PlayerMovement
{
    public PlayerSadMovement(Transform transform, PlayerSettings npcSettings) : base(transform, npcSettings)
    {
    }

    public override void UpdatePosition()
    {
        m_Transform.position
            += new Vector3(Input.GetAxis(k_InputAxisHorizontal), Input.GetAxis(k_InputAxisVertical), 0f)
            * m_PlayerSettings.movementSpeed
            * m_PlayerSettings.sadMovementMultiplier
            * Time.deltaTime;
    }
}
