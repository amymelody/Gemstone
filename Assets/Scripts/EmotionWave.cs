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

    Transform m_Source;
    
    public static void CreateFromSource(Transform source, EmotionWave prefab, float scaleRate, float maxDeltaScale)
    {
        var wave = Instantiate(prefab);
        wave.transform.position = source.position;
        wave.m_Source = source;
        wave.m_ScaleRate = scaleRate;
        wave.m_MaxDeltaScale = maxDeltaScale;
        if (wave.m_ScaleRate <= 1f)
        {
            wave.m_ScaleRate = 1f + Mathf.Epsilon;
        }
        wave.StartCoroutine(wave.Grow());
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

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.transform != m_Source)
        {
            var entity = otherCollider.GetComponent<IEntity>();
            if (entity != null)
            {
                entity.ChangeEmotion(m_Emotion);
            }
        }
    }
}
