using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IEntity
{
    [SerializeField]
    LevelManager m_LevelManager;

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
        var oldEmotion = m_Emotion;
        m_Emotion = emotion;
        m_CurrentBehaviour = m_Behaviours[m_Emotion];
        m_CurrentBehaviour.InitializeState();
        m_Renderer.color = m_EmotionSettings.GetColorFromEmotion(m_Emotion);
        m_LevelManager.OnNPCEmotionChange(oldEmotion, m_Emotion);
    }

    public void Deserialize(JSONObject jsonObject)
    {

        int posX = 0;
        int posY = 0;
        int power = 0;
        int frequency = 0;
        string emotion;
        jsonObject.GetField(out posX, "x", posX);
        jsonObject.GetField(out posY, "y", posY);
        jsonObject.GetField(out power, "power", power);
        jsonObject.GetField(out frequency, "frequency", frequency);
        jsonObject.GetField(out emotion, "emotion", "happy");

        m_Emotion = (Emotion)System.Enum.Parse(typeof(Emotion), emotion, true);
        this.transform.position = new Vector3(posX, posY);
        m_WaveDistancePowerLevel = power;
        m_WaveRatePowerLevel = frequency;
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
        m_LevelManager.RegisterNPC(m_Emotion);
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
