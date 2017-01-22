using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
    const string k_InputAxisHorizontal = "Horizontal";
    const string k_InputAxisVertical = "Vertical";

    [SerializeField]
    PlayerSettings m_PlayerSettings;

    [SerializeField]
    EmotionSettings m_EmotionSettings;

    [SerializeField]
    Emotion m_InitialEmotion;

    [SerializeField]
    SpriteRenderer m_Renderer;

    Emotion m_Emotion;

    public void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        m_Renderer.color = m_EmotionSettings.GetColorFromEmotion(m_Emotion);
    }

    public void Deserialize(JSONObject jsonObject)
    {
        // Set transform.position and m_InitialEmotion here
    }

    void Start()
    {
        ChangeEmotion(m_InitialEmotion);
    }

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
        EmotionWave.CreateFromSource(transform, m_EmotionSettings.GetWavePrefabFromEmotion(m_Emotion));
    }
}
