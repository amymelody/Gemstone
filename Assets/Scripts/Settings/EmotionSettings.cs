using UnityEngine;

[CreateAssetMenu(fileName = "EmotionSettings", menuName = "Settings/EmotionSettings")]
public class EmotionSettings : ScriptableObject
{
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
}
