using UnityEngine;

public class Player : MonoBehaviour
{
    const string k_InputAxisHorizontal = "Horizontal";
    const string k_InputAxisVertical = "Vertical";

    [SerializeField]
    PlayerSettings m_PlayerSettings;

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        transform.position
            += new Vector3(Input.GetAxis(k_InputAxisHorizontal), Input.GetAxis(k_InputAxisVertical), 0f) 
            * m_PlayerSettings.movementSpeed
            * Time.deltaTime;
    }
}
