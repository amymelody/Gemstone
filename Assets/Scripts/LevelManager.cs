using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [SerializeField]
    Text m_Text;

    [SerializeField]
    AudioSource m_WinAudio;

    public GameManager m_GameManager;

    public bool m_LevelLoaded;

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
        if (m_LevelLoaded)
        {
            m_NumNPCsWithEmotion[oldEmotion] = m_NumNPCsWithEmotion[oldEmotion] - 1;
            m_NumNPCsWithEmotion[newEmotion] = m_NumNPCsWithEmotion[newEmotion] + 1;
            if (m_NumNPCsWithEmotion[m_WinStateEmotion] >= m_WinStateNumNPCs)
            {
                // Win condition stuff here. Go to next level.
                m_LevelLoaded = false;
                m_Text.text = "Success!";
                StartCoroutine(PlayWinAudio());
            }
        }
    }

    IEnumerator PlayWinAudio()
    {
        m_WinAudio.Play();
        while (m_WinAudio.isPlaying)
            yield return null;
        m_GameManager.LoadNextLevel();
    }

    public void LoadLevel(Transform levelParent, JSONObject playerJSONObj, JSONObject npcsJSONObj, JSONObject obstaclesJSONobj)
    {
        m_Text.gameObject.SetActive(true);
        m_Text.text = "Make " + m_WinStateNumNPCs + " Neurons " + m_WinStateEmotion;

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
            npc.m_Player = player;
            npc.Deserialize(npcsJSONObj[i]);
            npc.transform.SetParent(levelParent, true);
        }
        for (int i = 0; i < obstaclesJSONobj.Count; ++i)
        {
            //var obstacle = Instantiate(m_NPCPrefab);
            //npc.m_LevelManager = this;
            //npc.Deserialize(npcsJSONObj[i]);
        }

        m_LevelLoaded = true;
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
