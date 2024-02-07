using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogAdjuster : MonoBehaviour
{
    public static FogAdjuster Instance;
    private Coroutine m_FogCoroutine;
    [SerializeField] private Color m_FogColor;
    [SerializeField] private float m_FadeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.fog = true;
        RenderSettings.fogColor = m_FogColor;
        RenderSettings.fogDensity = 0.0f;
        Instance = this;
    }


    /// <summary>
    /// This is for fading in and out.
    /// </summary>
    /// <param name="a_GoalValue"></param>
    /// <returns></returns>
    private IEnumerator FadeFogCoroutine(float a_GoalValue)
    {
        float timer = 0;
        float previousFog = RenderSettings.fogDensity;
        
        while (timer < 1)
        {
            timer += Time.deltaTime * m_FadeSpeed;
            RenderSettings.fogDensity = Mathf.Lerp(previousFog, a_GoalValue, timer);    
            yield return null;
        }

        m_FogCoroutine = null;
    }

    public void FadeFog(float a_GoalValue)
    {
        if (m_FogCoroutine != null)
        {
            StopCoroutine(m_FogCoroutine);
        }
        m_FogCoroutine = StartCoroutine(FadeFogCoroutine(a_GoalValue));
    }

    public void SetFogAmount(float a_FogLevel)
    {
        RenderSettings.fogDensity = a_FogLevel;
    }

    public void StopFog()
    {
        if (m_FogCoroutine != null)
            StopCoroutine(m_FogCoroutine);
    }
}
