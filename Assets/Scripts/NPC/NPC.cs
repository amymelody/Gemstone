using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    NPCSettings m_NPCSettings;

    [SerializeField]
    Emotion m_InitialEmotion;

    Dictionary<Emotion, NPCBehaviour> m_Behaviours;
    NPCBehaviour m_CurrentBehaviour;
    Emotion m_Emotion;

    public void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        m_CurrentBehaviour = m_Behaviours[m_Emotion];
    }

    void Awake()
    {
        m_Behaviours = new Dictionary<Emotion, NPCBehaviour>
        {
            { Emotion.Neutral, new NPCNeutralBehaviour(transform, m_NPCSettings) }
        };
        ChangeEmotion(m_InitialEmotion);
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        m_CurrentBehaviour.UpdatePosition();
    }
}
