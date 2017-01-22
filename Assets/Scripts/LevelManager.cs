using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    public int m_WinStateNumNPCs;

    [SerializeField]
    public Emotion m_WinStateEmotion;

    [SerializeField]
    Player m_PlayerPrefab;

    [SerializeField]
    NPC m_NPCPrefab;

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

    public void LoadLevel(Transform levelParent, JSONObject playerJSONObj, JSONObject npcsJSONObj, JSONObject obstaclesJSONobj)
    {
        m_NumNPCsWithEmotion = new Dictionary<Emotion, int>
        {
            { Emotion.Neutral, 0 },
            { Emotion.Happy, 0 },
            { Emotion.Sad, 0 },
            { Emotion.Angry, 0 },
            { Emotion.Afraid, 0 },
        };

        var player = Instantiate(m_PlayerPrefab);
        player.Deserialize(playerJSONObj);
        player.transform.SetParent(levelParent, true);

        for (int i = 0; i < npcsJSONObj.Count; ++i)
        {
            var npc = Instantiate(m_NPCPrefab);
            npc.m_LevelManager = this;
            npc.Deserialize(npcsJSONObj[i]);
            npc.transform.SetParent(levelParent, true);
        }
        for (int i = 0; i < obstaclesJSONobj.Count; ++i)
        {
            //var obstacle = Instantiate(m_NPCPrefab);
            //npc.m_LevelManager = this;
            //npc.Deserialize(npcsJSONObj[i]);
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
