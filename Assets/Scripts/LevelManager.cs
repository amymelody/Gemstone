﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    const KeyCode k_ResetKey = KeyCode.R;

    [SerializeField]
    public int m_WinStateNumNPCs;

    [SerializeField]
    public Emotion m_WinStateEmotion;

    Dictionary<Emotion, int> m_NumNPCsWithEmotion = new Dictionary<Emotion, int>
    {
        { Emotion.Neutral, 0 },
        { Emotion.Happy, 0 },
        { Emotion.Sad, 0 },
        { Emotion.Angry, 0 },
        { Emotion.Afraid, 0 },
    };

    public void RegisterNPC(Emotion npcEmotion)
    {
        m_NumNPCsWithEmotion[npcEmotion] = m_NumNPCsWithEmotion[npcEmotion] + 1;
    }

    public void OnNPCEmotionChange(Emotion oldEmotion, Emotion newEmotion)
    {
        m_NumNPCsWithEmotion[oldEmotion] = m_NumNPCsWithEmotion[oldEmotion] - 1;
        m_NumNPCsWithEmotion[newEmotion] = m_NumNPCsWithEmotion[newEmotion] + 1;
        if (m_NumNPCsWithEmotion[m_WinStateEmotion] >= m_WinStateNumNPCs)
        {
            // Win condition stuff here. Go to next level.
            Debug.Log("WIN STATE");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(k_ResetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
