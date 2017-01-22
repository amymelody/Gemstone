using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Intro = 0,
        Level
    }

    const KeyCode k_ResetKey = KeyCode.R;
    const string k_LevelSceneName = "level";
    const string k_LevelParentName = "levelParent";

    [SerializeField]
    PlayerSettings m_PlayerSettings;

    [SerializeField]
    LevelManager m_LevelManager;

    [SerializeField]
    string m_LevelJSONsFolder = "Levels";

    [SerializeField]
    string[] m_LevelJSONs;

    [SerializeField]
    Text m_IntroText;

    GameState m_GameState;
    JSONObject[] m_LevelJSONObjects;
    int m_CurrentLevel = -1;
    Transform m_LevelParentTransform;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        m_LevelJSONObjects = new JSONObject[m_LevelJSONs.Length];
        for (int i = 0; i < m_LevelJSONObjects.Length; ++i)
        {
            m_LevelJSONObjects[i] = new JSONObject(File.ReadAllText(
                Application.streamingAssetsPath + Path.DirectorySeparatorChar
                + m_LevelJSONsFolder + Path.DirectorySeparatorChar + m_LevelJSONs[i]));
        }
    }

    void Update()
    {
        if (m_GameState == GameState.Intro && Input.GetKeyDown(m_PlayerSettings.sendWaveKey))
        {
            StartCoroutine(LoadFirstLevel());
        }

        if (m_GameState == GameState.Level && Input.GetKeyDown(k_ResetKey))
        {
            LoadLevel(m_CurrentLevel);
        }
    }

    IEnumerator LoadFirstLevel()
    {
        m_GameState = GameState.Level;

        m_IntroText.gameObject.SetActive(false);

        SceneManager.LoadScene(k_LevelSceneName);
        var scene = SceneManager.GetSceneByName(k_LevelSceneName);
        while (!scene.isLoaded)
            yield return null;

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        m_CurrentLevel++;
        LoadLevel(m_CurrentLevel);
    }

    public void LoadLevel(int index)
    {
        if (m_LevelParentTransform != null)
        {
            DestroyImmediate(m_LevelParentTransform.gameObject);
        }

        m_LevelParentTransform = new GameObject(k_LevelParentName).transform;

        var levelJSONObject = m_LevelJSONObjects[index];
        var winConObj = levelJSONObject.GetField("winCon");
        var winConEmotion = "";
        var winConNumber = 0;
        winConObj.GetField(out winConEmotion, "emotion", winConEmotion);
        winConObj.GetField(out winConNumber, "number", winConNumber);
        m_LevelManager.m_WinStateEmotion = (Emotion)Enum.Parse(typeof(Emotion), winConEmotion, true);
        m_LevelManager.m_WinStateNumNPCs = winConNumber;
        m_LevelManager.LoadLevel(m_LevelParentTransform, levelJSONObject.GetField("player"), levelJSONObject.GetField("NPCArray"), levelJSONObject.GetField("Obstacle"));
    }
}
