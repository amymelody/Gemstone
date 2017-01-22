using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IEntity
{
    [SerializeField]
    PlayerSettings m_PlayerSettings;

    [SerializeField]
    EmotionSettings m_EmotionSettings;

    [SerializeField]
    Emotion m_InitialEmotion;

    [SerializeField]
    SpriteRenderer m_Renderer;

    Dictionary<Emotion, PlayerMovement> m_MovementTypes;
    PlayerMovement m_CurrentMovement;
    Emotion m_Emotion;

    public void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        m_CurrentMovement = m_MovementTypes[m_Emotion];
        m_Renderer.color = m_EmotionSettings.GetColorFromEmotion(m_Emotion);
    }

    public void Deserialize(JSONObject jsonObject)
    {
        // Set transform.position and m_InitialEmotion here
    }

    void Start()
    {
        m_MovementTypes = new Dictionary<Emotion, PlayerMovement>
        {
            { Emotion.Happy, new PlayerHappyMovement(transform, m_PlayerSettings) },
            { Emotion.Sad, new PlayerSadMovement(transform, m_PlayerSettings) }
        };
        ChangeEmotion(m_InitialEmotion);
    }

    void Update()
    {
        UpdateMovement();
        CheckForActionInput();
    }

    void UpdateMovement()
    {
        m_CurrentMovement.UpdatePosition();
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
