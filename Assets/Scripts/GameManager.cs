using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Intro = 0,
        Level
    }

    const string k_LevelSceneName = "level";

    [SerializeField]
    PlayerSettings m_PlayerSettings;

    [SerializeField]
    LevelManager m_LevelManagerPrefab;

    [SerializeField]
    Player m_PlayerPrefab;

    [SerializeField]
    NPC m_NPCPrefab;

    [SerializeField]
    string m_LevelJSONsFolder = "Levels";

    [SerializeField]
    string[] m_LevelJSONs;

    GameState m_GameState;
    JSONObject[] m_LevelJSONObjects;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_LevelJSONObjects = new JSONObject[m_LevelJSONs.Length];
        for (int i = 0; i < m_LevelJSONObjects.Length; ++i)
        {
            m_LevelJSONObjects[i] = new JSONObject(File.ReadAllText(
                Application.dataPath + Path.DirectorySeparatorChar
                + m_LevelJSONsFolder + Path.DirectorySeparatorChar + m_LevelJSONs[i]));
        }
    }

    void Update()
    {
        if (m_GameState == GameState.Intro && Input.GetKeyDown(m_PlayerSettings.sendWaveKey))
        {
            m_GameState = GameState.Level;
            StartCoroutine(LoadLevel(0));
        }
    }

    public IEnumerator LoadLevel(int index)
    {
        SceneManager.LoadScene(k_LevelSceneName);
        var scene = SceneManager.GetSceneByName(k_LevelSceneName);
        while (!scene.isLoaded)
            yield return null;

        var levelJSONObject = m_LevelJSONObjects[index];
        var levelManager = Instantiate(m_LevelManagerPrefab);
        var winConObj = levelJSONObject.GetField("winCon");
        var winConEmotion = "";
        var winConNumber = 0;
        winConObj.GetField(out winConEmotion, "emotion", winConEmotion);
        winConObj.GetField(out winConNumber, "number", winConNumber);
        levelManager.m_WinStateEmotion = (Emotion)Enum.Parse(typeof(Emotion), winConEmotion, true);
        levelManager.m_WinStateNumNPCs = winConNumber;
    }
}
