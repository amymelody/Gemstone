﻿using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    NPCSettings m_NPCSettings;

    [SerializeField]
    EmotionSettings m_EmotionSettings;

    [SerializeField]
    Emotion m_InitialEmotion;

    [SerializeField]
    SpriteRenderer m_Renderer;

    Dictionary<Emotion, NPCBehaviour> m_Behaviours;
    NPCBehaviour m_CurrentBehaviour;
    Emotion m_Emotion;

    void Awake()
    {
        m_Behaviours = new Dictionary<Emotion, NPCBehaviour>
        {
            { Emotion.Neutral, new NPCNeutralBehaviour(transform, m_NPCSettings) },
            { Emotion.Happy, new NPCHappyBehaviour(transform, m_NPCSettings) }
        };
        ChangeEmotion(m_InitialEmotion);
    }

    void Update()
    {
        UpdateMovement();
    }

    void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        m_CurrentBehaviour = m_Behaviours[m_Emotion];
        m_Renderer.color = m_EmotionSettings.GetColorFromEmotion(m_Emotion);
    }

    void UpdateMovement()
    {
        m_CurrentBehaviour.UpdatePosition();
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        var wave = otherCollider.GetComponent<EmotionWave>();
        if (wave != null)
        {
            ChangeEmotion(wave.emotion);
        }
    }
}