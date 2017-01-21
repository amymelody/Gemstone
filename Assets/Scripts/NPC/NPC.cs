using UnityEngine;

[RequireComponent(typeof(NPCNeutralBehaviour), typeof(NPCHappyBehaviour), typeof(NPCSadBehaviour))]
[RequireComponent(typeof(NPCAngryBehaviour), typeof(NPCAfraidBehaviour))]
public class NPC : MonoBehaviour
{
    [SerializeField]
    Emotion m_InitialEmotion;

    INPCBehaviour m_Behaviour;
    Emotion m_Emotion;

    public void ChangeEmotion(Emotion emotion)
    {
        m_Emotion = emotion;
        switch(m_Emotion)
        {
            case Emotion.Happy:
                m_Behaviour = GetComponent<NPCHappyBehaviour>();
                break;
            case Emotion.Sad:
                m_Behaviour = GetComponent<NPCSadBehaviour>();
                break;
            case Emotion.Angry:
                m_Behaviour = GetComponent<NPCAngryBehaviour>();
                break;
            case Emotion.Afraid:
                m_Behaviour = GetComponent<NPCAfraidBehaviour>();
                break;
            default:
                m_Behaviour = GetComponent<NPCNeutralBehaviour>();
                break;
        }
    }

    void Awake()
    {
        ChangeEmotion(m_InitialEmotion);
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        m_Behaviour.UpdateMovement();
    }
}
