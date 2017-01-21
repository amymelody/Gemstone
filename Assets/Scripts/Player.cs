using UnityEngine;

public class Player : MonoBehaviour
{
    const string k_InputAxisHorizontal = "Horizontal";
    const string k_InputAxisVertical = "Vertical";

    [SerializeField]
    PlayerSettings m_PlayerSettings;

    [SerializeField]
    EmotionWave m_WavePrefab;

    void Update()
    {
        UpdateMovement();
        CheckForActionInput();
    }

    void UpdateMovement()
    {
        transform.position
            += new Vector3(Input.GetAxis(k_InputAxisHorizontal), Input.GetAxis(k_InputAxisVertical), 0f) 
            * m_PlayerSettings.movementSpeed
            * Time.deltaTime;
    }

    void CheckForActionInput()
    {
        if (Input.GetKeyDown(m_PlayerSettings.sendWaveKey))
        {
            SendEmotionWave();
        }
    }

    void SendEmotionWave()
    {
        var wave = Instantiate(m_WavePrefab);
        wave.transform.position = transform.position;
    }
}
