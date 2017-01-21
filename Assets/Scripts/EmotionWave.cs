using System.Collections;
using UnityEngine;

public class EmotionWave : MonoBehaviour
{
    [SerializeField]
    float m_ScaleRate;

    [SerializeField]
    float m_MaxDeltaScale;

    [SerializeField]
    Emotion m_Emotion;

    public Emotion emotion { get { return m_Emotion; } }

    void Awake()
    {
        StartCoroutine(Grow());
        if (m_ScaleRate <= 1f)
        {
            m_ScaleRate = 1f + Mathf.Epsilon;
        }
    }

    IEnumerator Grow()
    {
        var maxScale = transform.localScale.x * m_MaxDeltaScale;
        while (transform.localScale.x < maxScale)
        {
            transform.localScale *= Mathf.Pow(m_ScaleRate, Time.deltaTime);
            yield return null;
        }
        Destroy(gameObject);
    }
}
