using UnityEngine;

[CreateAssetMenu(fileName = "EmotionSettings", menuName = "Settings/EmotionSettings")]
public class EmotionSettings : ScriptableObject
{
    [Header("Colors")]
    [SerializeField]
    Color m_NeutralColor;

    [SerializeField]
    Color m_HappyColor;

    [SerializeField]
    Color m_SadColor;

    [SerializeField]
    Color m_AngryColor;

    [SerializeField]
    Color m_AfraidColor;

    [Header("Wave Prefabs")]
    [SerializeField]
    EmotionWave m_HappyWavePrefab;

    [SerializeField]
    EmotionWave m_SadWavePrefab;

    [SerializeField]
    EmotionWave m_AngryWavePrefab;

    [SerializeField]
    EmotionWave m_AfraidWavePrefab;

    public Color GetColorFromEmotion(Emotion emotion)
    {
        switch (emotion)
        {
            case Emotion.Happy:
                return m_HappyColor;
            case Emotion.Sad:
                return m_SadColor;
            case Emotion.Angry:
                return m_AngryColor;
            case Emotion.Afraid:
                return m_AfraidColor;
        }
        return m_NeutralColor;
    }

    public EmotionWave GetWavePrefabFromEmotion(Emotion emotion)
    {
        switch (emotion)
        {
            case Emotion.Happy:
                return m_HappyWavePrefab;
            case Emotion.Sad:
                return m_SadWavePrefab;
            case Emotion.Angry:
                return m_AngryWavePrefab;
        }
        return m_AfraidWavePrefab;
    }
}
