using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IEntity
{
    [SerializeField]
    NPCSettings m_NPCSettings;

    [SerializeField]
    EmotionSettings m_EmotionSettings;

    [SerializeField]
    Emotion m_InitialEmotion;

    [SerializeField]
    SpriteRenderer m_Renderer;

    [SerializeField]
    [Range(1, 3)]
    int m_WaveRatePowerLevel;

    [SerializeField]
    [Range(1, 3)]
    int m_WaveDistancePowerLevel;

    Dictionary<Emotion, NPCBehaviour> m_Behaviours;
    NPCBehaviour m_CurrentBehaviour;
    Emotion m_Emotion;

    public void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        m_CurrentBehaviour = m_Behaviours[m_Emotion];
        m_CurrentBehaviour.InitializeState();
        m_Renderer.color = m_EmotionSettings.GetColorFromEmotion(m_Emotion);
    }

    public void Deserialize(JSONObject jsonObject)
    {
        // Set transform.position, m_InitialEmotion, m_WaveRatePowerLevel, and m_WaveDistancePowerLevel here
    }

    void Start()
    {
        m_Behaviours = new Dictionary<Emotion, NPCBehaviour>
        {
            { Emotion.Neutral, new NPCNeutralBehaviour(transform, m_NPCSettings) },
            { Emotion.Happy, new NPCHappyBehaviour(transform, m_NPCSettings) },
            { Emotion.Sad, new NPCSadBehaviour(transform, m_NPCSettings) }
        };
        ChangeEmotion(m_InitialEmotion);
        InvokeRepeating("SendEmotionWave", m_NPCSettings.baseDelayBetweenWaves, m_NPCSettings.baseDelayBetweenWaves);
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        m_CurrentBehaviour.UpdatePosition();
    }

    void SendEmotionWave()
    {
        if (m_Emotion != Emotion.Neutral)
        {
            EmotionWave.CreateFromSource(
                transform,
                m_EmotionSettings.GetWavePrefabFromEmotion(m_Emotion),
                m_NPCSettings.waveScaleRate,
                m_NPCSettings.waveMaxDeltaScale * m_WaveDistancePowerLevel);
        }
    }
}
