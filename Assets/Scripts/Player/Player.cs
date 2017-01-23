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
        int posX = 0;
        int posY = 0;
        string emotion;
        jsonObject.GetField(out posX, "x", posX);
        jsonObject.GetField(out posY, "y", posY);
        jsonObject.GetField(out emotion, "emotion", "happy");

        m_InitialEmotion = (Emotion)System.Enum.Parse(typeof(Emotion), emotion, true);
        this.transform.position = new Vector3(posX, posY);
    }

    void Awake()
    {
        m_MovementTypes = new Dictionary<Emotion, PlayerMovement>
        {
            { Emotion.Happy, new PlayerHappyMovement(transform, m_PlayerSettings) },
            { Emotion.Sad, new PlayerSadMovement(transform, m_PlayerSettings) },
            { Emotion.Angry, new PlayerAngryMovement(transform, m_PlayerSettings) }
        };
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
        EmotionWave.CreateFromSource(
            transform,
            m_EmotionSettings.GetWavePrefabFromEmotion(m_Emotion),
            m_PlayerSettings.waveScaleRate,
            m_PlayerSettings.waveMaxDeltaScale);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var npc = collision.gameObject.GetComponent<NPC>();
        if (npc != null && npc.m_Emotion != Emotion.Neutral)
        {
            ChangeEmotion(npc.m_Emotion);
        }
    }
}
